using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class Prefab : MonoBehaviour {

	public int id;
	public int setId;

	public Texture2D texture;
	public int width;
	public int height;

	public bool isPrimary;
	public bool isAlt;
	
	
	public PrefabType prefabType;
	
	public PrefabCategory prefabCategory = PrefabCategory.None;
	
	public PrefabColor prefabColor = PrefabColor.None;

	public void Check() {

		if (id != int.Parse (this.gameObject.name))
			id = int.Parse (this.gameObject.name);

		if (texture == null) 
			SetTexture ();

		if (width != texture.width)
			width = texture.width;

		if (height != texture.height)
			height = texture.height;
		
		CheckPrefabType();

	}

	void SetTexture() {

		var renderer = this.GetComponent<SpriteRenderer>();

		this.texture = new Texture2D ((int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height);
	
		this.texture.SetPixels (renderer.sprite.texture.GetPixels ((int)renderer.sprite.rect.x, (int)renderer.sprite.rect.y, (int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height));

		this.texture.Apply ();
	}
	
	void CheckPrefabType()
	{
		if(this.gameObject.tag == "ground tile" && this.prefabType != PrefabType.Ground)
			this.prefabType = PrefabType.Ground;
			
		if(this.gameObject.tag == "ground corner" && this.prefabType != PrefabType.Corner)
			this.prefabType = PrefabType.Corner;
			
		if(this.gameObject.tag == "thing" && this.prefabType != PrefabType.Thing)
			this.prefabType = PrefabType.Thing;
	}

	 
	
}
