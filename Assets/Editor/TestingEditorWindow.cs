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

			var colors = PrefabManager.current.texture.GetPixels();

			foreach(var blah in colors.GroupBy(x => x).OrderBy(x => x.Count ()))
				Debug.Log(blah.Count() + " " + blah.First());



		}

		
		}
		

}
