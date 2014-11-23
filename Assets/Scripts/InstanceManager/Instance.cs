using UnityEngine;
using System.Collections;

public class Instance {
	
	public GameObject gameObject;
	public Transform transform;
	public Stack stack;
	
	public Instance(GameObject obj, Transform trans, Stack stackComponent) {
		this.gameObject = obj;
		this.transform = trans;
		this.stack = stackComponent;
	}
}