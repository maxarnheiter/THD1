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
					MapEditor.MouseMove(currentMousePos);
				}
			           
			break;
			}
			
			case EventType.Repaint: {
			
				HeightManager.UpdateObjectsForCamera(sceneView.camera);
				break;
			}
		}
		
		MapEditor.DrawPreview();
	}
	
}


