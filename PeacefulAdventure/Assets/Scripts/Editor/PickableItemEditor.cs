using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PickableItem), true)]
public class PickableItemEditor : Editor
{
    PickableItem item;

    private void Awake() {
        this.item = (PickableItem)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update visuals")) {
            item.SetItemSprite();
            item.SetLightColor();
        }
    }
}
