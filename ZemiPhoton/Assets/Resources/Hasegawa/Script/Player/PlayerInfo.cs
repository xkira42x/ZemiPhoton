using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	public static string playerName = "default";
	public static int playerNumber = 1;
	public static int statusCount = 1;

	public static bool Spawn = false;

	public const byte Server = 0,Client = 1;
	public static byte role;
	public static bool isClient(){return role == Client;}

}
