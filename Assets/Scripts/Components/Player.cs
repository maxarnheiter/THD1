using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	WalkAnimation walkAnimation;

	public Vector3 last;
	public Vector3 current;
	public Vector3 target;
	
	public bool allowInput = true;

	public float speed = 0.5f;

	void Start () {
	
		
		
		walkAnimation = this.GetComponent<WalkAnimation> ();

		last = this.transform.position;
		current = this.transform.position;
	}

	void Update () {
	
		float step = speed * Time.deltaTime;

		current = this.transform.position;

		if (Input.GetKey (KeyCode.W) && allowInput) 
		{
			walkAnimation.direction = Direction.North;
			target = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
			allowInput = false;
		}
		if (Input.GetKey (KeyCode.A) && allowInput) 
		{
			walkAnimation.direction = Direction.West;
			target = new Vector3 (this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
			allowInput = false;
		}
		if (Input.GetKey (KeyCode.S) && allowInput) 
		{
			walkAnimation.direction = Direction.South;
			target = new Vector3 (this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
			allowInput = false;
		}
		if (Input.GetKey (KeyCode.D) && allowInput) 
		{
			walkAnimation.direction = Direction.East;	
			target = new Vector3 (this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
			allowInput = false;
		}

		if(current != target)
			transform.position = Vector3.MoveTowards(current, target, step);
			
		if (current != last) 
		{
			walkAnimation.isWalking = true;
			walkAnimation.OnPositionChanged();
			allowInput = false;
		}
		else
		{
			if(!Input.GetKey ((KeyCode.D)) && !Input.GetKey ((KeyCode.A)) && !Input.GetKey ((KeyCode.S)) && !Input.GetKey ((KeyCode.W)))
			{
				walkAnimation.isWalking = false;
				walkAnimation.OnPositionChanged();
				allowInput = true;
			}
			else
			{
				walkAnimation.OnPositionChanged();
				allowInput = true;
			}
		}

		last = current;
	}
}
