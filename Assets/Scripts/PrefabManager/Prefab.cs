using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class Prefab : MonoBehaviour {

	public int id;
	public int setId;

	public string spriteName;
	public int width;
	public int height;

	public bool isPrimary;
	public bool isAlt;
	
	public PrefabType prefabType = PrefabType.None;
	public PrefabCategory prefabCategory = PrefabCategory.None;
	public PrefabColor prefabColor = PrefabColor.None;

	public void OnLoad()
	{
		var renderer = this.gameObject.GetComponent<SpriteRenderer>();
		
		if(renderer == null)
			return;
			
		if(renderer.sprite == null)
			return;
		
		this.spriteName = renderer.sprite.name;
		this.width = (int)renderer.sprite.rect.width;
		this.height = (int)renderer.sprite.rect.height;
	}
	
}
