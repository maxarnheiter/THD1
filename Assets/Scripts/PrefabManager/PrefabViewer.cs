using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class PrefabViewer
{
	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;

	public static void Display(int displayWidth)
	{
		if(!PrefabManager.hasPrefabs)
			return;
		
		size = EditorGUILayout.BeginScrollView(size);

		foreach(var setGrouping in PrefabManager.prefabCollection.prefabs.GroupBy(p => p.Value.setId))
		{
			DisplaySet(setGrouping, displayWidth);
		}
		
		EditorGUILayout.EndScrollView();
	}
	
	static void DisplaySet(IGrouping<int, KeyValuePair<int, Prefab>> setGrouping, int displayedWidth)
	{
		int setId = setGrouping.First().Value.setId;
		int currentWidth = 0;
		
		//Print a label for our set id
		GUILayout.Label ("Set ID: " + setId);
		
		//Loop through all prefabs in this set
		GUILayout.BeginHorizontal();
		foreach(var kvp in setGrouping)
		{
			Prefab prefab = kvp.Value;
			
			//Check if the prefab passes our filters
			if(!IsFiltered(prefab))
			{
				if(currentWidth >= displayedWidth)
				{
					currentWidth = 0;
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
				}
				
				currentWidth = currentWidth + prefab.width + (2 * buttonPadding);
				
				DisplayPrefab(prefab);
			}
		}
		GUILayout.EndHorizontal();	}
	
	static bool IsFiltered(Prefab prefab)
	{
		if (PrefabManager.prefabType != PrefabType.Any && prefab.prefabType != PrefabManager.prefabType)
			return true;
		
		if (PrefabManager.prefabCategory != PrefabCategory.Any && prefab.prefabCategory != PrefabManager.prefabCategory)
			return true;
		
		if (PrefabManager.prefabColor != PrefabColor.Any && prefab.prefabColor != PrefabManager.prefabColor)
			return true;
		
		return false;
	}
	
	static void DisplayPrefab(Prefab prefab)
	{
		if(PrefabManager.current == prefab)
			GUI.enabled = false;
		
		var displayTexture = PrefabManager.prefabCollection.GetTexture(prefab.spriteName);
		
		if(GUILayout.Button (displayTexture, GUILayout.Width (prefab.width + buttonPadding), GUILayout.Height(prefab.height + buttonPadding)))
			OnPrefabButtonPressed(prefab);
			
		GUI.enabled = true;
	}
	
	static void OnPrefabButtonPressed(Prefab prefab)
	{
		switch(MapEditor.selectAction)
		{
			case SelectAction.Current:
			{
				PrefabManager.current = prefab;
				Selection.activeGameObject = PrefabUtility.FindPrefabRoot(prefab.gameObject);
				break;
			}
			case SelectAction.SetID:
			{
				prefab.setId = MapEditor.nextSetId;
				Selection.activeGameObject = prefab.gameObject;
				EditorUtility.SetDirty(prefab.gameObject);
				break;
			}
			case SelectAction.SetCategory:
			{
				prefab.prefabCategory = MapEditor.nextCategory;
				Selection.activeGameObject = prefab.gameObject;
				EditorUtility.SetDirty(prefab.gameObject);
				break;
			}
			case SelectAction.SetColor:
			{
				prefab.prefabColor = MapEditor.nextColor;
				Selection.activeGameObject = prefab.gameObject;
				EditorUtility.SetDirty(prefab.gameObject);
				break;
			}
		}
	}
	
}
