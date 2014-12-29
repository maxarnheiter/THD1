using UnityEngine;
using System;
using System.Collections;

public enum ClickAction {
	Add, 
	Remove
}

public enum SelectAction {
	Current, 
	SetID,
	SetCategory,
	SetColor
}

public enum PrefabType {

	None,
	Any,
	Ground,
	Corner,
	Thing,
	Player
}

public enum PrefabCategory {

	None,
	Any,
	Trees,
	Walls,
	Water,
	Bushes,
	Statues,
	Pillar,
	Stone,
	Stairs,
	Ladders,
	Railings,
	Science,
	Ships,
	Wood,
	Road,
	Cave,
	Roofs,
	Ice,
	Grass,
	Swamp,
	Carpet,
	Desert,
	Tile
}

public enum PrefabColor {

	None,
	Any,
	White, 
	Black,
	Gray,
	Red, 
	Blue, 
	Green, 
	Yellow, 
	Purple, 
	Orange, 
	Brown, 
	Beige, 
	Pink
}

public enum Direction {

	North,
	South,
	East,
	West
}

public enum PlayerInputChoice {

	None,
	Move,
	ChangeDirection
}

