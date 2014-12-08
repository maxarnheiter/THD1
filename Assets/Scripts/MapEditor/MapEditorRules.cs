using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class MapEditorRules {

	public static void OnInstantiate(GameObject obj) {

		if (obj.tag == "ground tile") {
			CheckDuplicateGround(obj);
		}
	}

	static void CheckDuplicateGround(GameObject obj) {

		var current = InstanceManager.GetAllFromPosition (obj.transform.position).With ("ground tile");

		if (current.Count() > 1) {

			var old = current.OrderBy (x => x.Value.stack.uid).FirstOrDefault ();

			InstanceManager.Destroy (old.Key);
		}

	}
}
