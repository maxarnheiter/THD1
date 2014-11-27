using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class MapContainer : MonoBehaviour {

	void Start () {
		CheckForDisparty();
	}
	
	void Update () {
		CheckForDisparty();
	}
	
	void CheckForDisparty() {
	
		if(InstanceManager.instances.Count == 0 && this.transform.childCount != 0) {
			InstanceManager.ImportFrom(this.gameObject);
		}
	}
	
}
