using UnityEngine;
using System.Collections.Generic;

public class CreatureAnimation : MonoBehaviour {

	//publics
	public float animationSpeed;
	public float movementSpeed;
	
	public DirectionalAnimation north = null;
	public DirectionalAnimation south = null;
	public DirectionalAnimation east = null;
	public DirectionalAnimation west = null;
	
	//privates
	DirectionalAnimation current = null;
	SpriteRenderer renderer;
	bool isMoving;
	
	bool delayedStop;
	float delayedStopCount = 0f;
	float delayedStopMax = 10f;

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
		
		current = south;
	}

	void Update () 
	{
		CheckDelayedStop();
		current.Animate(renderer, isMoving, GetAdjustedSpeed());
	}
	
//Public Controls
	
	public void SetDirection(Direction direction)
	{
		switch(direction)
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
	
	public void SetMoving(bool moving)
	{
		if(moving)
		{
			isMoving = true;
			ResetDelayedStop();
		}
		else
		{
			delayedStop = true;
		}
	}
	
//Private Controls
	
	float GetAdjustedSpeed()
	{
		if(movementSpeed <= 0f)
			return animationSpeed;
		else
		{
			float adjustedSpeed = animationSpeed / movementSpeed;
			return adjustedSpeed;
		}
	}
	
	void CheckDelayedStop()
	{
		if(delayedStop)
		{
			delayedStopCount++;
			if(delayedStopCount >= delayedStopMax)
			{
				delayedStop = false;
				delayedStopCount = 0;
				isMoving = false;
			}
		}
	}
	
	void ResetDelayedStop()
	{
		delayedStop = false;
		delayedStopCount = 0;
	}



}
