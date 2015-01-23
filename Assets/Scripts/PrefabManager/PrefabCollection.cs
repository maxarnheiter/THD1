using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PrefabCollection  
{
	Dictionary<string, Texture2D> _spriteTextures;
	public Dictionary<string, Texture2D> spriteTextures
	{ get { return _spriteTextures; } }
	
	Dictionary<int, Prefab> _prefabs;
	public Dictionary<int, Prefab> prefabs
	{ get { return _prefabs; } }

	public PrefabCollection(Dictionary<string, Texture2D> spriteTextures, Dictionary<int, Prefab> prefabs)
	{
		this._spriteTextures = spriteTextures;
		this._prefabs = prefabs;
	}	
	
	public Prefab GetPrefab(int id)
	{
		Prefab prefab = null;
		prefabs.TryGetValue(id, out prefab);
		return prefab;
	}
	
	public Texture2D GetTexture(string name)
	{
		Texture2D texture = null;
		spriteTextures.TryGetValue(name, out texture);
		return texture;
	}
}
