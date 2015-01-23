using UnityEngine;
using UnityEditor;
using System.Collections;

//The job of this class is to keep the prefab manager up-to-date, when there 
//is some kind of change to our assets, regardless of what the change might be. 

public class AssetChangeDetector : AssetPostprocessor {

	static bool enabled = true;
	
	public static void Enable()
	{
		enabled = true;
	}
	
	public static void Disable()
	{
		enabled = false;
	}

	static void OnPostprocessAllAssets (string[] imported, string[] deleted, string[] moved, string[] movedFrom)
	{
		if (enabled) {
			Debug.Log ("Asset change detected. Reloading prefabs, and attempting to recreate instances from MapContainer. ");
			PrefabManager.Load ();
		}
	}
}
