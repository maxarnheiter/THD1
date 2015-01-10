using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

	public Direction direction;
	public bool isDown;

	void Start () 
	{
		if(this.isDown)
		{
			Debug.Log ("im down");
			var renderer = this.gameObject.GetComponent<SpriteRenderer>();
			renderer.sprite = null;
		}
	}
	
	void Update () 
	{
	
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Vector3 targetPosition = transform.position;
		
		GameObject obj = collider.gameObject;
		TileMovement tileMovement = obj.GetComponent<TileMovement>();
		
		tileMovement.Lock();
		tileMovement.Stop();
		
		if(obj.tag == "player")
		{
			switch(direction)
			{
				case Direction.North:
					targetPosition = targetPosition.North();
				break;
				case Direction.South:
					targetPosition = targetPosition.South();
				break;
				case Direction.East:
					targetPosition = targetPosition.East();
				break;
				case Direction.West:
					targetPosition = targetPosition.West();
				break;
			}
			
			if(isDown)
				targetPosition = targetPosition.FloorDown();
			else
				targetPosition = targetPosition.FloorUp();
			
			obj.layer = LayerMask.NameToLayer("Floor " + targetPosition.z.ToString());
			
			obj.transform.position = targetPosition;
			
			tileMovement.Unlock();
		}
	}
}
