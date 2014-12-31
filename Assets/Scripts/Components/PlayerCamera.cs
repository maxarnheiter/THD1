using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
			var currentPos = this.transform.position;
			
			var checkPos = currentPos.South();
			
			Rect r = new Rect(checkPos.x, checkPos.y, 1f, 1f);
			
			DebugExtensions.DrawRect(r, -8f);
			
			var result = InstanceManager.instances.Within(r).Where (p => p.Value.transform.position.z < currentPos.z);
			
			if(result.Count() > 0)
			{
				bool isCovered = false;
				float coverHeight = -100f;
				
				foreach(var res in result)
				{
					Vector3 compare = new Vector3(currentPos.x, currentPos.y, res.Value.transform.position.z);
					if(res.Value.spriteRenderer.bounds.Contains(compare))
					{
						isCovered = true;
						if(compare.z > coverHeight)
							coverHeight = compare.z;
					}
				}
				
				if(isCovered)
				{
					isCovered = false;
					FloorRenderer.SetVisibleFloors(coverHeight+1, true);
					coverHeight = -100f;
				}
			}	
			else
			{
				FloorRenderer.SetVisibleFloors(refreshHeight, true);
			}
		}
		
	}
	

}
