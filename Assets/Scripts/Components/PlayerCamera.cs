using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	GameObject playerCam;
	Camera cam;
	
	void Start () {
	
		playerCam = GameObject.Find("Player Camera");
		if(playerCam != null)
		{
			playerCam.transform.position = this.transform.position;
			cam = playerCam.GetComponent<Camera>();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
		playerCam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
		StackRenderer.UpdateCameraObjects(cam);
	
	}
}
