using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public static class PrefabTagManager  {


		public static void EnsureTagCompliance(Dictionary<int, string> paths, Dictionary<int, Object> objs) {

			if (paths.Count != objs.Count)
				return;
			
			for (int i = 1; i < paths.Count; i++) {

				string path = "";
				Object obj = null;
				GameObject gameObj = null;
				bool log = false;

				paths.TryGetValue(i, out path);
				objs.TryGetValue(i, out obj);
				gameObj = obj as GameObject;

				if(path.Contains("/Grounds/")) {
					if(gameObj.tag != "ground tile") {
						gameObj.tag = "ground tile";
						log = true;
					}
				}
				if(path.Contains("/Corners/")) {
					if(gameObj.tag != "ground corner") {
						gameObj.tag = "ground corner";
						log = true;
					}
				}
				if(path.Contains("/Things/") || path.Contains("/Player/")) {
					if(gameObj.tag != "thing") {
						gameObj.tag = "thing";
						log = true;
					}
				}

				if(log) 
					Debug.Log("Prefab with path " + path + " did not meet tag compliance. It has been changed.");
			}

		}
}
