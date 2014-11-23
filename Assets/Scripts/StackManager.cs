using UnityEngine;
using System.Collections;

public static class StackManager {

	static int _uid;
	
	public static int nextUID {
		get { 
			_uid++;
			return _uid;
		}
	}
}
