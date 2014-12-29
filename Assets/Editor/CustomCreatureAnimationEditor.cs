using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CreatureAnimation))]
public class CustomCreatureAnimationEditor : Editor
{

	public override void OnInspectorGUI()
	{
		var ca = (CreatureAnimation)target;
		
		ca.switchDistance = EditorGUILayout.FloatField("Switch Distance: ", ca.switchDistance);
		
		EditorGUILayout.Space ();

		DirectionalAnimationMenu (ca.north);
		DirectionalAnimationMenu (ca.south);
		DirectionalAnimationMenu (ca.east);
		DirectionalAnimationMenu (ca.west);

	}

	void DirectionalAnimationMenu(DirectionalAnimation dirAnim)
	{
		dirAnim.direction = (Direction)EditorGUILayout.EnumPopup(dirAnim.direction);
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Standing", GUILayout.Width(100f));
		dirAnim.standing = (Sprite)EditorGUILayout.ObjectField(dirAnim.standing, typeof(Sprite));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Left", GUILayout.Width(100f));
		dirAnim.left = (Sprite)EditorGUILayout.ObjectField(dirAnim.left, typeof(Sprite));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Right", GUILayout.Width(100f));
		dirAnim.right = (Sprite)EditorGUILayout.ObjectField(dirAnim.right, typeof(Sprite));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space ();
	}
}
