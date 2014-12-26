using UnityEngine;
using System.Collections;

public class AnimTester : MonoBehaviour {

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
	
	SpriteRenderer renderer;


	void Start () 
	{
		renderer = this.gameObject.GetComponent<SpriteRenderer>();
		
	}
	

	void FixedUpdate () 
	{
			
			elapsedTime += Time.deltaTime;
			
			if(elapsedTime >= speed)
			{
				elapsedTime = 0;
				
				
			}
			
	}
}
