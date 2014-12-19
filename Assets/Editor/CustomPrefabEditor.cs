using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Prefab))]
public class CustomPrefabEditor : Editor {

	public override void OnInspectorGUI()
	{
		Prefab prefab = target as Prefab;
		
		prefab.prefabType = (PrefabType)EditorGUILayout.EnumMaskField(prefab.prefabType);
		prefab.prefabCategory = (PrefabCategory)EditorGUILayout.EnumMaskField(prefab.prefabCategory);
		prefab.prefabColor = (PrefabColor)EditorGUILayout.EnumMaskField(prefab.prefabColor);
	}

	
}
