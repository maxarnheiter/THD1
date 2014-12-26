using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Animator animator;

	public Vector3 last;
	public Vector3 current;
	public Vector3 target;

	public float speed;

	void Start () {
	
		animator = this.GetComponent<Animator> ();

		last = this.transform.position;
		current = this.transform.position;
	}

	void Update () {

		float step = speed * Time.deltaTime;

		current = this.transform.position;

		if (Input.GetKeyUp (KeyCode.W)) {
						animator.SetInteger("direction", 0);
						//this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
						target = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
				}
		if (Input.GetKeyUp (KeyCode.A)) {
						animator.SetInteger("direction", 3)	;
						//this.transform.position = new Vector3 (this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
						target = new Vector3 (this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
				}
		if (Input.GetKeyUp (KeyCode.S)) {
						animator.SetInteger("direction", 2);
						//this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
						target = new Vector3 (this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
				}
		if (Input.GetKeyUp (KeyCode.D)) {
						animator.SetInteger("direction", 1);
						//this.transform.position = new Vector3 (this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
						target = new Vector3 (this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
				}

		if(current != target)
			transform.position = Vector3.MoveTowards(current, target, step);

		Debug.Log (current + " " + last);

		if (current != last) {

			animator.SetBool ("is_walking", true);
			Debug.Log ("setting is walking true");
		}
		else
			animator.SetBool ("is_walking", false);

		last = current;
	}
}
