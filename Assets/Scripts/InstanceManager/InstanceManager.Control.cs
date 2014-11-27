using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public static partial class InstanceManager {
	
	public static bool hasInstances {
		get {
			if(instances.Count > 0)
				return true;
			return false;
		}
	}
	
	public static void Clear() {
		
		foreach(var instance in instances) {
			Object.DestroyImmediate(instance.Value.gameObject);
		}
		_instances =  new Dictionary<int, Instance>();
	}
	
	
	public static void ImportFrom(GameObject obj) {
		
		foreach(Transform child in obj.transform) {
			Add (child.gameObject.GetInstanceID(), new Instance(child.gameObject));
		}
	}
	
	
	public static void Instantiate(int prefabId, Vector3 position) {
		if(prefabId == 0)
			return;
		
		var newObject = PrefabUtility.InstantiatePrefab(PrefabManager.currentObject) as GameObject;
		var transform = newObject.transform;
		var stack = newObject.GetComponent<Stack>();
		
		transform.position = position;
		transform.Rotate(Config.DEFAULT_ROTATION);
		transform.parent = MapEditor.mapContainer.transform;
		
		stack.Start();
		
		Add(newObject.GetInstanceID(), new Instance(newObject));
	}
	
}
