using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class TestingEditorWindow : EditorWindow {

	[MenuItem ("THD/Testing Window")]
	static void Init () {
		TestingEditorWindow testingEditorWindow = (TestingEditorWindow)EditorWindow.GetWindow (typeof(TestingEditorWindow));
	}
	
	void OnEnable() {
		this.title = "Testing Window";
	}
	
	void OnGUI()
	{
		EditorGUILayout.Space ();
		if(GUILayout.Button ("Test", GUILayout.Width (100f))) {
		
			var floor = InstanceManager.instances.Where (f => f.Value.transform.position.z == 0);
			
			Debug.Log (floor.Count ());
			
			//var grounds = floor.Where (f => f.Value.gameObject.tag == "ground tile");
			
		
		}
		
	}
}
