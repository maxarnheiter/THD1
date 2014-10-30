using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class InstanceManager {

	static Dictionary<int, Transform> _instances;
	public static Dictionary<int, Transform> instances {
		get { return _instances; }
	}
	
	public static void Clear() {
		
		foreach(var instance in _instances) {
			Object.DestroyImmediate(instance.Value.gameObject);
		}
		_instances =  new Dictionary<int, Transform>();
	}
	
}
