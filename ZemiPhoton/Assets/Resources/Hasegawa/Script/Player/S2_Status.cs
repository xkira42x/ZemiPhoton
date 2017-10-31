using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2_Status: Photon.MonoBehaviour {

	PlayerStatusUI statusUI;

	[PunRPC]
	void SetName(string name) {
		statusUI.UserName = name;
	}

	short health = 100;
	public short Health {
		get{ return health; }
		set { health = value; }
	}
	public void Damage(short dmg){
		health -= dmg;
		statusUI.Health = health;
		photonView.RPC ("SetHP", PhotonTargets.Others, health);
	}
	[PunRPC]
	void SetHP(short hp){
		statusUI.Health = health = hp;
	}

	void Awake(){
		if (photonView.isMine)
			statusUI = GameObject.Find ("PlayerStatusUI0").GetComponent<PlayerStatusUI> ();
		else {
			statusUI = GameObject.Find ("PlayerStatusUI" + PlayerInfo.playerNumber.ToString ()).GetComponent<PlayerStatusUI> ();
			PlayerInfo.playerNumber++;
		}
	}
}
