using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public static class InstanceManager {

	static Dictionary<int, Transform> _instances;
	
	public static Dictionary<int, Transform> instances {
		get { return _instances ?? (_instances =  new Dictionary<int, Transform>()); }
	}
	
	public static bool hasInstances {
		get {
				if(instances.Count > 0)
					return true;
				return false;
		}
	}
	
	public static void Clear() {
		
		foreach(var instance in _instances) {
			Object.DestroyImmediate(instance.Value.gameObject);
		}
		_instances =  new Dictionary<int, Transform>();
	}
	
	public static void Instantiate(int prefabId, Vector3 position) {
		if(prefabId == 0)
			return;
		
		var newObject = PrefabUtility.InstantiatePrefab(PrefabManager.currentObject) as GameObject;
		newObject.transform.position = position;
		newObject.transform.Rotate(Config.DEFAULT_ROTATION);
		newObject.transform.parent = MapEditor.mapContainer.transform;
		instances.Add(newObject.GetInstanceID(), newObject.transform);
	}
	
}
