using UnityEngine;
using System.Collections;

public static class DebugExtensions
{

	public static void DrawX(Vector3 position, float width)
	{
		var start1 = new Vector3(position.x - width, position.y + width, position.z);
		var end1 = new Vector3(position.x + width, position.y - width, position.z);
		
		var start2 = new Vector3(position.x - width, position.y - width, position.z);
		var end2 = new Vector3(position.x + width, position.y + width, position.z);
		
		Debug.DrawLine(start1, end1);
		Debug.DrawLine(start2, end2);
	}
	
}
