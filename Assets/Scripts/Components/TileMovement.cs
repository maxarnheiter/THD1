using UnityEngine;
using System.Collections;

public class TileMovement : MonoBehaviour {

	CreatureAnimation creatureAnimation;

	Vector3 current;
	Vector3 last;
	Vector3 target;

	float step;
	float speed = 1f;

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

		if (current != target) 
		{
			DoStep();
		}

		if (current == target) 
		{
			OnTargetReached();
		}

	}

	public void Move(Direction direction)
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

		Vector3 rounded = current.RoundXY();
		target = new Vector3 (rounded.x + xChange, rounded.y + yChange, rounded.z);
	}

	public void ChangeDirection(Direction direction)
	{

	}

	void DoStep()
	{
		transform.position = Vector3.MoveTowards(current, target, step);
		//send step info to animation
	}

	void OnTargetReached()
	{

	}

}
