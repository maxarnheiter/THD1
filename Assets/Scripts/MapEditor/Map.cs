using UnityEngine;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class Map
{
	public List<MapObject> data;
	
	public Map() {
		data =  new List<MapObject>();
	}
	
}
