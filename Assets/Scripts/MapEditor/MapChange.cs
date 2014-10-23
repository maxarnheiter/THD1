using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class MapChange
{
	[XmlAttribute("isAdd")]		bool _isAdd;
	
	[XmlAttribute("mapObject")]	MapObject _mapObject;
	
	//Constructor
	public MapChange(bool isAdd, MapObject mapObject) {
		this._isAdd = isAdd;
		this._mapObject = mapObject;
	}
	
	[XmlIgnore]
	public bool IsAdd {
	get { return this._isAdd; }
	}
	
	[XmlIgnore]
	public int ID {
	get { return this._mapObject.ID; }
	}
	
	[XmlIgnore]
	public Vector3 Position {
		get { return this._mapObject.Position; }
	}
	
}