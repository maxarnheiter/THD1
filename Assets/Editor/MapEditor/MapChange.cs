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
	public string ObjectName {
		get { return this._mapObject._name; }
	}
	
	[XmlIgnore]
	public Vector3 ObjectPosition {
		get { return this._mapObject._pos; }
	}
	
	public void Trigger() {
		if (this._isAdd)
			Add ();
		else
			Remove ();
	}
	
	public void Undo() {
		if (this._isAdd)
			Remove ();
		else
			Add ();
	}
	
	void Add() {
		MapEditor.OnAdd (this);
	}
	
	void Remove() {
		MapEditor.OnRemove (this);
	}
}