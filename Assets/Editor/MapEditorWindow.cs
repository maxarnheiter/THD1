using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow 
{
	static Vector2 currentMousePos;
	static Vector2 lastMousePos;
	
	static Texture2D _pencilIcon;
	static Texture2D pencilIcon {
		get { return _pencilIcon ?? (_pencilIcon = Resources.Load("EditorSprites/pencil") as Texture2D); }
	}
	
	static Texture2D _eraserIcon;
	static Texture2D eraserIcon {
		get { return _eraserIcon ?? (_eraserIcon = Resources.Load("EditorSprites/eraser") as Texture2D); }
	}
	
	static Texture2D _upIcon;
	static Texture2D upIcon {
		get { return _upIcon ?? (_upIcon = Resources.Load("EditorSprites/up") as Texture2D); }
	}
	
	static Texture2D _downIcon;
	static Texture2D downIcon {
		get { return _downIcon ?? (_downIcon = Resources.Load("EditorSprites/down") as Texture2D); }
	}

	[MenuItem ("THD/Map Editor")]
	static void Init () {
		MapEditorWindow mapEditorWindow = (MapEditorWindow)EditorWindow.GetWindow (typeof(MapEditorWindow));
	}
	
	void OnEnable() {
		this.title = "Map Editor";
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;			//Listen to scene events
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
					MapEditor.position = currentMousePos;
				}
			           
			break;
			}
			
			case EventType.Repaint: {
					StackRenderer.UpdateCameraObjects(sceneView.camera);
				break;
			}
		}
	}
	
	void OnGUI() {
		
		EditorGUILayout.Space();
		GUILayout.Label("Map Options: ");
		GUILayout.BeginHorizontal();
			MapOptionsGUI();
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space ();
		GUILayout.Label("Map Statistics: ");
			MapStatisticsGUI();
		
		EditorGUILayout.Space();
			MapToolsGUI();
	}
	
	void MapOptionsGUI() {
	
	//Clear
		if(!InstanceManager.hasInstances)
			GUI.enabled = false;
		if(GUILayout.Button ("Clear", GUILayout.Width (100f))) {
			MapEditor.Clear();
		}
		GUI.enabled = true;
	
	//New
		if(InstanceManager.hasInstances)
			GUI.enabled = false;
		if(GUILayout.Button ("New", GUILayout.Width(100f))) {
			MapEditor.New();
		}
		GUI.enabled = true;
		
	//Load
		if(InstanceManager.hasInstances)
			GUI.enabled = false;
		if(GUILayout.Button ("Load", GUILayout.Width(100f))) {
			string path = EditorUtility.OpenFilePanel("", "", "xml");
			if(path != "")
				MapEditor.Load(path);
		}
		GUI.enabled = true;
		
	//Save
		if(!InstanceManager.hasInstances || MapEditor.mapPath == "")
			GUI.enabled = false;
		if(GUILayout.Button ("Save", GUILayout.Width(100f))) {
			MapEditor.Save();
		}
		GUI.enabled = true;
	
	//Save As
		if(!InstanceManager.hasInstances)
			GUI.enabled = false;
		if(GUILayout.Button ("Save As", GUILayout.Width(100f))) {
			string path = EditorUtility.SaveFilePanel("", "", "", "xml");
			if(path != "")
				MapEditor.SaveAs(path);
		}
		GUI.enabled = true;
	}
	
	void MapStatisticsGUI() {
				
		GUILayout.Label ("Current Map Path: " + MapEditor.mapPath);	
	}
	
	
	void MapToolsGUI() {
	
	EditorGUILayout.BeginHorizontal();
	
		EditorGUILayout.BeginHorizontal(GUILayout.Width (150f));
			ActionGUI();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal(GUILayout.Width (150f));
			FloorGUI();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal(GUILayout.Width (150f));
			PrefabGUI();
		EditorGUILayout.EndHorizontal();
	
	EditorGUILayout.EndHorizontal();	                
	}
	
	void ActionGUI() {
	
		EditorGUILayout.BeginVertical();
		
		if(MapEditor.action == ClickAction.Add)
			GUI.enabled = false;
		if(GUILayout.Button (pencilIcon, GUILayout.Width (50f))) {
			MapEditor.action = ClickAction.Add;
		}
		GUI.enabled = true;
		
		if(MapEditor.action == ClickAction.Remove)
			GUI.enabled = false;
		if(GUILayout.Button (eraserIcon, GUILayout.Width (50f))) {
			MapEditor.action = ClickAction.Remove;
		}
		GUI.enabled = true;
		
		EditorGUILayout.EndVertical();
	}
	
	void FloorGUI() {
	
		EditorGUILayout.BeginVertical();
	
		if(GUILayout.Button(upIcon, GUILayout.Width (40f))) {
			MapEditor.floor++;
		}
		
		GUILayout.Label ("Current floor: " + MapEditor.floor);
		
		if(GUILayout.Button(downIcon, GUILayout.Width (40f))) {
			MapEditor.floor--;
		}
		
		EditorGUILayout.EndVertical();
	}
	
	void PrefabGUI() {
	
		EditorGUILayout.BeginVertical();
	
		GUILayout.Label ("Current prefab id: " + PrefabManager.current);
		
		GUILayout.Button (	PrefabManager.currentTexture, 
		                  GUILayout.Width (PrefabManager.currentTextureWidth), 
		                  GUILayout.Height(PrefabManager.currentTextureHeight));
		                  
		EditorGUILayout.EndVertical();
	}
}


