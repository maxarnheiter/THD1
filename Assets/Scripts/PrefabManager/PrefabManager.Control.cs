using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static partial class PrefabManager 
{
	
	static void Clear() {
		_count = 0;
		_prefabs = new Dictionary<int, Prefab> ();
	}
	
	public static bool Reload() {
		Clear ();
		return Load();
	}

	public static bool Load() {

		Clear ();
	
		var rawObjects = Resources.LoadAll ("Prefabs/");
		var rawPrefabs = new List<Prefab>();
		
		foreach(Object rawObject in rawObjects)
			rawPrefabs.Add ((rawObject as GameObject).GetComponent<Prefab>());

		foreach (var prefab in rawPrefabs) {

			if(!prefabs.ContainsKey(prefab.id)) {
				prefab.Check ();
				prefabs.Add(prefab.id, prefab);

			}
		}

		_hasPrefabs = true;
		_count = prefabs.Count;
		if(prefabs.Count > 0)
			_nextId = prefabs.Keys.Max() + 1;

		return true;
	}
	
	static Dictionary<int, string> GetPrefabPaths() {
	
		var dictionary =  new Dictionary<int, string>();
		string[] allPaths = AssetDatabase.GetAllAssetPaths();
		
		foreach(string path in allPaths) {
			if(path.Contains("/Prefabs/") && path.Contains (".prefab")) {
			
				int id = int.Parse(Path.GetFileNameWithoutExtension(path));
				
				if(dictionary.ContainsKey (id)) {
					Debug.Log ("Error: Duplicate prefab id found: ");
					Debug.Log ("Duplicate path: " + dictionary[id].ToString());
					Debug.Log ("Duplicate path: " + path);
					return null;
				}
				else
					dictionary.Add ( id, path);
			}
		}
		if(dictionary.Count == 0) {
			Debug.Log ("Error: There are no prefabs to load. Failed at finding paths.");
			return null;
		}
		
		return dictionary;
	}
	
	static Dictionary<int, Object> GetPrefabObjects(Dictionary<int, string> tempPrefabPaths) {
		
		var dictionary =  new Dictionary<int, Object>();
		
		foreach(var pair in tempPrefabPaths) {
			var obj = AssetDatabase.LoadAssetAtPath(pair.Value, typeof(Object));
			dictionary.Add(pair.Key, obj);
		}
		
		return dictionary;
	}
	
	static Dictionary<int, Texture2D> GetPrefabTextures(Dictionary<int, Object> objects) {
	
		var textures = new Dictionary<int, Texture2D> ();

		foreach (var obj in objects) {

			var renderer = (obj.Value as GameObject).GetComponent<SpriteRenderer>();

			var texture = new Texture2D((int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height);
			
			texture.SetPixels (renderer.sprite.texture.GetPixels((int)renderer.sprite.rect.x, (int)renderer.sprite.rect.y, (int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height));

			texture.hideFlags = HideFlags.None;

			texture.Apply();
					
			textures.Add (obj.Key, texture);
			}

		return textures;
	} 
}
