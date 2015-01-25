using UnityEngine;
using UnityEditor;
using System.Collections;

public static class MapEditorPreview 
{

	static GameObject _preview;
	static SpriteRenderer _previewRenderer;
	static Sprite _previewSprite;
	static Texture2D _previewTexture;
	static float _previewTransparency = 0.5f;
	static Color _addColor = new Color(0f, 1f, 0f, _previewTransparency);
	static Color _removeColor = new Color(1f, 0f, 0f, _previewTransparency);
	
	static string floorSortingLayerName 
	{
		get { return ("Floor " + MapEditor.floor); }
	}

	static int previewSortingOrder = 32000;

	static void CreatePreviewObject() 
	{
		
		while(GameObject.Find("Preview Object") != null)
			Object.DestroyImmediate(GameObject.Find("Preview Object"));
		
		_preview = new GameObject();
		_preview.name = "Preview Object";
		_preview.hideFlags = HideFlags.HideAndDontSave;
		
		_previewTexture = new Texture2D(1,1);
		if(MapEditor.action == ClickAction.Add)
			_previewTexture.SetPixel(0,0, _addColor);
		else
			_previewTexture.SetPixel(0,0, _removeColor);
		_previewTexture.Apply();
		
		_previewSprite = Sprite.Create(_previewTexture, new Rect(0,0,1,1), new Vector2(1,0), 1f);
		
		_previewRenderer = _preview.AddComponent<SpriteRenderer>();
		_previewRenderer.sprite = _previewSprite;
		_previewRenderer.sortingLayerName = floorSortingLayerName;
		_previewRenderer.sortingOrder = previewSortingOrder;
	}
	
	public static void OnActionChanged() 
	{
		DisableIfNoPrefab();
		
		if(_preview) 
		switch(MapEditor.action) 
		{
			case ClickAction.Add : _previewRenderer.color = _addColor; 
			break;
			case ClickAction.Remove : _previewRenderer.color = _removeColor;
			break;
		}
	}
	
	public static void OnPositionChanged() 
	{
		DisableIfNoPrefab();
		
		if(!_preview)
			CreatePreviewObject();
		
		_preview.transform.position = new Vector3(MapEditor.position.x, MapEditor.position.y, MapEditor.floorHeight);
		SceneView.RepaintAll();
	}
	
	public static void OnFloorChanged() 
	{
		DisableIfNoPrefab();
		
		if(_preview)
			_previewRenderer.sortingLayerName = floorSortingLayerName;
	}
	
	static void DisableIfNoPrefab() 
	{
		if(_previewRenderer != null) 
		{
			if(PrefabManager.current == null)
				_previewRenderer.enabled = false;
			else
				_previewRenderer.enabled = true;
		}
	}
	
}
