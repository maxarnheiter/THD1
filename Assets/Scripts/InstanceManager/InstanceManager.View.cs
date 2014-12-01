using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static partial class InstanceManager  {

	//GetAllFromPosition
	public static IEnumerable<KeyValuePair<int, Instance>> GetAllFromPosition(Vector3 position) {

		return InstanceManager.instances.Where (x => x.Value.transform.position == position);
	}

	public static IEnumerable<KeyValuePair<int, Instance>> Sort(this IEnumerable<KeyValuePair<int, Instance>> objs) {
		
		return objs.OrderByDescending(x => x.Value.transform.position.z)	
				.ThenBy(x => TagManager.TagToInt(x.Value.gameObject.tag))
				.ThenByDescending(x => x.Value.transform.position.y)			
				.ThenBy(x => x.Value.transform.position.x)				
				.ThenBy(x => x.Value.stack.uid);						
	}

	public static IEnumerable<KeyValuePair<int, Instance>> Within(this IEnumerable<KeyValuePair<int, Instance>> objs, Rect rect) {

		return objs.Where 	(x => (x.Value.transform.position.x >= rect.xMin) &&
							(x.Value.transform.position.x <= rect.xMax) &&
							(x.Value.transform.position.y >= rect.yMin) &&
							(x.Value.transform.position.y <= rect.yMax));
	}

	public static IEnumerable<KeyValuePair<int, Instance>> Without(this IEnumerable<KeyValuePair<int, Instance>> objs, string tag) {

		return objs.Where (x => x.Value.gameObject.tag != tag);
	}
	
}
