using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class PrefabLoader 
{
	public static PrefabCollection GetPrefabCollection()
	{
		var prefabs = LoadPrefabs();
		
		if(prefabs == null)
		{
			Debug.Log ("Failed to load prefabs.");
			return null;
		}
		
		var spriteTextures = LoadSpriteTextures();
		
		if(spriteTextures == null)
		{
			Debug.Log ("Failed to load sprite textures.");
			return null;
		}
		
		return new PrefabCollection(spriteTextures, prefabs);
	}
	
	static Dictionary<int, Prefab> LoadPrefabs()
	{
		var rawPrefabObjects = Resources.LoadAll("Prefabs/");
		
		if(rawPrefabObjects.Count() <= 0)
		{
			Debug.Log ("No prefab objects could be loaded.");
			return null;
		}
		
		Dictionary<int, Prefab> prefabs = new Dictionary<int, Prefab>();
		
		foreach(var obj in rawPrefabObjects)
		{
			Prefab prefab = (obj as GameObject).GetComponent<Prefab>();
			if(!prefabs.ContainsKey(prefab.id))
			{
				prefab.OnLoad();
				prefabs.Add(prefab.id, prefab);
			}
			else
			{
				Debug.Log ("Duplicate id found. Prefab with id " + prefab.id);
				return null;
			}
		}
	
		return prefabs;
	}
	
	static Dictionary<string, Texture2D> LoadSpriteTextures()
	{
	
		var rawSpriteTextures = Resources.LoadAll ("Sprites/");
		
		if(rawSpriteTextures.Count () <= 0)
		{
			Debug.Log ("No sprites or spritesheets could be loaded.");
			return null;
		}
		
		Dictionary<string, Texture2D> spriteTextures =  new Dictionary<string, Texture2D>();
		
		foreach(var obj in rawSpriteTextures)
		{
			if(obj.GetType() == typeof(Sprite))
			{
				Sprite sprite = obj as Sprite;
				if(!spriteTextures.ContainsKey(sprite.name))
				{
					spriteTextures.Add(sprite.name, sprite.GetSpriteTexture());
				}
				else
				{
					Debug.Log ("Duplicate sprite name found. Sprite with name " + sprite.name);
					return null;
				}
			}
		}
		
		return spriteTextures;
	}
}
