using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class PrefabManager 
{
	public static Dictionary<int, GameObject> Prefabs;

	public static bool LoadPrefabs()
	{
		Prefab[] prefabs = Resources.FindObjectsOfTypeAll<Prefab> ();
		
		if(prefabs.Count() == 0) {
			Debug.Log ("Error: There are no prefabs to load");
			return false;
		}
		
		if(HasDuplicates(prefabs)) {
			Debug.Log ("Error: There are prefabs with duplicate ids.");
			return false;
		}
		
		Prefabs = new Dictionary<int, GameObject>();
		
		//foreach(Prefab prefab in prefabs)
		//	Prefabs.Add (prefab.id, prefab.gameObject);

		Prefabs = (prefabs as IEnumerable<Prefab>).ToDictionary (p => p.id, p => p.gameObject);

		return true;
	}
	

	static bool HasDuplicates(Prefab[] prefabs)
	{
		if (prefabs.GroupBy (p => p.id).Where (g => g.Count() > 1).Count () != 0)
			return true;
		
		return false;
	}
}
