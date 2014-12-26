using UnityEngine;
using System.Collections;

public class AnimTester : MonoBehaviour {

	public Sprite leftFoot;
	public Sprite rightFoot;
	
	SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
	
		this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
