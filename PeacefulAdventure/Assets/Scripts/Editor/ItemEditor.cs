using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    Item item;

    SerializedProperty icon;
    SerializedProperty itemName;
    SerializedProperty description;

    private void Awake() {
        this.item = (Item)target;
    }

    private void OnEnable() {
        icon = serializedObject.FindProperty("icon");
        itemName = serializedObject.FindProperty("itemName");
        description = serializedObject.FindProperty("description");
    }

    public override void OnInspectorGUI() {
        // load the current values
        serializedObject.Update();
        // icon preview
        GUILayout.BeginHorizontal();
        GUILayout.Label("Icon");
        icon.objectReferenceValue = EditorGUILayout.ObjectField(item.icon, typeof(Sprite), false, GUILayout.Height(80), GUILayout.Width(80)) as Sprite;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        // exclude unnecessary properties
        Editor.DrawPropertiesExcluding(serializedObject, "icon", "m_Script");
        // help boxes
        if (icon.objectReferenceValue == null) {
            EditorGUILayout.HelpBox("The item needs an icon, asign it please.", MessageType.Warning);
        } else if (itemName.stringValue == null || itemName.stringValue == "") {
            EditorGUILayout.HelpBox("The item needs a name, fill it in please.", MessageType.Warning);
        } else if (description.stringValue == null || description.stringValue == "") {
            EditorGUILayout.HelpBox("The item needs a description, fill it in please.", MessageType.Warning);
        }
        // write changes back
        serializedObject.ApplyModifiedProperties();
    }
}
