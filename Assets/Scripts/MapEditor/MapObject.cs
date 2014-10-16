using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class MapObject
{
	[XmlAttribute("id")]	public int _id;
	
	[XmlAttribute("pos")]	public Vector3 _pos;
	
	public MapObject(int id, Vector3 pos) {
		this._id = id;
		this._pos = pos;
	}
}