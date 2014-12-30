using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	Camera playerCamera;
	float distance = 10f;
	
	bool needsRefresh;
	float refreshHeight;
	
	float xOffset = -0.5f;
	float yOffset = 0.5f;
	
	void Start () 
	{
		FindPlayerCamera();
		AlignCameraToPlayer();
		
		needsRefresh = true;
		refreshHeight = -100f;
	}

	void Update () 
	{
		AlignCameraToPlayer();
		
		CheckIfNeedsRefresh();
		
		CheckIfUnderSomething();
		
		UpdatePlayerView();
	}
	
// Private Controls

	void UpdatePlayerView()
	{
		if(playerCamera != null)
			StackRenderer.UpdateCameraObjects(playerCamera);
	}

	void FindPlayerCamera()
	{
		var obj = GameObject.Find("Player Camera");
		
		if(obj != null)
			playerCamera = obj.GetComponent<Camera>();
	}
	
	void AlignCameraToPlayer()
	{
		if(playerCamera != null)
		{
			playerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (distance * -1f));
		}
	}
	
	void CheckIfNeedsRefresh()
	{
		if(needsRefresh)
		{
			if(InstanceManager.instances.Count > 0)
			{
				FloorRenderer.SetVisibleFloors(refreshHeight, true);
				needsRefresh = false;
			}
		}
	}
	
	void CheckIfUnderSomething()
	{
		if(playerCamera != null)
		{
			/*
			var currentPosition = this.transform.position;
		
			var start = 	new Vector2(playerCamera.transform.position.x + xOffset, 	playerCamera.transform.position.y + yOffset);
			var end = 		new Vector2(currentPosition.x + xOffset, 					currentPosition.y + yOffset);
			
			

			RaycastHit2D hit = Physics2D.Raycast(start, end);
			
			
			
			if(hit.collider != null)
			{
			
				Vector3 hitPosition = hit.collider.transform.position;
				float hitHeight = hitPosition.z;
				
				if(hitPosition.IsAbove(currentPosition))
				{
					DebugExtensions.DrawX(new Vector3(hit.centroid.x, hit.centroid.y, -10f), 0.1f);
					Debug.Log ("Found something above us on floor with height: " + hitHeight);
					//float hideHeight = hitHeight + 1f;
					//FloorRenderer.SetVisibleFloors(hideHeight, true);
					hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
				}
					
				if(hitPosition.IsSameHeight(currentPosition))
				{
					Debug.Log ("There is nothing above us.");
					FloorRenderer.SetVisibleFloors(refreshHeight, true);
				}
			}

			*/
			
		}
		
	}
	

}
