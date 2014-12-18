using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

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
		
		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.BeginVertical ();
		PrefabFilterGUI ();
		PrefabManager.OnGUI (1000);
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ();

		EditorControlsGUI ();

		EditorGUILayout.EndVertical ();

		EditorGUILayout.EndHorizontal ();
	}

	void EditorControlsGUI() {

		MapOptionsGUI ();
		MapStatisticsGUI();
		ActionGUI ();
		FloorGUI();
		PrefabGUI();
		SetIDGUI();
	}

	void PrefabFilterGUI() {

		EditorGUILayout.BeginHorizontal ();

		PrefabManager.showGrounds = GUILayout.Toggle (PrefabManager.showGrounds, "Show Grounds");
		PrefabManager.showCorners = GUILayout.Toggle (PrefabManager.showCorners, "Show Corners");
		PrefabManager.showThings = GUILayout.Toggle (PrefabManager.showThings, "Show Things");

		EditorGUILayout.EndHorizontal ();


	}
	
	void MapOptionsGUI() {

		EditorGUILayout.BeginHorizontal ();
		
		//Clear
			if(!InstanceManager.hasInstances)
				GUI.enabled = false;
			if(GUILayout.Button ("Clear", GUILayout.Width (50f))) {
				MapEditor.Clear();
			}
			GUI.enabled = true;
		
		//New
			if(InstanceManager.hasInstances)
				GUI.enabled = false;
			if(GUILayout.Button ("New", GUILayout.Width(50f))) {
				MapEditor.New();
			}
			GUI.enabled = true;
			
		//Load
			if(InstanceManager.hasInstances)
				GUI.enabled = false;
			if(GUILayout.Button ("Load", GUILayout.Width(50f))) {
				string path = EditorUtility.OpenFilePanel("", "", "xml");
				if(path != "")
					MapEditor.Load(path);
			}
			GUI.enabled = true;
			
		//Save
			if(!InstanceManager.hasInstances || MapEditor.mapPath == "")
				GUI.enabled = false;
			if(GUILayout.Button ("Save", GUILayout.Width(50f))) {
				MapEditor.Save();
			}
			GUI.enabled = true;
		
		//Save As
			if(!InstanceManager.hasInstances)
				GUI.enabled = false;
			if(GUILayout.Button ("Save As", GUILayout.Width(70f))) {
				string path = EditorUtility.SaveFilePanel("", "", "", "xml");
				if(path != "")
					MapEditor.SaveAs(path);
			}
			GUI.enabled = true;

			EditorGUILayout.EndHorizontal ();
	}
	
	void MapStatisticsGUI() {
				
		GUILayout.Label ("Current Map Path: " + MapEditor.mapPath);	
	}
	
	void ActionGUI() {
	
		EditorGUILayout.BeginHorizontal();
		
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
		
		EditorGUILayout.EndHorizontal();
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

		if (GUILayout.Button ("Floor Visibility", GUILayout.Width (100f))) {
			MapEditor.fullFloors =! MapEditor.fullFloors;
		}
	}
	
	void PrefabGUI() {
	
		EditorGUILayout.BeginVertical();
	
		GUILayout.Label ("Current prefab id: " + PrefabManager.current);

		if (PrefabManager.current == null)
						return;
		GUILayout.Button (	PrefabManager.current.texture, 
		                  GUILayout.Width (PrefabManager.current.width), 
		                  GUILayout.Height(PrefabManager.current.height));
		                  
		EditorGUILayout.EndVertical();
	}

	void SetIDGUI() {

		if (PrefabManager.prefabs.Count > 0)
			GUILayout.Label ("Next SID: " + (PrefabManager.prefabs.Max (p => p.Value.setId + 1)));

		GUILayout.Label ("Next click SID: " + MapEditor.nextSetId.ToString());

		EditorGUILayout.BeginHorizontal ();
		
		if (GUILayout.Button ("+", GUILayout.Width (40f)))
			MapEditor.nextSetId = MapEditor.nextSetId + 1;

		if (GUILayout.Button ("-", GUILayout.Width (40f)))
			MapEditor.nextSetId--;

		if (GUILayout.Button ("*", GUILayout.Width (40f)))
			MapEditor.nextSetId = PrefabManager.prefabs.Max (p => p.Value.setId) + 1;

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

		if (MapEditor.selectAction == SelectAction.Current)
			GUI.enabled = false;
		if(GUILayout.Button ("Current", GUILayout.Width (60f)))
		   MapEditor.selectAction = SelectAction.Current;
		GUI.enabled = true;

		if (MapEditor.selectAction == SelectAction.SetID)
			GUI.enabled = false;
		if (GUILayout.Button ("Set ID", GUILayout.Width (60f))) {
			MapEditor.selectAction = SelectAction.SetID;
			PrefabManager.current = null;
		}
		GUI.enabled = true;

		EditorGUILayout.EndHorizontal ();
	}
}


