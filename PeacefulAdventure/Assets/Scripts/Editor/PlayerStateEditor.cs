using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerState))]
public class PlayerStateEditor : Editor
{
    Vector2 scrollPosition = Vector2.zero;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        PlayerState state = (PlayerState)target;
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(300));

        if (state.levelSystem != null) {
            for (int i = 0; i <= state.levelSystem.maxLevel; ++i) {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField($"Level {i}");
                EditorGUILayout.LabelField($"{state.levelSystem.GetExperienceNeededForLevel(i)} XP");
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
