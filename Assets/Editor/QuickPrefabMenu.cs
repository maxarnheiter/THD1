using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class QuickPrefabMenu  {

	[MenuItem("Assets/Create Ground")]
	static void createGround()
	{
		processSelection ("ground tile");
	}
	
	[MenuItem("Assets/Create Corner")]
	static void createCorner()
	{
		processSelection ("ground corner");
	}

	[MenuItem("Assets/Create Thing")]
	static void createThing()
	{
		processSelection ("thing");
	}

	static void processSelection(string tag) {

		int nextId = PrefabManager.nextId;

		PrefabChangeDetector.enabled = false;
		foreach (var obj in Selection.objects) {
			var newObj = createStandardPrefab(nextId.ToString(), tag, obj as Sprite);

			if(tag == "ground corner" || tag == "thing")
				addDefaultBoxCollider2D(newObj);

			saveToFolder(newObj, tag, nextId.ToString());

			nextId++;
		}
		PrefabManager.Reload ();
		PrefabChangeDetector.enabled = true;
	}

	static void saveToFolder(GameObject obj, string tag, string name) {

		if(tag == "ground tile")
			PrefabUtility.CreatePrefab ("Assets/Resources/Prefabs/Grounds/" + name + ".prefab", obj);
		if(tag == "ground corner")
			PrefabUtility.CreatePrefab ("Assets/Resources/Prefabs/Corners/" + name + ".prefab", obj);
		if(tag == "thing")
			PrefabUtility.CreatePrefab ("Assets/Resources/Prefabs/Things/" + name + ".prefab", obj);
	}

	static GameObject createStandardPrefab(string name, string tag, Sprite sprite) {

		var obj = new GameObject();

		obj.name = name;
		obj.tag = tag;
		var renderer = obj.AddComponent<SpriteRenderer>();
		renderer.sprite = sprite;
		obj.AddComponent<Stack>();
		var prefab = obj.AddComponent<Prefab>();
		prefab.id = int.Parse(name);

		return obj;
	}

	static GameObject addDefaultBoxCollider2D(GameObject obj) {

		var box = obj.AddComponent<BoxCollider2D> ();
		box.size = new Vector2 (1f, 1f);
		box.center = new Vector2(-0.5f, 0.5f);
		return obj;
	}
}
