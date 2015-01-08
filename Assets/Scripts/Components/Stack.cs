using UnityEngine;
using System.Collections;

public class Stack : MonoBehaviour {

	public int uid;
	public Vector3 lastPosition;
	
	SpriteRenderer renderer;
	
	public void Start () {
		CheckForPositionChange();
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	public void Update () {
		CheckForPositionChange();
	}
	
	void CheckForPositionChange() {
		
		if(lastPosition != this.transform.position) {
			GetNewStackUID();
			UpdateSpriteLayer();
		}
		lastPosition = this.transform.position;
	}
	
	void GetNewStackUID() {
		this.uid = StackManager.nextUID;
	}
	
	void UpdateSpriteLayer()
	{
		if(renderer != null)
			renderer.sortingLayerName = "Floor " + this.transform.position.z.ToString();
	}
}
