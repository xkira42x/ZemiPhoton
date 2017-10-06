using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : Photon.MonoBehaviour {

	private string Username;

	void Start(){

		Username = GameObject.Find ("InputField").GetComponent<InputManager_test> ().inputValue;

	}

	void Update(){
		
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (Username);
		} else {
			Username = (string)stream.ReceiveNext ();
		}
	}


}
