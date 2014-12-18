using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public static partial class PrefabManager {

	static Vector2 size = Vector2.zero;

	static bool _showGrounds;
	public static bool showGrounds { 
		get { return _showGrounds; }
		set {
				_showGrounds = value;
				SetFilteredPrefabs();
			}
		}
	static bool _showCorners;
	public static bool showCorners {
		get { return _showCorners; }
		set {
			_showCorners = value;
			SetFilteredPrefabs(); 
		}
	}

	static bool _showThings;
	public static bool showThings {
		get { return _showThings; }
		set {
			_showThings = value;
			SetFilteredPrefabs();
		}
	}

	static IEnumerable<KeyValuePair<int, Prefab>> _filteredPrefabs;
	static IEnumerable<KeyValuePair<int, Prefab>> filteredPrefabs {
		get { return _filteredPrefabs ?? (_filteredPrefabs = GetFilteredPrefabs()); }
	}
	
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
				
				if(filteredPrefabs != null)
				foreach (var prefab in filteredPrefabs.OrderBy(x => x.Value.setId)) {
						
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
	
						//subtract button width from total width
						if (prefab.Value != null) {
		
								width -= (prefab.Value.width + (2 * previewPadding));
		
								if (prefab.Value == current)
										GUI.enabled = false;
		
								if (GUILayout.Button (prefab.Value.texture, GUILayout.Width (prefab.Value.width + previewPadding),
		                   				  									GUILayout.Height (prefab.Value.height + previewPadding))) {

									if(MapEditor.selectAction == SelectAction.Current) {
										PrefabManager.current = prefab.Value;
										Selection.activeGameObject = prefab.Value.gameObject;
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

	public static void SetFilteredPrefabs() {

		_filteredPrefabs = GetFilteredPrefabs ();
	}

	public static IEnumerable<KeyValuePair<int, Prefab>> GetFilteredPrefabs() {

		_filteredPrefabs = Enumerable.Empty<KeyValuePair<int, Prefab>> ();

		if (showGrounds)
			_filteredPrefabs = Enumerable.Union (_filteredPrefabs, PrefabManager.prefabs.Where (p => p.Value.gameObject.tag == "ground tile"));
		if (showCorners)
			_filteredPrefabs = Enumerable.Union(_filteredPrefabs, PrefabManager.prefabs.Where (p => p.Value.gameObject.tag == "ground corner"));
		if (showThings)
			_filteredPrefabs = Enumerable.Union(_filteredPrefabs, PrefabManager.prefabs.Where (p => p.Value.gameObject.tag == "thing"));

		return _filteredPrefabs;

	}
}
