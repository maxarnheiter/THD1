using UnityEngine;
using UnityEditor;

//for testing only
using System.Collections.Generic;
using System.Linq;

public delegate void MapEditorWindowEventHandler();

public class MapEditorWindow : EditorWindow 
{
	static Vector2 currentMousePos;
	static Vector2 lastMousePos;

	[MenuItem ("THD/Map Editor")]
	static void Init () {
		MapEditorWindow mapEditorWindow = (MapEditorWindow)EditorWindow.GetWindow (typeof(MapEditorWindow));
	}
	
	void OnEnable() {
		this.title = "Map Editor";
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;			//Listen to scene events
	}
	
	void OnGUI() {
	
		EditorGUILayout.Space();
		displayMapGUI();
		
		EditorGUILayout.Space();
		displayMapToolsGUI();
		
	}
	
	void OnSceneGUI(SceneView sceneView) {
	
		if(Tools.current != Tool.View)
			return;
		
		Event currentEvent = Event.current;
		
		switch(currentEvent.type) {
		
			case EventType.MouseUp: {
			
				MapEditor.Click();
				break;
			}
			
			case EventType.MouseMove: {
			
				Vector2 adjustedMousePosition = new Vector2(currentEvent.mousePosition.x, sceneView.camera.pixelHeight - currentEvent.mousePosition.y);
				Vector2 rawMousePosition = sceneView.camera.ScreenToWorldPoint(adjustedMousePosition);
				currentMousePos = new Vector3(Mathf.Floor(rawMousePosition.x) + 1f, Mathf.Floor(rawMousePosition.y));
				
				if(currentMousePos != lastMousePos) {
					lastMousePos = currentMousePos;
					MapEditor.Position = currentMousePos;
				}
			           
			break;
			}
			
			case EventType.Repaint: {
			
				HeightManager.UpdateObjectsForCamera(sceneView.camera);
				break;
			}
		}
	}
	
	void displayMapGUI() {
	
		EditorGUILayout.Space ();
		GUILayout.Label("Map Options: ");
		GUILayout.BeginHorizontal();
			displayMapOptions();
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space ();
		GUILayout.Label("Map Statistics: ");
			displayMapStatistics();
		

		
	}
	
	void displayMapOptions() {
	
		if(GUILayout.Button ("New", GUILayout.Width(100f))) {
			MapEditor.CreateNewMap();
		}
		
		if(GUILayout.Button ("Load", GUILayout.Width(100f))) {
			string path = EditorUtility.OpenFilePanel("", "", ".xml");
			if(path != "")
				MapEditor.Load(path);
		}
		
		if(!MapEditor.hasMap)
		if(MapEditor.mapPath != "")
			GUI.enabled = false;
		if(GUILayout.Button ("Save", GUILayout.Width(100f))) {
			MapEditor.Save();
		}
		GUI.enabled = true;
		
		if(!MapEditor.hasMap)
			GUI.enabled = false;
		if(GUILayout.Button ("Save As", GUILayout.Width(100f))) {
			string path = EditorUtility.SaveFilePanel("", "", "", ".xml");
			if(path != "")
				MapEditor.SaveAs(path);
		}
		GUI.enabled = true;
	}
	
	void displayMapStatistics() {
			
		if(!MapEditor.hasMap)
			GUI.enabled = false;
				
		GUILayout.Label ("Current Map Path: " + MapEditor.mapPath);	
		
		GUILayout.BeginHorizontal();
		
		GUILayout.Label ("Total Map Objects: " + MapEditor.mapObjects);
		GUILayout.Label ("Total Map Changes: " + MapEditor.mapChanges);
		
		
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.Label ("Past Changes: " + MapEditor.pastChanges);
		GUILayout.Label ("Future Changes: " + MapEditor.futureChanges);
		
		GUILayout.EndHorizontal();
		
		GUI.enabled = true;
	}
	
	void displayMapToolsGUI() {
	
	
	
	}
	
}


