using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2_Status: Photon.MonoBehaviour {

	PlayerStatusUI statusUI;

	float health = 100;

	public float Health {
		get{ return health; }
		set { health = value; }
	}

	public void Damage(float dmg){
		health -= dmg;
		statusUI.Health = health;
		photonView.RPC ("SyncHP", PhotonTargets.Others, health);
	}

	void Awake(){
		if (photonView.isMine) {
			statusUI = GameObject.Find ("PlayerStatusUI0").GetComponent<PlayerStatusUI> ();
			photonView.RPC ("SyncPlayerID", PhotonTargets.AllBuffered, PlayerInfo.playerNumber+1);
		}else {
			statusUI = GameObject.Find ("PlayerStatusUI" + (PlayerInfo.statusCount).ToString ()).GetComponent<PlayerStatusUI> ();
			PlayerInfo.statusCount++;
		}
	}


	[PunRPC]
	void SetName(string name) {
		statusUI.UserName = name;
	}

	[PunRPC]
	void SyncPlayerID(int id){
		gameObject.name = "Player" + id.ToString ();
	}

	[PunRPC]
	void SyncHP(short hp){
		statusUI.Health = health = hp;
	}
}
