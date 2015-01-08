using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	TileMovement tileMovement;

	bool receivedInput = false;
	Direction directionChange;
	PlayerInputChoice input;
	
	float minimumWaitTime = 0.3f;
	float lastTime;
	float elapsedTime;

	void Start () 
	{
		tileMovement = gameObject.GetComponent<TileMovement>();
		SetLastTime();
	}

	void Update () 
	{
		GetInput();
		ProcessInput();
		Reset();
	}
	
	void SetElapsedTime()
	{
		elapsedTime = Time.realtimeSinceStartup - lastTime;
	}
	
	bool CheckTimeElaped()
	{
		if(elapsedTime >= minimumWaitTime)
			return true;
		
		return false;
	}
	
	void SetLastTime()
	{
		lastTime = Time.realtimeSinceStartup;
	}

	void GetInput()
	{
		//Move in a direction
		if (Input.GetKey (KeyCode.W))
		{
			SetInputState(Direction.North, PlayerInputChoice.Move);
		}
		if (Input.GetKey (KeyCode.A))
		{
			SetInputState(Direction.West, PlayerInputChoice.Move);
		}
		if (Input.GetKey (KeyCode.S))
		{
			SetInputState(Direction.South, PlayerInputChoice.Move);
		}
		if (Input.GetKey (KeyCode.D))
		{
			SetInputState(Direction.East, PlayerInputChoice.Move);
		}

		//Change the look direction
		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.LeftControl)) 
		{
			SetInputState(Direction.North, PlayerInputChoice.ChangeDirection);
		}
		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.LeftControl)) 
		{
			SetInputState(Direction.West, PlayerInputChoice.ChangeDirection);
		}
		if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.LeftControl)) 
		{
			SetInputState(Direction.South, PlayerInputChoice.ChangeDirection);
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.LeftControl)) 
		{
			SetInputState(Direction.East, PlayerInputChoice.ChangeDirection);
		}
	}

	void SetInputState(Direction newDirection, PlayerInputChoice choice)
	{
		SetElapsedTime();
		
		if(CheckTimeElaped())
		{
			directionChange = newDirection;
			receivedInput = true;
			input = choice;
			
			SetLastTime();
		}
	}

	void ProcessInput()
	{
		if (receivedInput) 
		{
			if(input == PlayerInputChoice.Move)
			{
				tileMovement.Move(directionChange);
			}
			if(input == PlayerInputChoice.ChangeDirection)
			{
				tileMovement.ChangeDirection(directionChange);
			}
		}
	}

	void Reset()
	{
		receivedInput = false;
		input = PlayerInputChoice.None;
	}
}
