using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public List<Item> items;

    private int width = 180;
    private int height = 60;
    private int offsetX = 10;
    private int offsetY = 10;

    private int ColumnToX(int column) => Screen.width - (2 - column) * width - offsetX;
    private int RowToY(int row) => offsetY + row * height;

    private bool show = false;

    private void Awake() {
        Cheats[] objs = FindObjectsOfType<Cheats>();
        if (objs.Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleVisibility() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    private void OnGUI() {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        GUI.skin.button.fontSize = 32;

        if (!show) {
            if (GUI.Button(new Rect(ColumnToX(1), RowToY(0), width, height), "Show")) {
                Debug.Log("Show cheats");
                show = true;
            }
        } else {
            if (GUI.Button(new Rect(ColumnToX(1), RowToY(0), width, height), "Hide")) {
                Debug.Log("Hide cheats");
                show = false;
            }

            // Increase health +10
            if (GUI.Button(new Rect(ColumnToX(0), RowToY(1), width, height), "Health +10")) {
                Debug.Log("Health +10");
                PlayerState.Instance.UpdateHealth(10);
            }
            // Decrease health -10
            if (GUI.Button(new Rect(ColumnToX(1), RowToY(1), width, height), "Health -10")) {
                Debug.Log("Health -10");
                PlayerState.Instance.UpdateHealth(-10);
            }

            // Increase XP +20
            if (GUI.Button(new Rect(ColumnToX(0), RowToY(2), width, height), "XP +25")) {
                Debug.Log("XP +25");
                PlayerState.Instance.levelSystem.UpdateExperience(25);
            }

            // Add one of each item
            if (GUI.Button(new Rect(ColumnToX(1), RowToY(2), width, height), "Add items")) {
                Debug.Log("Add items");
                foreach (var item in items) {
                    PlayerState.Instance.inventory.AddToInventory(item);
                }
            }

            // Teleport to the town scene
            if (GUI.Button(new Rect(ColumnToX(-1), RowToY(3), width, height), "Town")) {
                Debug.Log("Town");
                FindObjectOfType<SceneLoader>().LoadSceneWithState("MainMap");
            }
            // Teleport to the house scene
            if (GUI.Button(new Rect(ColumnToX(0), RowToY(3), width, height), "House")) {
                Debug.Log("House");
                FindObjectOfType<SceneLoader>().LoadSceneWithState("HouseIndoor");
            }
            // Teleport to the dungeon scene
            if (GUI.Button(new Rect(ColumnToX(1), RowToY(3), width, height), "Dungeon")) {
                Debug.Log("Dungeon");
                FindObjectOfType<SceneLoader>().LoadSceneWithState("Dungeon");
            }

        }
#endif
    }
}
