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
		
		if (PrefabManager.hasPrefabs) {
		
			var width = this.position.width;
			var count = PrefabManager.prefabTextures.Count;
			
			GUILayout.BeginHorizontal();
			
			for(int i = 0; i < count; i++) {
				var prefab = PrefabManager.prefabTextures.ElementAt(i);
				
				//Wrap to new row if we exceed screen width
				if(width <= (minimumPreviewSize + (previewPadding*2))) {
					width = this.position.width;
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}
				
				//subtract button width from total width
				width -= (prefab.Value.width + (2 * previewPadding));
				
				if(prefab.Key == PrefabManager.current)
					GUI.enabled = false;
				
				if(GUILayout.Button (prefab.Value, GUILayout.Width (prefab.Value.width + previewPadding),
				                 				GUILayout.Height (prefab.Value.height + previewPadding)))
				                 				PrefabManager.current = prefab.Key;
				
				GUI.enabled = true;
				
				//End Horizontal if we're on the last one
				if(i == count)
					GUILayout.EndHorizontal();
			}
		}
		 
		
		
	}

}
