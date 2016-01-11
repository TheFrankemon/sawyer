using UnityEngine;
using System.Collections;

public class PlayerInfo {

	private static string name;

	// Use this for initialization
	static PlayerInfo() {
		name = "";
	}
	
	public static string getName() {
		return name;
	}

	public static void setName(string name) {
		PlayerInfo.name = name;
	}
}
