using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrefabManagerWindow : EditorWindow {

	[MenuItem ("THD/Prefab Window")]
	static void Init () {
		PrefabManagerWindow prefabManagerWindow = (PrefabManagerWindow)EditorWindow.GetWindow (typeof(PrefabManagerWindow));
	}

	void OnEnable() {
		this.title = "Prefab Editor";
	}
	
	void OnGUI()
	{
		EditorGUILayout.Space ();

		if (PrefabManager.hasPrefabs) 
			GUI.enabled = false;
		if(GUILayout.Button ("Load Prefabs", GUILayout.Width (100f)))
				PrefabManager.LoadPrefabs();

		GUI.enabled = true;

		GUILayout.Label ("There are currently " + PrefabManager.count + " prefabs loaded.");

		if (PrefabManager.hasPrefabs)
		foreach (var prefab in PrefabManager.prefabTextures) {
			GUILayout.Button (prefab.Value);		
		}
	}

}
