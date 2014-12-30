using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{

	public static Vector3 RoundXY(this Vector3 position)
	{
		float x = Mathf.Round (position.x);
		float y = Mathf.Round (position.y);
		float z = position.z;

		return new Vector3(x,y,z);
	}
	
	public static bool IsAbove(this Vector3 position, Vector3 target)
	{
		if(position.z < target.z)
			return true;
			
		return false;
	}
	
	public static bool IsBelow(this Vector3 position, Vector3 target)
	{
		if(position.z > target.z)
			return true;
		
		return false;
	}
	
	public static bool IsSameHeight(this Vector3 position, Vector3 target)
	{
		if(position.z == target.z)
			return true;
			
		return false;
	}
	

}
