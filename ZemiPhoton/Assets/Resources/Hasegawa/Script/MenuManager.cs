using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Photon.MonoBehaviour {

	[SerializeField]Text[] Names;
	[SerializeField]Text[] Status;
	[SerializeField]Text timerText;
	[SerializeField]GameObject[] MenuItems;
	[SerializeField]myPhotonManager photonManager;

	byte index = 0;

	public void SetName(string name){
		photonView.RPC ("SetNameText", PhotonTargets.AllBufferedViaServer, name);
	}
	[PunRPC]
	void SetNameText(string name){
		Names [index].text = name;
		index++;
	}

	public void OnClickReadyButton(){
		photonView.RPC ("Ready", PhotonTargets.AllBuffered, photonManager.No);
	}

	[PunRPC]
	void Ready(int no){
		Status[no].text = "Ready";

		bool flg = true;
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
			if (Status [i].text != "Ready") {
				flg = false;
				break;
			}
		}

		if (flg) {
			StartCoroutine ("StartTimeCount");
		}
	}

	IEnumerator StartTimeCount(){
		int time = 4;
		timerText.text = time.ToString ();
		while(true){
			yield return new WaitForSeconds (1);
			time--;
			timerText.text = time.ToString ();

			if (time < 0)
				break;
		}
		PlayerSpawn ();
	}

	void PlayerSpawn(){
		GameObject player = PhotonNetwork.Instantiate ("FPSPlayer", Vector3.up, Quaternion.identity, 0);
		player.GetPhotonView ().RPC ("SetName", PhotonTargets.AllBuffered, PlayerInfo.playerName);
		foreach (GameObject obj in MenuItems)
			obj.SetActive (false);

		if (photonView.isMine)
			PlayerInfo.Spawn = true;
	}
}
