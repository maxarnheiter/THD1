﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class StackRenderer  {

	static float searchBuffer = 2f;
	
	public static void UpdateCameraObjects(Camera camera)
	{
		//Get the things visible to the camera
		var cameraRect = GetVisibleRect(camera);
		
		//Get all the things within that rect
		var visibleObjects = InstanceManager.instances.Within(cameraRect).Without("ground tile").Sort();

		//Set the sorting order to all objects within the rect, according to our rules
		SetSortingOrder(visibleObjects);
	}
	
	static Rect GetVisibleRect(Camera camera) {
	
		float camRatio = camera.pixelWidth / camera.pixelHeight;
		float halfCamHeight = camera.orthographicSize;
		float halfCamWidth = halfCamHeight * camRatio;
		
		return new Rect( (camera.transform.position.x - halfCamWidth - searchBuffer),
		                           (camera.transform.position.y - halfCamHeight - searchBuffer),
		                           ((halfCamWidth + searchBuffer) * 2f),
		                           ((halfCamHeight + searchBuffer) * 2f));
	}
	
	static void SetSortingOrder(IEnumerable<KeyValuePair<int, Instance>> objects) {
	
		int c_count = 0;
		int t_count = 0;
		
		foreach(var obj in objects) {
			
			switch (TagManager.TagToInt(obj.Value.gameObject.tag)) {
				
				case 0: //ground tile
				break;
				case 1: //ground corner
					obj.Value.spriteRenderer.sortingOrder = Config.CORNER_SORT_BASE + c_count;
					c_count++;
				break;
				case 2: //thing or player
					obj.Value.spriteRenderer.sortingOrder = Config.THING_SORT_BASE + t_count;
					t_count++;
				break;
			}
		}
	}
	

}