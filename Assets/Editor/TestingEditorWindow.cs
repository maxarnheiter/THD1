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
		
			var blah = InstanceManager.instances	.OrderBy(x => x.Value.transform.position.z)
													.OrderBy(x => x.Value.transform.position.x)
													.OrderBy(x => x.Value.transform.position.y)
													.OrderBy (y => y.Value.stack.uid);
			
			foreach (var b in blah)
				Debug.Log (b.Key + " " + b.Value.transform.position + " " + b.Value.stack.uid);
		
		}
		
	}
}
