using UnityEngine;
using UnityEditor;
using System.Collections;

public class InstanceManagerWindow : EditorWindow {

	[MenuItem ("THD/Instance Editor")]
	static void Init () {
		InstanceManagerWindow instanceManagerWindow = (InstanceManagerWindow)EditorWindow.GetWindow (typeof(InstanceManagerWindow));
	}

	void OnEnable() {
		this.title = "Instance Editor";
	}

	void OnGUI()
	{
	}
}
