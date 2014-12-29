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
	
	float lastSwitchTime;
	float minimumSwitchTime = 0.1f;

	public void SwitchFeet(SpriteRenderer renderer)
	{
		float timeElapsed = Time.realtimeSinceStartup - lastSwitchTime;
		
		if(timeElapsed >= minimumSwitchTime)
		{
			//If our starting point is a standing position
			if(renderer.sprite != left && renderer.sprite != right)
			{
				renderer.sprite = left;
				lastSwitchTime = Time.realtimeSinceStartup;
				return;
			}
			//If our current sprite has the right foot forward
			if(renderer.sprite == right)
			{
				renderer.sprite = left;
				lastSwitchTime = Time.realtimeSinceStartup;
				return;
			}
			//If our current sprite has the left foot forward
			if(renderer.sprite == left)
			{
				renderer.sprite = right;
				lastSwitchTime = Time.realtimeSinceStartup;
				return;
			}
		}
		else
			Debug.Log (timeElapsed);
	}
	
	public void SetStanding(SpriteRenderer renderer)
	{
		renderer.sprite = standing;
	}
	
}
