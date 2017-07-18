using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Photon.MonoBehaviour {

	private int enemyid;
	private bool deadflg = false;

	private PhotonView m_photonView;

	void Start(){
		m_photonView = GetComponent<PhotonView>();
	}

	void Update(){
		enemyid = m_photonView.ownerId;

		if (deadflg == true) {
			Destroy (gameObject);
		}

	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (enemyid);
			stream.SendNext (deadflg);
		} else {
			enemyid = (int)stream.ReceiveNext ();
			deadflg = (bool)stream.ReceiveNext ();


		}
	}

	void OnCollisionEnter (Collision collision){

		if (collision.gameObject.tag == "Player") {
			deadflg = true;
		}

	}

}
