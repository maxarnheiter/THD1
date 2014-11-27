using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static partial class InstanceManager  {

	//GetAllFromPosition
	public static IEnumerable<KeyValuePair<int, Instance>> GetAllFromPosition(Vector3 position) {
		return InstanceManager.instances.Where (x => x.Value.transform.position == position);
	}
	
	//GetAllWithinRect
	//We remove ground tiles because this is our function that colaborates with our stack renderer
	public static IEnumerable<KeyValuePair<int, Instance>> GetAllWithinRectWithoutGround(Rect rect) {
		return InstanceManager.instances.Where (x => (x.Value.transform.position.x >= rect.xMin) &&
													(x.Value.transform.position.x <= rect.xMax) &&
													(x.Value.transform.position.y >= rect.yMin) &&
													(x.Value.transform.position.y <= rect.yMax) &&
													(x.Value.gameObject.tag != "ground tile"));
	}
	
	
}
