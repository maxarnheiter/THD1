using UnityEngine;
using System.Collections.Generic;

public static partial class PrefabManager {

	static PrefabType _prefabType;
	public static PrefabType prefabType
	{
		get { return _prefabType; }
		set
		{
			if(value != _prefabType) 
			{
			_prefabType = value;
			_prefabCategory = 0;
			_prefabColor = 0;
			Debug.Log ("on prefab type change");
			}
		}
	}

	public static bool filterByCategory;
	static PrefabCategory _prefabCategory;
	public static PrefabCategory prefabCategory
	{
		get { return _prefabCategory; }
		set
		{
			if(value != _prefabCategory) 
			{
			_prefabCategory = value;
			_prefabType = 0;
			_prefabColor = 0;
			Debug.Log ("on prefab category change");
			}
		}
	}
	
	public static bool filterByColor;
	static PrefabColor _prefabColor;
	public static PrefabColor prefabColor
	{
		get { return _prefabColor; }
		set
		{
			if(value != _prefabColor)
			{
			_prefabColor = value;
			_prefabCategory = 0;
			_prefabType = 0;
			Debug.Log ("on prefab color change");
			}
		}
	}

	static Prefab _current;
	public static Prefab current {
		get { return _current; }
		set { 	_current =  value; 
			MapEditor.action = ClickAction.Add;
		}
	}

	static Dictionary<int, Prefab> _prefabs;

	public static Dictionary<int, Prefab> prefabs {
		get { return _prefabs ?? (_prefabs = new Dictionary<int, Prefab>()); }
	}
	
	static int _count;
	public static int count {
		get { return _count; }
	}
	
	static int _nextId;
	public static int nextId {
		get { return _nextId; }
	}
	
	static bool _hasPrefabs;
	public static bool hasPrefabs {
		get { return _hasPrefabs; }
	}
	
	static bool _triedToLoad;
	
	public static void TryLoadOnce() {
		if(!_triedToLoad) {
			Load ();
			_triedToLoad = true;
		}
	}
}
