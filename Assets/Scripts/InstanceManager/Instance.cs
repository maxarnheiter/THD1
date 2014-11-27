using UnityEngine;
using System.Collections;

public class Instance {
	
	public GameObject gameObject;
	public Transform transform;
	public Stack stack;
	public SpriteRenderer spriteRenderer;
	
	public Instance(GameObject obj) {
		this.gameObject = obj;
		this.transform = obj.transform;
		this.stack = obj.GetComponent<Stack>();
		this.spriteRenderer = obj.GetComponent<SpriteRenderer>();
	}
}