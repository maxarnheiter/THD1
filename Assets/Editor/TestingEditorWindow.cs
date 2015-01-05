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
			


			GameObject o = new GameObject();

			Vector3 v = new Vector3(0f, 0f,0f);

			string blah = "Floor " + v.z.ToString();
			//string blah2 = "Floor 0";

			Debug.Log (blah);
			o.layer = LayerMask.NameToLayer(blah);
			//o.layer = LayerMask.NameToLayer(blah2);

		}

		
		}
		

}
