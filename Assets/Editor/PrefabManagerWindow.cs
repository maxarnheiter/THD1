using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;

public class PrefabManagerWindow : EditorWindow {

	static int minimumPreviewSize = 32;
	static int previewPadding = 12;

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
			
			GUILayout.BeginHorizontal();
			
			foreach(var prefab in PrefabManager.prefabTextures) {
				
				//Wrap to new row if we exceed screen width
				if(width <= (minimumPreviewSize + (previewPadding*2))) {
					width = this.position.width;
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}
				
				//subtract button width from total width
				if(prefab.Value == null) { // null check to prevent errors during reload
					Debug.Log ("Could not load texture for next prefab.");
					return;
					}
				else
					width -= (prefab.Value.width + (2 * previewPadding));
				
				if(prefab.Key == PrefabManager.current)
					GUI.enabled = false;

				if(GUILayout.Button (prefab.Value, GUILayout.Width (64+ previewPadding),
				                 				GUILayout.Height (64 + previewPadding)))
				                 				PrefabManager.current = prefab.Key;
				
				GUI.enabled = true;
			}
			GUILayout.EndHorizontal();
		}
		 
	}

}
