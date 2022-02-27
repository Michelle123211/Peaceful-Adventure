using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor {
    DungeonGenerator generator;

    private void Awake() {
        this.generator = (DungeonGenerator)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Dungeon")) {
            generator.GenerateDungeon();
        }
    }
}
