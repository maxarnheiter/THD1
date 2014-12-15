using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class PrefabManagerWindow : EditorWindow {



	static Vector2 size = Vector2.zero;

	[MenuItem ("THD/Prefab Window")]
	static void Init () {
		PrefabManagerWindow prefabManagerWindow = (PrefabManagerWindow)EditorWindow.GetWindow (typeof(PrefabManagerWindow));
	}

	void OnEnable() {
		this.title = "Prefab Editor";
		PrefabManager.TryLoadOnce();
	}
	
	void OnGUI()
	{

		PrefabManager.TryLoadOnce();
	
		EditorGUILayout.Space ();
		
		EditorGUILayout.BeginHorizontal();

		if (PrefabManager.hasPrefabs) 
			GUI.enabled = false;
		if(GUILayout.Button ("Load Prefabs", GUILayout.Width (100f)))
				PrefabManager.Load();
		GUI.enabled = true;
		
		if (!PrefabManager.hasPrefabs) 
			GUI.enabled = false;
		if(GUILayout.Button ("Reload Prefabs", GUILayout.Width (100f)))
				PrefabManager.Reload();
		GUI.enabled = true;
		
		EditorGUILayout.EndHorizontal();
		
		if(GUILayout.Button ("Get Next Prefab ID", GUILayout.Width (200f))) {
			if(PrefabManager.hasPrefabs)
				EditorUtility.DisplayDialog("Next Prefab ID:", PrefabManager.nextId.ToString(), "OK");
		}

		GUILayout.Label ("There are currently " + PrefabManager.count + " prefabs loaded.");
		

		 
	}

}
