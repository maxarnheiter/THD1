using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class PrefabManager 
{
	static int _current;
	public static int current {
		get { return _current; }
		set { 	_current =  value; 
				_currentPath = prefabPaths[_current];
				_currentObject = prefabObjects[_current];
				_currentTexture = prefabTextures[_current];
				MapEditor.action = ClickAction.Add;
		}
	}
	
	public static string currentPath {
		get { return _currentPath; }
		set { _currentPath = value; }
	}
	static string _currentPath;
	
	public static Object currentObject {
		get { return _currentObject; }
		set { _currentObject =  value; }
	}
	static Object _currentObject;
	
	public static Texture2D currentTexture {
		get { return _currentTexture; }
		set { _currentTexture = value; }
	}
	static Texture2D _currentTexture;
	
	static Dictionary<int, string> _prefabPaths;
	public static Dictionary<int, string> prefabPaths {
		get {return _prefabPaths ?? (_prefabPaths = new Dictionary<int, string>()); }
	}
	
	static Dictionary<int, Object> _prefabObjects;
	public static Dictionary<int, Object> prefabObjects {
		get {return _prefabObjects ?? (_prefabObjects = new Dictionary<int, Object>());}
	}
	
	static Dictionary<int, Texture2D> _prefabTextures;
	public static Dictionary<int, Texture2D> prefabTextures {
		get {return _prefabTextures ?? (_prefabTextures =  new Dictionary<int, Texture2D>());}
	}
	
	public static int currentTextureWidth {
		get {
			if(currentTexture != null)
				return currentTexture.width;
			return 32;
		}
	}
	
	public static int currentTextureHeight {
		get {
			if(currentTexture != null)
				return currentTexture.height;
			return 32;
		}
	}

	static int _count;
	public static int count {
		get { return _count; }
	}
	
	static int _nextId;
	public static int nextId {
		get { return _nextId; }
	}

	static bool _hasPrefabs;
	public static bool hasPrefabs {
		get { return _hasPrefabs; }
	}
	
	static bool _triedToLoad;
	
	public static void TryLoadOnce() {
		if(!_triedToLoad) {
			Load ();
			_triedToLoad = true;
		}
	}
	
	static void Clear() {
		_count = 0;
		_prefabPaths =  new Dictionary<int, string>();
		_prefabObjects =  new Dictionary<int, Object>();
		_prefabTextures =  new Dictionary<int, Texture2D>();
	}
	
	public static bool Reload() {
		Clear ();
		return Load();
	}

	public static bool Load() {
	
		var tempPrefabPaths = GetPrefabPaths();
		
		if(tempPrefabPaths == null) {
			return false;
		}

		var tempPrefabObjects = GetPrefabObjects(tempPrefabPaths);

		TagManager.EnsureTagCompliance (tempPrefabPaths, tempPrefabObjects);
		
		if(tempPrefabObjects == null) {
			Debug.Log ("Error: Something went wrong when loading assets from paths.");
			return false;
		}
		var tempPrefabTextures = GetPrefabTextures(tempPrefabObjects);
		
		if(tempPrefabTextures == null) {
			Debug.Log ("Error: Something went wrong when loading textures from paths.");
			return false;
		}
		
		_prefabPaths = tempPrefabPaths;
		_prefabObjects = tempPrefabObjects;
		_prefabTextures = tempPrefabTextures;
		
		_hasPrefabs = true;
		_count = prefabObjects.Count;
		_nextId = prefabObjects.Keys.Max() + 1;

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

			texture.Apply();
					
			textures.Add (obj.Key, texture);
			}

		return textures;
	} 
}
