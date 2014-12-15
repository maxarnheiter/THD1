using UnityEngine;
using System.Collections.Generic;

public static partial class PrefabManager {

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
