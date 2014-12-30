using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileMovement))]
public class CustomTileMovementEditor : Editor 
{
	
	public override void OnInspectorGUI()
	{
		var tm = (TileMovement)target;
		
		tm.speed = EditorGUILayout.FloatField("Speed: ", tm.speed);
		
	}

}
