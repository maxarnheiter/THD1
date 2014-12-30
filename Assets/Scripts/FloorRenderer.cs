using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class FloorRenderer  
{

// Public Controls

	public static void SetVisibleFloors(float floorHeight, bool fullVisibility) 
	{

		foreach (var i in InstanceManager.instances) 
		{
			var pos = i.Value.transform.position;
			
			if(pos.z < floorHeight)			//Above
			{
				SetVisibility(i.Value, false, false);
			}
			else if(pos.z == floorHeight) 	//Same
			{
				SetVisibility(i.Value, true, true);
			}
			else if(pos.z > floorHeight) 	//Below
			{
				SetVisibility(i.Value, true, fullVisibility);
			}
		}
	}
	
// Private Controls 

	static void SetVisibility(Instance instance, bool isEnabled, bool fullVisibility)
	{
		if(!isEnabled)
		{
			instance.spriteRenderer.enabled = false;
		}
		else
		{
			instance.spriteRenderer.enabled = true;
		
			if(fullVisibility)
			{
				CheckAndSetColor(instance.spriteRenderer, Color.white);
			}
			else
			{
				CheckAndSetColor(instance.spriteRenderer, Config.EDITOR_FLOOR_COLOR);
			}
		}
	}
	
	static void CheckAndSetColor(SpriteRenderer renderer, Color color)
	{
		if(renderer.color != color)
			renderer.color = color;
	}
	
	
	
}
