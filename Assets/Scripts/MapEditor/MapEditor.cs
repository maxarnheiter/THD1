using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class MapEditor
{
	public static int nextSetId;
	public static SelectAction selectAction;
	public static PrefabCategory nextCategory;
	public static PrefabColor nextColor;

	static string _mapPath;
	public static string mapPath {
		get { return _mapPath; }
	}

	public static bool fullFloors {
		get { return _fullFloors; }
		set { _fullFloors = value; 
			FloorRenderer.SetVisibleFloors(floorHeight, _fullFloors);
		}
	}
	static bool _fullFloors = false;
	
	static GameObject _mapContainer;
	public static GameObject mapContainer {
		get {
			if(_mapContainer == null) {
				if(GameObject.Find("MapContainer") != null)
					_mapContainer = GameObject.Find("MapContainer");
				else {
					_mapContainer = new GameObject();
					_mapContainer.name = "MapContainer";
					_mapContainer.transform.position = Vector3.zero;
				}
			}
			return _mapContainer;
		}
	}
	
	static ClickAction _action;
	public static ClickAction action {
		get { return _action; }
		set {
			_action = value;
			MapEditorPreview.OnActionChanged();
		}
	}
	
	static Vector2 _position;
	public static Vector2 position {
		get { return _position; }
		set {
			_position = value;
			MapEditorPreview.OnPositionChanged();
		}
	}
	
	static int _floor;
	
	public static int floor {
		get { return _floor; }
		set {
			_floor = value;
			MapEditorPreview.OnFloorChanged();
			FloorRenderer.SetVisibleFloors(floorHeight, fullFloors);
			StackRenderer.UpdateCameraObjects(SceneView.GetAllSceneCameras().First());
		}
	}
	
	public static float floorHeight {
		get { return (_floor * -1); }
	}
	
	public static void Click() {
	
		switch(_action) {
		
			case ClickAction.Add: {
				if(PrefabManager.current != null)
					InstanceManager.Instantiate(	PrefabManager.current, 
													new Vector3(position.x, position.y, (float)(floorHeight)));
				break;
			}
			
			case ClickAction.Remove: {
				
				var obj = InstanceManager.GetTopFromPosition(new Vector3(position.x, position.y, (float)(floorHeight)));
				InstanceManager.Destroy(obj.Key);
				break;
			}
		}
	}
	
	public static void Clear() {
		
		InstanceManager.Clear();
	}
	
	public static void New() {
		_mapPath = "";
		Clear ();
	}
	
	public static void Load(string path) {
		Clear ();
		load(path);
		_mapPath = path;
		StackRenderer.UpdateCameraObjects (SceneView.GetAllSceneCameras ().First ());
	}
	
	public static void Save() {
		save(_mapPath);
	}
	
	public static void SaveAs(string path) {
		save(path);
		_mapPath = path;
	}

	static void load(string path) {
	
		Map map;
		
		XmlSerializer serializer =  new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Open))
			map = serializer.Deserialize(stream) as Map;
		
		foreach(var mapObject in map.data) {
			Prefab prefab = PrefabManager.prefabCollection.GetPrefab(mapObject.id);
			InstanceManager.Instantiate(prefab, new Vector3(mapObject.x, mapObject.y, mapObject.z));
		}

		FloorRenderer.SetVisibleFloors (floorHeight, fullFloors);
	}

	static void save(string path) {
		
		Map map = new Map();
		
		foreach(var instance in InstanceManager.instances.Sort()) {
			MapObject mapObject =  new MapObject(	int.Parse(instance.Value.gameObject.name), 
													instance.Value.transform.position.x,
													instance.Value.transform.position.y,
													instance.Value.transform.position.z);
			map.data.Add (mapObject);
		}
	
		XmlSerializer serializer = new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Create))
			serializer.Serialize(stream, map);
	}
}


