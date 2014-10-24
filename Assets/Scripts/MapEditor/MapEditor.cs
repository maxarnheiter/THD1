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
	public static Map Map;
	
	static ClickAction _action;
	public static ClickAction Action {
		get { 
			return _action; 
		}
		set {
			_action = value;
			OnActionChange();
		}
	}
	
	static Vector2 _position;
	public static Vector2 Position {
		get {
			return _position;
		}
		set {
			_position = value;
			OnPositionChange();
		}
	}
	
	static float _floor;
	public static float Floor {
		get {
			return _floor;
		}
		set {
			_floor = value;
			OnFloorChange();
		}
	}
	
	public static float FloorHeight {
		get {
			return (_floor * -1);
		}
	}
	
	public static string FloorOverlaySortingLayerName {
		get {
			return ("Floor " + _floor + " Overlay");
		}
	}
	
	static float _previewTransparency = 0.5f;
	
	static Color _addColor = new Color(0f, 1f, 0f, _previewTransparency);
	static Color _removeColor = new Color(1f, 0f, 0f, _previewTransparency);

	
	static int _prefabId;

	static GameObject _preview;
	static SpriteRenderer _previewRenderer;
	static Sprite _previewSprite;
	static Texture2D _previewTexture;
	
	public static void Click() {
	
		switch(_action) {
		
			case ClickAction.Add: {
				//TODO add event
				break;
			}
			
			case ClickAction.Remove: {
				//TODO remove event
				break;
			}
		}
	}
	
	static void CreatePreviewObject() {
	
		while(GameObject.Find("Preview Object") != null)
			Object.DestroyImmediate(GameObject.Find("Preview Object"));
		
		_preview = new GameObject();
		_preview.name = "Preview Object";
		
		_previewTexture = new Texture2D(1,1);
		if(Action == ClickAction.Add)
			_previewTexture.SetPixel(0,0, _addColor);
		else
			_previewTexture.SetPixel(0,0, _removeColor);
		_previewTexture.Apply();
		
		_previewSprite = Sprite.Create(_previewTexture, new Rect(0,0,1,1), new Vector2(1,0), 1f);
		
		_previewRenderer = _preview.AddComponent<SpriteRenderer>();
		_previewRenderer.sprite = _previewSprite;
		_previewRenderer.sortingLayerName = FloorOverlaySortingLayerName;
	}
	
	static void OnActionChange() {
	
		if(_preview) 
			switch(_action) {
				case ClickAction.Add : _previewRenderer.color = _addColor; 
				break;
				case ClickAction.Remove : _previewRenderer.color = _removeColor;
				break;
			}
	}
	
	static void OnPositionChange() {
		
		if(!_preview)
			CreatePreviewObject();
		
		_preview.transform.position = new Vector3(Position.x, Position.y, FloorHeight);
	}
	
	static void OnFloorChange() {
	
		if(_preview)
			_previewRenderer.sortingLayerName = FloorOverlaySortingLayerName;
	}

	static void Load(string path) {
		XmlSerializer serializer =  new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Open))
			Map = serializer.Deserialize(stream) as Map;
	}

	static void Save(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Create))
			serializer.Serialize(stream, Map);
	}
	
}


