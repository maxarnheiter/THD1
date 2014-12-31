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
	
	public static void DrawRect(Rect r, float height)
	{ 
		var point1 = new Vector3(r.xMin, r.yMin, height);
		var point2 = new Vector3(r.xMax, r.yMin, height);
		var point3 = new Vector3(r.xMax, r.yMax, height);
		var point4 = new Vector3(r.xMin, r.yMax, height);
		
		Debug.DrawLine (point1,point2);
		Debug.DrawLine (point2,point3);
		Debug.DrawLine (point3,point4);
		Debug.DrawLine (point4,point1);
	}
	
}
