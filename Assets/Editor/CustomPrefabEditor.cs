using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Prefab))]
public class CustomPrefabEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
	}

	
}
