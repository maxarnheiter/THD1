using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class MapObject  {

	public int id;
	public float x;
	public float y;
	public float z;
	
	
	public MapObject() {}
	
	public MapObject(int ID, float X, float Y, float Z) {
		this.id = ID;
		this.x = X;
		this.y = Y;
		this.z = Z;
	}
	
}
