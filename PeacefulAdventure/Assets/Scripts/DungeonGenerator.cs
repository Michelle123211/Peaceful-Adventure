using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour {
    public int minRoomWidth, maxRoomWidth, minRoomHeight, maxRoomHeight;
    public int minNumOfRooms, maxNumOfRooms;

    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap groundTilemap;

    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase outterWallTile;

    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private Transform skeletonsParent;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private Transform chestsParent;

    private HashSet<Vector2Int> ground = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> walls = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> outterWalls = new HashSet<Vector2Int>();

    private HashSet<Vector2Int> skeletons = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> chests = new HashSet<Vector2Int>();

    public void GenerateDungeon() {
        Initialize();

        GenerateRoomsAndCorridors();
        GenerateWalls();

        DrawDungeon();
    }

    public void DrawDungeon() {
        ClearEverything();

        DrawTiles(groundTilemap, groundTile, ground);
        DrawTiles(wallTilemap, outterWallTile, outterWalls);
        DrawTiles(wallTilemap, wallTile, walls);

        DrawGameObjects(skeletonsParent, skeletonPrefab, skeletons);
        DrawGameObjects(chestsParent, chestPrefab, chests);
        InitializeChests();
    }

    public DungeonState GetState() {
        DungeonState state = new DungeonState();
        state.ground = this.ground;
        state.walls = this.walls;
        state.outterWalls = this.outterWalls;
        state.skeletons = this.skeletons;
        state.chests = this.chests;
        return state;
    }

    public void SetState(DungeonState state) {
        if (state == null) {
            Initialize();
            return;
        }
        this.ground = state.ground;
        this.walls = state.walls;
        this.outterWalls = state.outterWalls;
        this.skeletons = state.skeletons;
        this.chests = state.chests;
}

    private void ClearEverything() {
        groundTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        RemoveGameObjects(skeletonsParent);
        RemoveGameObjects(chestsParent);
    }

    private void Initialize() {
        this.ground = new HashSet<Vector2Int>();
        this.skeletons = new HashSet<Vector2Int>();
        this.chests = new HashSet<Vector2Int>();
        this.walls = new HashSet<Vector2Int>();
        this.outterWalls = new HashSet<Vector2Int>();

        DirectionPicker.Initialize();
    }

    private void RemoveGameObjects(Transform parent) {
        while (parent.childCount > 0) {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }

    private void AddRoom(BoundsInt room, bool addChest = false) {
        for (int x = room.min.x; x < room.min.x + room.size.x; x++) {
            for (int y = room.min.y; y < room.min.y + room.size.y; y++) {
                this.ground.Add(new Vector2Int(x, y));
            }
        }
        // if the room is big enough, add skeletons and chests
        int size = room.size.x * room.size.y;
        if (size > 20) {
            int numOfSkeletons = size / 30;
            int numOfChests = addChest ? 1 : 0;
            // select a random placement for everything
            List<int> positionIndex = new List<int>();
            while (positionIndex.Count < numOfSkeletons + numOfChests) { 
                int r = Random.Range(0, size - 1);
                if (!positionIndex.Contains(r))
                    positionIndex.Add(r);
            }
            // store positions of the skeletons and chests
            for (int i = 0; i < positionIndex.Count; ++i) {
                int posIdx = positionIndex[i];
                Vector2Int position = new Vector2Int(room.min.x + (posIdx % room.size.x), room.min.y + (posIdx / room.size.x));
                if (i < numOfSkeletons) {
                    if (Vector2Int.Distance(Vector2Int.zero, position) > 5) // not too close to the player's initial position
                        this.skeletons.Add(position);
                } else {
                    this.chests.Add(position);
                }
            }
        }
    }

    private void GenerateRoomsAndCorridors() {

        // generate rooms, one after another, rooms will be adjacent, divided only by one row of wall (no corridors, tileset is not prepared for corridors going down)
        // first room should contain 0,0 (starting position of player) in the top left corner
        Vector3Int size = new Vector3Int(Random.Range(minRoomWidth, maxRoomWidth + 1), Random.Range(minRoomHeight, maxRoomHeight + 1), 0);
        Vector3Int position = new Vector3Int(-1, -size.y, 0);
        BoundsInt currentRoom = new BoundsInt(position, size);
        AddRoom(currentRoom);

        BoundsInt previousRoom, corridor = new BoundsInt();
        Direction direction;
        int numOfRooms = Random.Range(minNumOfRooms - 1, maxNumOfRooms);
        for (int i = 0; i < numOfRooms; i++) {
            previousRoom = currentRoom;
            size = new Vector3Int(Random.Range(minRoomWidth, maxRoomWidth + 1), Random.Range(minRoomHeight, maxRoomHeight + 1), 0);
            // then continue by selecting random direction (not opposite the 2 last selected, for the first room only right or bottom)
            direction = DirectionPicker.GetNextDirection();
            // select, where there will be opening to the next room (horizontal should be at least 1 high, but 2 is better, vertical must be at least 2 wide)
            // and add corridor
            int ySplit, xSplit;
            switch (direction) {
                case Direction.Left:
                    ySplit = previousRoom.min.y + Random.Range(1, previousRoom.size.y);
                    position = new Vector3Int(previousRoom.min.x - 1 - size.x, ySplit - Random.Range(1, size.y - 1), 0);
                    corridor = new BoundsInt(new Vector3Int(previousRoom.min.x - 1, ySplit - 1, 0), new Vector3Int(1, 2, 0));
                    break;
                case Direction.Right:
                    ySplit = previousRoom.min.y + Random.Range(1, previousRoom.size.y);
                    position = new Vector3Int(previousRoom.max.x + 1, ySplit - Random.Range(1, size.y - 1), 0);
                    corridor = new BoundsInt(new Vector3Int(previousRoom.max.x, ySplit - 1, 0), new Vector3Int(1, 2, 0));
                    break;
                case Direction.Up:
                    xSplit = previousRoom.min.x + Random.Range(1, previousRoom.size.x);
                    position = new Vector3Int(xSplit - Random.Range(1, size.x - 1), previousRoom.max.y + 2, 0);
                    corridor = new BoundsInt(new Vector3Int(xSplit - 1, previousRoom.max.y, 0), new Vector3Int(2, 2, 0));
                    break;
                case Direction.Down:
                    xSplit = previousRoom.min.x + Random.Range(1, previousRoom.size.x);
                    position = new Vector3Int(xSplit - Random.Range(1, size.x - 1), previousRoom.min.y - 2 - size.y, 0);
                    corridor = new BoundsInt(new Vector3Int(xSplit - 1, previousRoom.min.y - 2, 0), new Vector3Int(2, 2, 0));
                    break;
            }
            // generate a new room adjacent to the previous one, in the selected direction and with the selected opening
            currentRoom = new BoundsInt(position, size);
            if (i < numOfRooms - 1)
                AddRoom(currentRoom);
            else
                AddRoom(currentRoom, true); // add a chest to the last room
            AddRoom(corridor);
        }
    }

    private void GenerateWalls() {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left,
            Vector2Int.up+Vector2Int.right, Vector2Int.right+Vector2Int.down , Vector2Int.down+Vector2Int.left, Vector2Int.left+Vector2Int.up };
        foreach (var position in this.ground) {
            foreach (var direction in directions) {
                var neighbourPos = position + direction;
                if (!this.ground.Contains(neighbourPos)) {
                    if (direction == Vector2Int.up) { // in the up direction the walls have height 2
                        this.walls.Add(neighbourPos);
                        this.outterWalls.Add(neighbourPos + Vector2Int.up);
                    } else {
                        this.outterWalls.Add(neighbourPos);
                        neighbourPos += Vector2Int.up;
                        if (direction.y == Vector2Int.up.y && !this.ground.Contains(neighbourPos)) { // in the up direction the walls have height 2
                            this.outterWalls.Add(neighbourPos);
                        }
                    }
                }
            }
        }
        // remove floor or other objects underneath a wall if necessary
        this.ground.Subtract(this.walls);
        this.ground.Subtract(this.outterWalls);
        this.skeletons.Subtract(this.walls);
        this.skeletons.Subtract(this.outterWalls);
        this.chests.Subtract(this.walls);
        this.chests.Subtract(this.outterWalls);
    }

    private void DrawTiles(Tilemap tilemap, TileBase tile, HashSet<Vector2Int> positions) {
        foreach (var position in positions) {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }
    }

    private void DrawGameObjects(Transform parent, GameObject original, HashSet<Vector2Int> positions) {
        foreach (var position in positions) {
            Instantiate(original, (Vector3)((Vector2)position + Vector2.one * 0.5f), Quaternion.identity, parent);
        }
    }

    private void InitializeChests() {
        foreach (Transform chest in chestsParent) {
            ChestBehaviour chestBeh = chest.gameObject.GetComponent<ChestBehaviour>();
            if (chestBeh != null) {
                int minCount = 3;
                int maxCount = Mathf.Min(Mathf.Max(skeletons.Count, 1) * 2, 12);
                chestBeh.InitializeItemsRandomly(Random.Range(minCount, maxCount));
            }
        }
    }
}

static class DirectionPicker {

    private static int[] prohibited = new int[2] { 0, 3 }; // don't pick up or left from the first room

    public static void Initialize() { 
        prohibited = new int[2] { 0, 3 }; // don't pick up or left from the first room
    }

    public static Direction GetNextDirection() {
        int numOfAllowed;
        if (prohibited[0] == prohibited[1]) numOfAllowed = 3;
        else numOfAllowed = 2;
        // get next direction
        int next = Random.Range(0, numOfAllowed);
        int index = next;
        if (next >= prohibited[0]) index++;
        if (prohibited[0] != prohibited[1] && next >= prohibited[1]) index++;
        // update fields
        prohibited[0] = prohibited[1];
        prohibited[1] = (index + 2) % 4; // prohibit the opposite direction
        // return the direction
        return (Direction)index;
    }
}
enum Direction {
    Up,
    Right,
    Down,
    Left
}

public class DungeonState {
    public HashSet<Vector2Int> ground = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> walls = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> outterWalls = new HashSet<Vector2Int>();

    public HashSet<Vector2Int> skeletons = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> chests = new HashSet<Vector2Int>();
}