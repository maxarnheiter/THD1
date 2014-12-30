using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class DirectionalAnimation
{
	public DirectionalAnimation(Direction dir)
	{
		this.direction = dir;
	}

	public Direction direction;
	
	public Sprite standing;
	public Sprite left;
	public Sprite right;
	
	float lastTime;
	float elapsedTime;
	
	public void Animate(SpriteRenderer renderer, bool isMoving, float minimumSwitchTime)
	{
		
		if(!isMoving)	//Standing
		{
			if(renderer.sprite != standing)
				renderer.sprite = standing;
		}
		
		else 			//Moving
		{
			elapsedTime = Time.realtimeSinceStartup - lastTime;
			
			CheckIfStanding(renderer);
			
			if(elapsedTime >= minimumSwitchTime)
			{
				SwitchFeet(renderer);
			}
		}
	}

	void SwitchFeet(SpriteRenderer renderer)
	{
		//If our current sprite has the right foot forward
		if(renderer.sprite == right)
		{
			renderer.sprite = left;
			lastTime = Time.realtimeSinceStartup;
			return;
		}
		//If our current sprite has the left foot forward
		if(renderer.sprite == left)
		{
			renderer.sprite = right;
			lastTime = Time.realtimeSinceStartup;
			return;
		}
		
	}
	
	void CheckIfStanding(SpriteRenderer renderer)
	{
		//If our starting point is a standing position
		if(renderer.sprite != left && renderer.sprite != right)
		{
			renderer.sprite = left;
			lastTime = Time.realtimeSinceStartup;
		}
	}
	
}
