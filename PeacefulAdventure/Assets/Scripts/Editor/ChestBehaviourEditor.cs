using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChestBehaviour), true)]
public class ChestBehaviourEditor : Editor
{
    ChestBehaviour chest;

    private void Awake() {
        this.chest = (ChestBehaviour)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Fill with random items")) {
            chest.InitializeItemsRandomly(Random.Range(2, 4));
        }
        if (GUILayout.Button("Fill with one of each")) {
            chest.FillWithOneOfEach();
        }
    }
}
