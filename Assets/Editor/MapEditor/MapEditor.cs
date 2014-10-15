using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class MapEditor
{
	public static Map map;

	public static List<GameObject> instances {
		get {
			return ResourceManager.instances;
		}
	}

	public static List<GameObject> prefabs {
		get {
			return ResourceManager.prefabs;
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

	public static void OnAdd(MapChange change) {

	}

	public static void OnRemove(MapChange change) {
	
	}
}


