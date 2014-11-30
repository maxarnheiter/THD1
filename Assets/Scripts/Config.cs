using UnityEngine;
using System.Collections;

public static class Config {

	public static Vector3 DEFAULT_ROTATION {
		get {
			return new Vector3(-0.001f, -0.001f, 0f);
		}
	}

	public static Color EDITOR_FLOOR_COLOR {
		get {
			return new Color(1f, 1f, 1f, 0.5f);
		}
	}
	
	//Corner Tile Sorting Order Base
	public const int CORNER_SORT_BASE = 1;
	
	//Thing Object Sorting Order Base
	public const int THING_SORT_BASE = 5000;

	//The amount by which we adjust the camera from a floor.
	public const float FLOOR_CAM_ADJUST = -0.9f;
}
