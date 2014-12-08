using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;

public class PrefabManagerWindow : EditorWindow {

	static int minimumPreviewSize = 32;
	static int previewPadding = 12;

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
		
		if (PrefabManager.hasPrefabs) {
		
			var width = this.position.width;
			var count = PrefabManager.prefabTextures.Count;

			size = EditorGUILayout.BeginScrollView(size, GUILayout.Height(200f));

			EditorGUILayout.BeginHorizontal();


			foreach(var prefab in PrefabManager.prefabTextures.OrderBy(x => x.Value.width)) {
				
				//Wrap to new row if we exceed screen width
				if(width <= (minimumPreviewSize + (previewPadding*2))) {
					width = this.position.width;
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}

				//subtract button width from total width
				if(prefab.Value != null) {

					width -= (prefab.Value.width + (2 * previewPadding));
				
				if(prefab.Key == PrefabManager.current)
					GUI.enabled = false;

				if(GUILayout.Button (prefab.Value, GUILayout.Width (prefab.Value.width + previewPadding),
				                 				GUILayout.Height (prefab.Value.height + previewPadding)))
				                 				PrefabManager.current = prefab.Key;
				
				GUI.enabled = true;
				}
			}
		
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndScrollView();
		}
		 
	}

}
