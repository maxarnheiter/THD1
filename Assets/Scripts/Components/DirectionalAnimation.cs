using UnityEngine;
using System.Collections;

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

	public void SwitchFeet(SpriteRenderer renderer)
	{

	}
	
}
