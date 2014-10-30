using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class PrefabManager 
{
	static int? _current;
	public static int? current {
		get { return _current; }
		set { 	_current =  value; 
				_currentPrefab = GetPrefabById((int)_current);
				_currentTexture = GetTextureById((int)_current);
		}
	}
	
	static GameObject _currentPrefab;
	public static GameObject currentPrefab {
		get { return _currentPrefab; }
	}
	
	static Texture2D _currentTexture;
	public static Texture2D currentTexture {
		get { return _currentTexture; }
	}

	static string[] _prefabFolders = 	{
										"Prefabs/Grounds",
										"Prefabs/Player"
										};
										
	public static string[] prefabFolders {
		get { return _prefabFolders; }
	}

	static int _count;
	public static int count {
		get { return _count; }
	}

	static bool _hasPrefabs;
	public static bool hasPrefabs {
		get { return _hasPrefabs; }
	}

	static Dictionary<int, GameObject> _prefabs;

	public static Dictionary<int, GameObject> prefabs {
		get { return _prefabs; }
		set {
			if (_prefabs == null)
				 _prefabs = new Dictionary<int, GameObject> ();
			_prefabs = value; 
			}
	}

	static Dictionary<int, Texture2D> _prefabTextures;

	public static Dictionary<int, Texture2D> prefabTextures {
		get { return _prefabTextures; }
	}

	public static bool LoadPrefabs() {
	
		List<Prefab> prefabList=  new List<Prefab>();
		
		foreach(var folder in prefabFolders) {
			prefabList.AddRange(Resources.LoadAll (folder, typeof(Prefab)).Cast<Prefab>());
		}

		if(prefabList.Count == 0) {
			Debug.Log ("Error: There are no prefabs to load");
			return false;
		}
		
		if(HasDuplicates(prefabList)) {
			Debug.Log ("Error: There are prefabs with duplicate ids.");
			return false;
		}

		prefabs = prefabList.ToDictionary(p => p.id, p => p.gameObject);
		_hasPrefabs = true;
		_count = prefabs.Count();
		
		CreatePreviewTextures();

		return true;
	}
	

	static bool HasDuplicates(List<Prefab> prefabList)
	{
		if (prefabList.GroupBy (p => p.id).Where (g => g.Count() > 1).Count () != 0)
			return true;
		
		return false;
	}

	static void CreatePreviewTextures() {

		_prefabTextures = new Dictionary<int, Texture2D> ();

		foreach (var prefab in _prefabs) {

			AddPreviewTexture (prefab);
		}
	}

	static void AddPreviewTexture(KeyValuePair<int,GameObject> prefab)
	{
		if (prefab.Value.GetComponent<SpriteRenderer>() == null)
			return;
		else
		{
			var sprite = prefab.Value.GetComponent<SpriteRenderer>().sprite;
			var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);		
			
			texture.SetPixels (sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height));
			texture.hideFlags = HideFlags.DontSave;
			texture.Apply();

			prefabTextures.Add(prefab.Key, texture);
		}																		
	}
	
	public static GameObject GetPrefabById(int id) {
		GameObject prefab = null;
		prefab = _prefabs.Where(p => p.Key == id).FirstOrDefault().Value;
		return prefab;
	}
	
	public static Texture2D GetTextureById(int id) {
		Texture2D texture = null;
		texture = _prefabTextures.Where (p => p.Key == id).FirstOrDefault().Value;
		return texture;
	}
}
