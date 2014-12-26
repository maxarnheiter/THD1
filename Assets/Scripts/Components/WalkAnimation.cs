using UnityEngine;
using System.Collections;

public class WalkAnimation : MonoBehaviour {

	public Sprite standing_north;
	public Sprite walking_north_left;
	public Sprite walking_north_right;
	
	public Sprite standing_south;
	public Sprite walking_south_left;
	public Sprite walking_south_right;
	
	public Sprite standing_east;
	public Sprite walking_east_left;
	public Sprite walking_east_right;
	
	public Sprite standing_west;
	public Sprite walking_west_left;
	public Sprite walking_west_right;
	
	public float speed = 1f;
	public float elapsedTime = 0f;
	
	Direction _direction;
	public Direction direction {
		get { return _direction; }
		set 
		{
			if(value != _direction)
			{
				_direction = value;
				OnDirectionChanged();
			}
		}
	}
	
	public bool isWalking;
	public bool legSwitch;
	
	SpriteRenderer renderer;

	void Start () 
	{
		renderer = this.gameObject.GetComponent<SpriteRenderer>();
		renderer.sprite = standing_north;
	}
	

	void FixedUpdate () 
	{
		elapsedTime += Time.deltaTime;

			
		if(elapsedTime >= speed) 
		{
			elapsedTime = 0f;
			legSwitch = true;
			UpdateAnimation();
		}
	}
	
	public void OnDirectionChanged()
	{
		UpdateAnimation();
	}
	
	public void OnPositionChanged()
	{
		UpdateAnimation();
	}
	
	void UpdateAnimation()
	{
		if(isWalking)
		{
			if(legSwitch)
			{
				switch(direction)
				{
					case Direction.North:
					{
						if(renderer.sprite == walking_north_left)
							renderer.sprite = walking_north_right;
						else
							renderer.sprite = walking_north_left;
					}
					break;
					case Direction.South:
					{
						if(renderer.sprite == walking_south_left)
							renderer.sprite = walking_south_right;
						else
							renderer.sprite = walking_south_left;
					}
						break;
					case Direction.East:
					{
						if(renderer.sprite == walking_east_left)
							renderer.sprite = walking_east_right;
						else
							renderer.sprite = walking_east_left;
					}
						break;
					case Direction.West:
					{
						if(renderer.sprite == walking_west_left)
							renderer.sprite = walking_west_right;
						else
							renderer.sprite = walking_west_left;
					}
						break;
				}
				legSwitch = false;
			}
		}
		else
		{
			switch(direction)
			{
				case Direction.North:
					renderer.sprite = standing_north;
				break;
				case Direction.South:
					renderer.sprite = standing_south;
				break;
				case Direction.East:
					renderer.sprite = standing_east;
				break;
				case Direction.West:
					renderer.sprite = standing_west;
				break;
			}
		}
	}
	
}
