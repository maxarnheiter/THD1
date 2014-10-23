using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class Map
{
	[XmlAttribute("mapObjects")] 		List<MapObject> _mapObjects;
	
	[XmlAttribute("pastChanges")]		Stack<MapChange> _pastChanges;
	
	[XmlAttribute("futureChanges")] 	Stack<MapChange> _futureChanges;
}
