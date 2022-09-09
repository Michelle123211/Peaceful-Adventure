using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public int minRoomWidth, maxRoomWidth, minRoomHeight, maxRoomHeight;
    public int minNumOfRooms, maxNumOfRooms;

    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap outterWallTilemap;
    [SerializeField] private Tilemap groundTilemap;

    [SerializeField] private TileBase voidTile;

    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase outterWallTile;

    private HashSet<Vector2Int> ground;
    private HashSet<Vector2Int> walls;
    private HashSet<Vector2Int> outterWalls;

    // TODO: Would be called from SceneLoader, if new state should not be loaded?
    public void GenerateDungeon() {

        DirectionPicker.Initialize();

        GenerateRoomsAndCorridors();
        GenerateWalls();
        
        DrawDungeon();
    }

    // TODO: Would be called directly from SceneLoader, if previous state should be loaded?
    public void DrawDungeon() {
        groundTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        outterWallTilemap.ClearAllTiles();

        DrawGround();
        DrawWalls();
    }

    public void SetState(DungeonState state) {
        this.ground = state.ground;
        // TODO: Set other things as well
    }

    private void AddRoom(BoundsInt room) {
        for (int x = room.min.x; x < room.min.x + room.size.x; x++) {
            for (int y = room.min.y; y < room.min.y + room.size.y; y++) {
                this.ground.Add(new Vector2Int(x, y));
            }
        }
    }

    private void GenerateRoomsAndCorridors() {
        this.ground = new HashSet<Vector2Int>();

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
            // select, where there will be opening to the next room (horizontal may be only 1 high, vertical must be at least 2 wide)
            // and add corridor
            int ySplit, xSplit;
            switch (direction) {
                case Direction.Left:
                    ySplit = previousRoom.min.y + Random.Range(1, previousRoom.size.y);
                    position = new Vector3Int(previousRoom.min.x - 1 - size.x, ySplit - Random.Range(1, size.y - 1), 0);
                    corridor = new BoundsInt(new Vector3Int(previousRoom.min.x - 1, ySplit, 0), new Vector3Int(1, 1, 0));
                    break;
                case Direction.Right:
                    ySplit = previousRoom.min.y + Random.Range(1, previousRoom.size.y);
                    position = new Vector3Int(previousRoom.max.x + 1, ySplit - Random.Range(1, size.y - 1), 0);
                    corridor = new BoundsInt(new Vector3Int(previousRoom.max.x, ySplit, 0), new Vector3Int(1, 1, 0));
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
            AddRoom(currentRoom);
            AddRoom(corridor);
        }
    }

    private void GenerateWalls() {
        this.walls = new HashSet<Vector2Int>();
        this.outterWalls = new HashSet<Vector2Int>();
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
        // remove floor underneath a wall if necessary
        foreach (var position in this.walls) {
            if (this.ground.Contains(position)) {
                this.ground.Remove(position);
            }
        }
        foreach (var position in this.outterWalls) {
            if (this.ground.Contains(position)) {
                this.ground.Remove(position);
            }
        }
    }

    // TODO: Predelat, aby byla jedna obecna metoda, predam Tilemap, TileBase a HasSet, vykresli to

    private void DrawGround() {
        foreach (var position in this.ground) {
            //Debug.Log("Tile on position: " + position.x + "," + position.y);
            var tilePosition = groundTilemap.WorldToCell((Vector3Int)position);
            groundTilemap.SetTile(tilePosition, groundTile);
        }
    }

    private void DrawWalls() {
        foreach (var position in this.walls) {
            var tilePosition = wallTilemap.WorldToCell((Vector3Int)position);
            wallTilemap.SetTile(tilePosition, wallTile);
        }
        foreach (var position in this.outterWalls) {
            var tilePosition = outterWallTilemap.WorldToCell((Vector3Int)position);
            outterWallTilemap.SetTile(tilePosition, outterWallTile);
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
        //Debug.Log("Prohibited: " + ((Direction)prohibited[0]).ToString() + " and " + ((Direction)prohibited[1]).ToString());
        int next = Random.Range(0, numOfAllowed);
        //Debug.Log("Generated next: " + next);
        int index = next;
        if (next >= prohibited[0]) index++;
        if (prohibited[0] != prohibited[1] && next >= prohibited[1]) index++;
        //for (int i = 0; i <= next; i++) {
        //    if (prohibited[0] == i || prohibited[1] == i) index++;
        //}
        // update fields
        prohibited[0] = prohibited[1];
        prohibited[1] = (index + 2) % 4; // prohibit the opposite direction
        // return the direction
        Debug.Log("Picked direction: " + ((Direction)index).ToString());
        return (Direction)index;
    }
}

public class DungeonState {
    public HashSet<Vector2Int> ground = new HashSet<Vector2Int>();
}

// TODO: Enums for ground and wall tiles - with numbers corresponding to bitmasks of neighbours

enum Direction { 
    Up,
    Right,
    Down,
    Left
}

enum GroundTile {
    TopLeftCorner,
    TopRightCorner,
    TopEdge,
    RightEdge,
    LeftEdge,
    Middle,
    LeftInnerCorner,
    RightInnerCorner//,
    //LeftRightInnerCorner,
    //VerticalCorridor
}


enum WallTile { 
    Top,
    Bottom,
    Horizontal,
    LeftEnd,
    Crossroad,
    TopCrossroad,
    RightCrossroad,
    BottomCrossroad,
    LeftCrossroad,
}