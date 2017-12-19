using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : Photon.MonoBehaviour {

	public static List<GameObject> Player = new List<GameObject>();

	public static void AddPlayerList(string name){
		Player.Add (GameObject.Find (name));
	}

	public static GameObject GetPlayerList_Shuffle(){
		int index = Random.Range (0, Player.Count);
		return Player [index];
	}

	public static GameObject GetPlayerList(int index){
		if (0 < index || index > Player.Count)
			return Player [0];
		else
			return Player [index];
	}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
