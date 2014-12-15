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
	
	public static void Instantiate(Prefab prefab, Vector3 position) {
		
		if(prefab == null)
			return;
		else {
			var newObject = PrefabUtility.InstantiatePrefab (prefab.gameObject) as GameObject;
			var transform = newObject.transform;
			var stack = newObject.GetComponent<Stack> ();

			transform.position = position;
			transform.Rotate (Config.DEFAULT_ROTATION);
			transform.parent = MapEditor.mapContainer.transform;

			if (transform.position.z != 0)
					newObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Floor " + transform.position.z.ToString ();

			stack.Start ();

			Add (newObject.GetInstanceID (), new Instance (newObject));

			MapEditorRules.OnInstantiate(newObject);
		}

	}

	public static void Destroy(int id) {

		if (id == 0)
			return;

		GameObject.DestroyImmediate (instances [id].gameObject);
		Remove (id);
	}
}
