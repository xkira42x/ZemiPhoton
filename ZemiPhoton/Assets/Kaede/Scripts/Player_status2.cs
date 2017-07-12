using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_status2 : Photon.MonoBehaviour {

	private int userid;
	private int hp = 100;

	private int userid2;
	private int hp2;

	private Text p1Text;
	private Text p2Text;

	void Start(){
		p1Text = GameObject.Find ("Suppoter1").GetComponent<Text> ();
		p2Text = GameObject.Find ("Suppoter2").GetComponent<Text> ();
		if (photonView.isMine) {
			userid = PhotonNetwork.player.ID;
		}
			
	}

	void Update(){

		p1Text.text = "自分： " + hp;
		p2Text.text = "仲間： " + hp2;


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
			//Debug.Log ("Receive: " + hp2);

		}
	}


}
