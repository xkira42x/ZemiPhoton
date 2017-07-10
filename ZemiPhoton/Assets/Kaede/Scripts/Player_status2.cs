using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_status2 : Photon.MonoBehaviour {

	private int userid;
	private int hp = 100;

	private int userid2;
	private int hp2;

	void Start(){

		if (photonView.isMine) {
			userid = PhotonNetwork.player.ID;
		}
			
	}

	void Update(){

		GameObject.Find ("Suppoter1").GetComponent<Text> ().text = "自分： " + hp;
		GameObject.Find ("Suppoter2").GetComponent<Text> ().text = "仲間： " + hp2;


		if (Input.GetKeyUp (KeyCode.B)) {
			hp -= 10;
		}



	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (userid);
			stream.SendNext (hp);
		} else {
			userid2 = (int)stream.ReceiveNext ();
			hp2 = (int)stream.ReceiveNext ();

		}
	}


}
