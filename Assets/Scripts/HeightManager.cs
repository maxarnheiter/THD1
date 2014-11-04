using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public static class HeightManager 
{
	static float searchBuffer = 2f;

	public static void UpdateObjectsForCamera(Camera camera)
	{
		/*
		float cameraHeight = camera.orthographicSize;
		float cameraWidth = camera.orthographicSize * (camera.pixelWidth / camera.pixelHeight);
		
		Rect cameraRect = new Rect(	(camera.transform.position.x - cameraWidth - searchBuffer),
									(camera.transform.position.y + cameraHeight + searchBuffer), 
									((cameraWidth + searchBuffer) * 2f), 
									((cameraHeight + searchBuffer) * 2f)
									);
		
		Get visible objects from instance manager
		Sort them by their correct render order
		Set the order in layer property
		*/
	}
	
	static bool ObjectVisibleToCamera(Camera camera, Renderer renderer)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}

}
