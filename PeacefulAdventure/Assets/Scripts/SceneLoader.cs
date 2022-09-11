using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private void Start() {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMap") {
            // delete Dungeon and HouseIndoor state if available
            WorldState.Instance.sceneStates.Remove("HouseIndoor");
            WorldState.Instance.sceneStates.Remove("Dungeon");
        }
        LoadState(currentScene); // load state if available
    }

    public void LoadScene(string sceneName) {
        string currentScene = SceneManager.GetActiveScene().name;
        // save the current state (rewriting any previous one)
        SaveState(currentScene);
        // load new scene
        SceneManager.LoadScene(sceneName);
    }

    private void LoadState(string currentScene) {
        WorldStateRepresentation state;
        if (WorldState.Instance.sceneStates.TryGetValue(currentScene, out state)) {
            if (currentScene == "Dungeon") {
                DungeonGenerator dungeonGenerator = FindObjectOfType<DungeonGenerator>();
                dungeonGenerator?.SetState(state.dungeonState);
                dungeonGenerator?.DrawDungeon();
            }
            FindObjectOfType<PlayerBehaviour>().gameObject.transform.position = state.playerPosition; // player's position
            LoadBehaviour<ChestBehaviour, List<Item>>(state.chests); // chests
            LoadBehaviour<SkeletonBehaviour, SkeletonState>(state.skeletons); // skeletons
            LoadBehaviour<PickableItem, bool>(state.items); // items
        } else if (currentScene == "Dungeon") {
            // generate a new dungeon
            DungeonGenerator dungeonGenerator = FindObjectOfType<DungeonGenerator>();
            dungeonGenerator?.GenerateDungeon();
        }
    }

    private void LoadBehaviour<Behaviour, State>(Dictionary<PositionID, State> worldState) where Behaviour : Component, ISaveable<State> {
        List<Behaviour> behaviours = Utils.FindObject<Behaviour>();
        foreach (var behaviour in behaviours) {
            PositionID id = behaviour.GetID();
            State state;
            if (worldState.TryGetValue(id, out state)) {
                behaviour.LoadState(state);
            } else {
                Destroy(behaviour.gameObject);
            }
        }
    }

    private void SaveState(string currentScene) {
        WorldStateRepresentation state = new WorldStateRepresentation();
        if (currentScene == "Dungeon") {
            DungeonGenerator dungeonGenerator = FindObjectOfType<DungeonGenerator>();
            state.dungeonState = dungeonGenerator?.GetState();
        }
        state.playerPosition = FindObjectOfType<PlayerBehaviour>().gameObject.transform.position; // player's position
        SaveBehaviour<ChestBehaviour, List<Item>>(state.chests); // chests
        SaveBehaviour<SkeletonBehaviour, SkeletonState>(state.skeletons); // skeletons
        SaveBehaviour<PickableItem, bool>(state.items); // items
        WorldState.Instance.sceneStates[currentScene] = state;
    }

    private void SaveBehaviour<Behaviour, State>(Dictionary<PositionID, State> worldState) where Behaviour : Component, ISaveable<State> {
        List<Behaviour> behaviours = Utils.FindObject<Behaviour>();
        foreach (var behaviour in behaviours) {
            PositionID id = behaviour.GetID();
            worldState[id] = behaviour.SaveState();
        }
    }
}
