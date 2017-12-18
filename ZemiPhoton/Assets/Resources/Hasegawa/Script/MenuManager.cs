using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Photon.MonoBehaviour {

	[SerializeField]Text[] Names;				// ユーザ名
	[SerializeField]Text[] Status;				// 準備ステート
	[SerializeField]Text timerText;				// スタートカウントダウン
	[SerializeField]GameObject[] MenuItems;		// メニュー画面の一覧

	byte index = 0;				// ユーザ名番号
	bool doOnce_Ready = false;	// 準備判定

	/// ユーザ名の設定と同期
	public void SetName(string name){
		photonView.RPC ("SetNameText", PhotonTargets.AllBufferedViaServer, name);
	}
	/// ユーザ名同期（受信）
	/// 一覧に順次名前を更新する
	[PunRPC]
	void SetNameText(string name){
		Names [index].text = name;
		if (PlayerInfo.playerName == name)
			PlayerInfo.playerNumber = index;
		index++;
	}

	/// 
	public void OnClickReadyButton(){
		if (!doOnce_Ready) {
			photonView.RPC ("Ready", PhotonTargets.AllBuffered, PlayerInfo.playerNumber);
			Debug.Log (PlayerInfo.playerNumber);
			doOnce_Ready = true;
		}
	}

	[PunRPC]
	void Ready(int no){
		Status[no].text = "Ready";

		bool flg = true;
		for (int i = 0; i < PhotonNetwork.playerList.Length - 1; i++) {
			if (Status [i].text != "Ready") {
				flg = false;
				break;
			}
		}

		if (flg && PlayerInfo.isClient ()) {
			StartCoroutine ("StartTimeCount");
		} else if(flg){
			PlayerInfo.Spawn = true;
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
		PlayerInfo.onTimer = true;
	}

	void PlayerSpawn(){
		GameObject player = PhotonNetwork.Instantiate ("FPSPlayer", Vector3.up, Quaternion.identity, 0);
		player.name = "Player" + (PlayerInfo.playerNumber+1).ToString();
		player.GetPhotonView ().RPC ("SetName", PhotonTargets.AllBuffered, PlayerInfo.playerName);
		foreach (GameObject obj in MenuItems)
			obj.SetActive (false);

		if (photonView.isMine)
			PlayerInfo.Spawn = true;
	}
}
