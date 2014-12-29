using UnityEngine;
using System.Collections.Generic;

public class CreatureAnimation : MonoBehaviour {

	public float switchDistance = 1f;
	public float currentDistance = 0f;

	DirectionalAnimation current = null;

	public DirectionalAnimation north = null;
	public DirectionalAnimation south = null;
	public DirectionalAnimation east = null;
	public DirectionalAnimation west = null;

	SpriteRenderer renderer;

	public CreatureAnimation()
	{
		north = new DirectionalAnimation (Direction.North);
		south = new DirectionalAnimation (Direction.South);
		east = new DirectionalAnimation (Direction.East);
		west = new DirectionalAnimation (Direction.West);
	}

	void Start () 
	{
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void FixedUpdate () 
	{
			
	}

	public void AddDistance(float distance)
	{
		currentDistance += distance;

		if (currentDistance >= switchDistance)
			current.SwitchFeet (renderer);

	}

	public void ChangeDirection(Direction direction)
	{
		switch (direction) 
		{
			case Direction.North:
				current = north;
			break;
			case Direction.South:
				current = south;
			break;
			case Direction.East:
				current = east;
			break;
			case Direction.West:
				current = west;
			break;
		}
	}


}
