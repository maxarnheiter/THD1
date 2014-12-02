using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class FloorRenderer  {

	public static void SetVisibleFloors(float floorHeight) {

		foreach (var i in InstanceManager.instances) {

			var pos = i.Value.transform.position;
			if(pos.z < floorHeight) {
				i.Value.spriteRenderer.enabled = false;
			}
			if(pos.z == floorHeight) {
				i.Value.spriteRenderer.enabled = true;
				if(i.Value.spriteRenderer.color != Color.white)
					i.Value.spriteRenderer.color = Color.white;
			}
			if(pos.z > floorHeight) {
				i.Value.spriteRenderer.enabled = true;
				if(i.Value.spriteRenderer.color != Config.EDITOR_FLOOR_COLOR)
					i.Value.spriteRenderer.color = Config.EDITOR_FLOOR_COLOR;
			}
		}
	}
}
