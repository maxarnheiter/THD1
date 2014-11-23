using UnityEngine;
using System.Collections;

public class Stack : MonoBehaviour {

	public int uid;
	
	public Vector3 lastPosition;
	
	public void Start () {
		CheckForPositionChange();
	}
	
	public void Update () {
		CheckForPositionChange();
	}
	
	void CheckForPositionChange() {
		
		if(lastPosition != this.transform.position) {
			GetNewStackUID();
		}
		lastPosition = this.transform.position;
	}
	
	void GetNewStackUID() {
		this.uid = StackManager.nextUID;
	}
}
