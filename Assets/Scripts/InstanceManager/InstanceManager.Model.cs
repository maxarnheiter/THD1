using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static partial class InstanceManager {

	static Dictionary<int, Instance> _instances;
	
	public static Dictionary<int, Instance> instances {
		get { return _instances ?? (_instances =  new Dictionary<int, Instance>()); }
	}
	
	static void Add(int instanceId, Instance instance) {
		instances.Add(instanceId, instance);
	}
	
	static void Remove(int instanceId) {
		instances.Remove(instanceId);
	}
}
