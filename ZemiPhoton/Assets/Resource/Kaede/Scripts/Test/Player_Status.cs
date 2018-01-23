using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : Photon.MonoBehaviour {

	private string Username;
	private int UserNo;
	private short hp = 100;

	bool find = false;

	[SerializeField]
	Text myText;
	Text CompanyText;

	void Start(){

		Username = GameObject.Find ("InputField").GetComponent<InputManager_test> ().inputValue;
		myText = GameObject.Find ("MyPlayerName").GetComponent<Text> ();


		if (photonView.isMine) {
			UserNo = PhotonNetwork.player.ID;
		} 


	}

	void Update(){

		if (!find) {

			if (photonView.isMine) {
				myText.text = Username;
			} 

		}
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (Username);
			stream.SendNext (UserNo);
		} else {
			Username = (string)stream.ReceiveNext ();
			UserNo = (int)stream.ReceiveNext ();
		}
	}


}
