using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow 
{
	enum Action
	{
		Add,
		Move, 
		Remove
	}

	Action currentAction;


	[MenuItem ("THD/Map Editor")]
	static void Init () {
		MapEditorWindow mapEditorWindow = (MapEditorWindow)EditorWindow.GetWindow (typeof(MapEditorWindow));
	}
	
	void OnEnable() {
		this.title = "Map Editor";
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}
	
	void OnGUI() {
		
		if (MapEditor.map == null)
			OnDisplayNoMap ();


	}
	
	void OnSceneGUI(SceneView sceneView)
	{
		
	}
	
	void OnDisplayNoMap() {
		
		EditorGUILayout.LabelField ("There is currently no map to be found. ");
		
		EditorGUILayout.BeginHorizontal ();
		
		if (GUILayout.Button ("Load Map")) {
			//todo
		}
		
		if (GUILayout.Button ("Create New Map")) {
			//todo
		}
		
		if (GUILayout.Button ("Load From Scene")) {
			//todo
		}
		
		EditorGUILayout.EndHorizontal ();
	}
}


