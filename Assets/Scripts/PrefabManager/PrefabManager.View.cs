using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Collections.Generic;

public static partial class PrefabManager {

	static Vector2 size = Vector2.zero;
	
	public static Prefab GetPrefab(int id) {

		Prefab prefab = null;

		prefabs.TryGetValue (id, out prefab);

		return prefab;
	}

	public static void OnGUI(int screenWidth) {

		int minimumPreviewSize = 32;
		int previewPadding = 12;
		int width = screenWidth;
		bool doOnce = false;

		size = EditorGUILayout.BeginScrollView (size);
		
		EditorGUILayout.BeginHorizontal (GUILayout.MinWidth((float)screenWidth));

		if (hasPrefabs) {

				int setId = 0;
				
				if(prefabs != null)
				foreach (var prefab in prefabs.OrderBy(x => x.Value.setId)) {
						
				if (prefab.Value != null)
				if(PrefabPassesFilters(prefab.Value)) {
					
						if(!doOnce) {
							PrintSetId(0);
							doOnce = true;
						}

						//Wrap if we have a new set id
						if(prefab.Value.setId != setId) {
							width = screenWidth;
							GUILayout.EndHorizontal ();
							GUILayout.BeginHorizontal ();
							PrintSetId(prefab.Value.setId);
							setId = prefab.Value.setId;
						}

						//Wrap to new row if we exceed screen width
						if (width <= (minimumPreviewSize + (previewPadding * 2))) {
								width = screenWidth;
								GUILayout.EndHorizontal ();
								GUILayout.BeginHorizontal ();
								PrintSetId(prefab.Value.setId);
						}
						
								width -= (prefab.Value.width + (2 * previewPadding));
		
								if (prefab.Value == current)
										GUI.enabled = false;
		
								if (GUILayout.Button (prefab.Value.texture, GUILayout.Width (prefab.Value.width + previewPadding),
		                   				  									GUILayout.Height (prefab.Value.height + previewPadding))) {

									if(MapEditor.selectAction == SelectAction.Current) {
										PrefabManager.current = prefab.Value;
										//Selection.activeGameObject = prefab.Value.gameObject;
										Selection.activeGameObject = PrefabUtility.FindPrefabRoot(prefab.Value.gameObject);
									}
									if(MapEditor.selectAction == SelectAction.SetID) {
										prefab.Value.setId = MapEditor.nextSetId;
										Selection.activeGameObject = prefab.Value.gameObject;
										EditorUtility.SetDirty(prefab.Value.gameObject);
									}
								}
		
								GUI.enabled = true;
						}
				}
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndScrollView ();
		}

	public static void PrintSetId(int setId) {

		//int val = (prefab.setId == 0) ? 0 : (prefab.setId - 1);
		GUILayout.Label (setId.ToString(), GUILayout.Width (30f));
	}
	
	static bool PrefabPassesFilters(Prefab prefab)
	{
		if(PrefabManager.prefabType == 0 && PrefabManager.prefabCategory == 0 && PrefabManager.prefabColor == 0)
			return false;
	
		if(PrefabManager.prefabType != 0)
		if(((int)PrefabManager.prefabType & (int)prefab.prefabType) != (int)prefab.prefabType)
			return false;
		
		if(PrefabManager.prefabCategory != 0)
		if(((int)PrefabManager.prefabCategory & (int)prefab.prefabCategory) != (int)prefab.prefabCategory)
			return false;
			
		if(PrefabManager.prefabColor != 0)
		if(((int)PrefabManager.prefabColor & (int)prefab.prefabColor) != (int)prefab.prefabColor)
			return false;
	
	
		return true;
	}

}
