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
	public static Map map;
	
	static ClickAction _action;
	static Vector2 _position;
	static float _floor;
	static int _prefabId;
	
	static Color _addColor = Color.green;
	static Color _removeColor = Color.red;
	static Color _outlineColor = Color.clear;
	static Vector3[] _drawVerts;
	
	
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
	
	public static void MouseMove(Vector2 position) {
	
		_position = position;
		
		_drawVerts = new Vector3[4] {
										new Vector3(_position.x, _position.y, (_floor * -1f)),
										new Vector3(_position.x, _position.y + 1f, (_floor * -1f)),
										new Vector3(_position.x - 1f, _position.y + 1f, (_floor * -1f)),
										new Vector3(_position.x - 1f, _position.y, (_floor * -1f))};
	}
	
	public static void DrawPreview() {
	
		switch(_action) {
			
		case ClickAction.Add: {
			Handles.DrawSolidRectangleWithOutline(_drawVerts, _addColor, _outlineColor);
			break;
		}
			
		case ClickAction.Remove: {
			Handles.DrawSolidRectangleWithOutline(_drawVerts, _removeColor, _outlineColor);
			break;
		}
		}
		
	}

	static void Load(string path) {
		XmlSerializer serializer =  new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Open))
			map = serializer.Deserialize(stream) as Map;
	}

	static void Save(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(Map));
		using(FileStream stream = new FileStream(path, FileMode.Create))
			serializer.Serialize(stream, map);
	}
	
}


