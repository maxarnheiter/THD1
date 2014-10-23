using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class MapObject
{
	[XmlAttribute("id")]	int _id;
	
	[XmlAttribute("pos")]	Vector3 _pos;
	
	public MapObject(int id, Vector3 pos) {
		this._id = id;
		this._pos = pos;
	}
	
	[XmlIgnore]
	public int ID { 
	get { return this._id; }
	}
	
	[XmlIgnore]
	public Vector3 Position{
	get { return this._pos; }
	}
}