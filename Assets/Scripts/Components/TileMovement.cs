using UnityEngine;
using System.Collections;

public class TileMovement : MonoBehaviour {

	CreatureAnimation creatureAnimation;

	Vector3 current;
	Vector3 last;
	Vector3 start;
	Vector3 target;
	
	bool isMoving;
	bool hasTarget;

	float step;
	public float speed = 1f;

	void Start () 
	{
		creatureAnimation = gameObject.GetComponent<CreatureAnimation> ();

		current = transform.position;
		last = transform.position;
	}

	void FixedUpdate () 
	{
		step = speed * Time.deltaTime;
		current = transform.position;

		if(current != last)
		{
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}

		if (current != target) 
		{
			DoStep();
		}

		if (current == target) 
		{
			OnTargetReached();
		}
		
		last = current;

	}

	public void Move(Direction direction)
	{
		if(!isMoving)
		{
			creatureAnimation.SetDirection(direction, true);
		
			start = current.RoundXY();
		
			float xChange = 0f;
			float yChange = 0f;
	
			switch (direction) 
			{
				case Direction.North:
					yChange = 1f;
				break;
				case Direction.East:
					xChange = 1f;
				break;
				case Direction.South:
					yChange = -1f;
				break;
				case Direction.West:
					xChange = -1f;
				break;
			}
			
			target = new Vector3 (start.x + xChange, start.y + yChange, start.z);
			hasTarget = true; 
		}
	}

	public void ChangeDirection(Direction direction)
	{
		creatureAnimation.SetDirection(direction, false);
	}

	void DoStep()
	{
		if(hasTarget)
		{
			transform.position = Vector3.MoveTowards(current, target, step);
			creatureAnimation.AddDistance(step);
		}
	}

	void OnTargetReached()
	{
		if(hasTarget)
		{
			hasTarget = false;
			transform.position = target;
			creatureAnimation.Stop();
		}
	}

}
