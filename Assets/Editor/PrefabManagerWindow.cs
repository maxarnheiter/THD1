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
	}

}
