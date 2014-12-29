using UnityEngine;
using System.Collections.Generic;

public class CreatureAnimation : MonoBehaviour {

	public float switchDistance;
	public float currentDistance;
	public bool isWalking;

	DirectionalAnimation current = null;
	
	bool delayedStop;
	int delayCount;
	int maxDelay = 10;

	public DirectionalAnimation north = null;
	public DirectionalAnimation south = null;
	public DirectionalAnimation east = null;
	public DirectionalAnimation west = null;

	SpriteRenderer renderer;

	public CreatureAnimation()
	{
		switchDistance = 0.5f;
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
	}

	public void AddDistance(float distance)
	{
		currentDistance += distance;

		if (currentDistance >= switchDistance)
		{
			current.SwitchFeet (renderer);
			currentDistance = 0f;
		}

	}

	public void SetDirection(Direction direction, bool isMoving)
	{
		isWalking = isMoving;
		delayCount = 0;
		delayedStop = false;
		
		switch (direction) 
		{
			case Direction.North:
				SetCurrent(north);
			break;
			case Direction.South:
				SetCurrent(south);
			break;
			case Direction.East:
				SetCurrent(east);
			break;
			case Direction.West:
				SetCurrent(west);
			break;
		}
	}
	
	void SetCurrent(DirectionalAnimation newDirectional)
	{
		current = newDirectional;
		
		if(isWalking)
			current.SwitchFeet(renderer);
		else if (!isWalking)
			current.SetStanding(renderer);
	}
	
	public void Stop()
	{
		delayedStop = true;
		isWalking = false;
	}
	
	public void CheckDelayedStop()
	{
		if(delayedStop)
		{
			delayCount++;
			if(delayCount == maxDelay)
			{
				delayCount = 0;
				delayedStop = false;
				if(!isWalking)
					current.SetStanding(renderer);
			}
		}
		
	}


}
