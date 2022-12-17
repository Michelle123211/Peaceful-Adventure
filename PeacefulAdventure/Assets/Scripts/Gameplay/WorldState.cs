using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    private static WorldState instance;
    public static WorldState Instance {
        get {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<WorldState>();
            if (instance == null) {
                GameObject ps = new GameObject();
                ps.name = "WorldState";
                instance = ps.AddComponent<WorldState>();
            }
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    public Dictionary<string, WorldStateRepresentation> sceneStates = new Dictionary<string, WorldStateRepresentation>();

    public static void Reset() {
        if (instance != null)
            instance.sceneStates.Clear();
    }
}

public class WorldStateRepresentation {
    public Vector2 playerPosition = Vector2.zero;
    public Dictionary<PositionID, List<InventoryItem>> chests = new Dictionary<PositionID, List<InventoryItem>>();
    public Dictionary<PositionID, SkeletonState> skeletons = new Dictionary<PositionID, SkeletonState>();
    public Dictionary<PositionID, bool> items = new Dictionary<PositionID, bool>();
    public DungeonState dungeonState = null;
}

public interface ISaveable<State> {
    public PositionID GetID();
    public State SaveState();
    public void LoadState(State model);
}

public struct PositionID {
    public int x;
    public int y;
}
