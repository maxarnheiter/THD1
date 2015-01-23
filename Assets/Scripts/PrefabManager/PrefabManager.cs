using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class PrefabManager 
{
	static PrefabCollection _prefabCollection;
	public static PrefabCollection prefabCollection
	{ get { return _prefabCollection; } }
	
	static bool _hasPrefabs;
	public static bool hasPrefabs
	{ get { return _hasPrefabs; } }
	
	public static PrefabType prefabType = PrefabType.Any;
	public static PrefabCategory prefabCategory = PrefabCategory.Any;
	public static PrefabColor prefabColor = PrefabColor.Any;
	
	static Prefab _current;
	public static Prefab current {
		get { return _current; }
		set { 	_current =  value; 
			MapEditor.action = ClickAction.Add;
		}
	}

	public static bool Load() 
	{
		_prefabCollection = PrefabLoader.GetPrefabCollection();
		
		if(prefabCollection != null)
		{
			_hasPrefabs = true;
			return true;
		}

		_hasPrefabs = false;
		Debug.Log ("Failed to load a prefab collection.");
		return false;
	}
	
	public static int GetNextPrefabId()
	{
		return prefabCollection.prefabs.Max(p => p.Value.id) + 1;
	}
}
