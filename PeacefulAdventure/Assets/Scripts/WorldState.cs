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
}

public class WorldStateRepresentation {
    public Vector2 playerPosition = Vector2.zero;
    public Dictionary<PositionID, List<Item>> chests = new Dictionary<PositionID, List<Item>>();
    public Dictionary<PositionID, SkeletonState> skeletons = new Dictionary<PositionID, SkeletonState>();
    public Dictionary<PositionID, bool> items = new Dictionary<PositionID, bool>();
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
