using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrefabChangeDetector : AssetPostprocessor {

	public static bool enabled = true;

	static void OnPostprocessAllAssets (
		string[] importedAssets,
		string[] deletedAssets,
		string[] movedAssets,
		string[] movedFromAssetPaths)
	{
		if (enabled) {
				Debug.Log ("Asset change detected. Reloading prefabs, and attempting to recreate instances from MapContainer. ");
				PrefabManager.Reload ();
		}
		/*
		foreach (string str in importedAssets)
		{
			Debug.Log("Reimported Asset: " + str);
			string[] splitStr = str.Split('/', '.');
			string folder = splitStr[splitStr.Length-3];
			string fileName = splitStr[splitStr.Length-2];
			string extension = splitStr[splitStr.Length-1];
			Debug.Log("File name: " + fileName);
			Debug.Log("File type: " + extension);
		}
		foreach (string str in deletedAssets)
			Debug.Log("Deleted Asset: " + str);
		for (int i=0;i<movedAssets.Length;i++)
			Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
			*/
	}
}
