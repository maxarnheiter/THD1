using UnityEngine;
using System.Collections;

public class TileMovement : MonoBehaviour {

	//publics
	
	[SerializeField] float _speed;
	public float speed {
		get { return _speed; }
		set {
			if(value != _speed) {
				_speed = value;
				OnSpeedChange();
			}
		}
	}

	//privates
	CreatureAnimation creatureAnimation;

	Vector3 current;
	Vector3 last;
	Vector3 start;

	bool isMoving;
	
	Vector3 target;
	bool hasTarget;

	void Start () 
	{
		creatureAnimation = gameObject.GetComponent<CreatureAnimation> ();
		creatureAnimation.movementSpeed = speed;

		current = transform.position;
		last = transform.position;
	}

	void FixedUpdate () 
	{
		float step = speed * Time.deltaTime;
		current = transform.position;

		if(current != last)
			OnMoving();
		else
			OnStopped();
		
		
		if(hasTarget)
		if(current != target)
		{
			Walk(step);
		}

		if (current == target) 
		{
			OnTargetReached();
		}
		
		last = current;

	}
	
//Public Controls

	public void Move(Direction direction)
	{
		if(!isMoving)
		{
			OnDirectionChange(direction);
			
			start = current.RoundXY();
			target = GetAdjustedPosition(start, direction);
			
			hasTarget = true; 
		}
	}

	public void ChangeDirection(Direction direction)
	{
		OnDirectionChange(direction);
	}
	
//Private Controls

	Vector3 GetAdjustedPosition(Vector3 startPosition, Direction direction)
	{
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
		
		return new Vector3 (startPosition.x + xChange, startPosition.y + yChange, startPosition.z);
	}
	
	void Walk(float distance)
	{
		transform.position = Vector3.MoveTowards(current, target, distance);
	}
	
	void RoundCurrentPosition()
	{
		transform.position = transform.position.RoundXY();
	}
	
//Private 'Events'
	
	void OnSpeedChange()
	{
		creatureAnimation.movementSpeed = speed;
	}
	
	void OnDirectionChange(Direction direction)
	{
		creatureAnimation.SetDirection(direction);
	}
	
	void OnMoving()
	{
		isMoving = true;
		creatureAnimation.SetMoving(isMoving);
	}
	
	void OnStopped()
	{
		isMoving = false;
		creatureAnimation.SetMoving(isMoving);
	}

	void OnTargetReached()
	{
		if(hasTarget)
		{
			hasTarget = false;
			RoundCurrentPosition();
		}
	}

}
