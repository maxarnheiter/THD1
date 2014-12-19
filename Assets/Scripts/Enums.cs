using UnityEngine;
using System;
using System.Collections;

public enum ClickAction {
	Add, 
	Remove
}

public enum SelectAction {
	Current, 
	SetID
}

public enum PrefabType {
	
	None = 1,
	Ground = 2,
	Corner = 4,
	Thing = 8
}

public enum PrefabCategory {

	None = 1,
	Tree = 2,
	Wall = 4,
	Water = 8
}

public enum PrefabColor {

	None = 1,
	White = 2, 
	Black = 4,
	Gray = 8,
	Red = 16, 
	Blue = 32, 
	Green = 64, 
	Yellow = 128, 
	Purple = 256, 
	Orange = 512, 
	Brown = 1024, 
	Beige = 2048, 
	Pink = 4096
}