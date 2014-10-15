using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class MapObject
{
	[XmlAttribute("name")]	public string _name;
	
	[XmlAttribute("pos")]	public Vector3 _pos;
	
	public MapObject(string name, Vector3 pos) {
		this._name = name;
		this._pos = pos;
	}
}