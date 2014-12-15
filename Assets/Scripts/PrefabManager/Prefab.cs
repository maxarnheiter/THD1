using UnityEngine;
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

	public void Check() {

		if (id != int.Parse (this.gameObject.name))
			id = int.Parse (this.gameObject.name);

		if (texture == null) 
			SetTexture ();

		if (width != texture.width)
			width = texture.width;

		if (height != texture.height)
			height = texture.height;
	}

	void SetTexture() {

		var renderer = this.GetComponent<SpriteRenderer>();

		this.texture = new Texture2D ((int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height);
	
		this.texture.SetPixels (renderer.sprite.texture.GetPixels ((int)renderer.sprite.rect.x, (int)renderer.sprite.rect.y, (int)renderer.sprite.rect.width, (int)renderer.sprite.rect.height));

		this.texture.Apply ();
	}

	 
	
}
