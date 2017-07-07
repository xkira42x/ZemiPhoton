using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : Photon.MonoBehaviour {

	private int userid;
	private int hp = 100;

	private int ouserid;
	private int ohp;

	private int[,] supporter = new int[4,2];


	void Start(){

		if (photonView.isMine) {
			userid = PhotonNetwork.player.ID;

			for (int i = 0; i < 4; i++) {
				supporter [i, 0] = -1;
				supporter [i, 1] = -1;
			}
		}

	}

	void Update(){

		if (photonView.isMine) {
/*			switch (userid) {
			case 1:
				for (int i = 1; i < 4; i++) {
					if (supporter [i, 0] != -1) {
						GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
					} else {
						GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "";
					}
				}
				break;
			case 2:
				for (int i = 0; i < 4; i++) {
					if (supporter [i, 0] != -1) {
						if (i == 0) {
							GameObject.Find ("Suppoter" + (i + 1)).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
						} else if (i > 1) {
							GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
						} 
					} else {
						GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "";
					}
				}
				
				break;
			case 3:
				for (int i = 0; i < 4; i++) {
					if (supporter [i, 0] != -1) {
						if (i < 2) {
							GameObject.Find ("Suppoter" + (i + 1)).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
						} else if (i == 3) {
							GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
						}
					} else {
						GameObject.Find ("Suppoter" + i).GetComponent<Text> ().text = "";
					}
				}

				break;
			case 4:
				for (int i = 0; i < 3; i++) {
					if (supporter [i, 0] != -1) {
						GameObject.Find ("Suppoter" + (i + 1)).GetComponent<Text> ().text = "ユーザ" + supporter [i, 0] + ":" + supporter [i, 1];
					} else {
						GameObject.Find ("Suppoter" + (i + 1)).GetComponent<Text> ().text = "";
					}
				}
				
				break;
			default:
				break;
			}
*/
		}

		if (Input.GetKey (KeyCode.A)) {
			for (int j = 0; j < 4; j++) {
				Debug.Log (supporter [j, 0]);
			}
		}

		Debug.Log ("receive");
		Debug.Log (supporter [3,0]);


	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (userid);
			stream.SendNext (hp);
		} else {
			ouserid = (int)stream.ReceiveNext ();
			ohp = (int)stream.ReceiveNext ();

			for (int i = 0; i < 4; i++) {
				if (ouserid == i + 1) {
					supporter [i, 0] = ouserid;
					supporter [i, 1] = ohp;
				}
			}
		}
	}


}
