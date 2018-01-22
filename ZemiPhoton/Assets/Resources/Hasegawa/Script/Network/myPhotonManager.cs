using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class myPhotonManager : Photon.MonoBehaviour {

	[SerializeField]GameObject menu;
	[SerializeField]RoomMenuControl roomMenuControl;

	void Start () {
		Cursor.lockState = CursorLockMode.None;
		//　ロビーに自動で入る
		PhotonNetwork.autoJoinLobby = true;
		//　ゲームのバージョン設定
		PhotonNetwork.ConnectUsingSettings ("v0.1");
	}

	//　マスターサーバに接続された時に呼ばれる
	void OnConnectedToMaster() {
		Debug.Log ("マスターサーバに接続");
	}

	//　ロビーに入った時に呼ばれる
	void OnJoinedLobby (){
		Debug.Log ("ロビーに入る");
	}

	//　部屋が更新された時の処理
	void OnReceivedRoomListUpdate() {
		Debug.Log ("部屋更新");

		roomMenuControl.DisplayRoomList ();
	}

	//　部屋に入室した時に呼ばれるメソッド
	void OnJoinedRoom() {
		Debug.Log ("入室");
		if (PlayerInfo.isClient ()) {
			Debug.Log ("クライアント入室");
			PlayerInfo.playerNumber = PhotonNetwork.player.ID - 1;
			gameObject.GetComponent<MenuManager> ().SetName (PlayerInfo.playerName);
			menu.SetActive (true);
			Cursor.lockState = CursorLockMode.Confined;
			roomMenuControl.JoinRoom ();
//			Debug.Log()
		} else {
			GetComponent<Server> ().StartUp ();
		}
	}

	//　部屋の入室に失敗した
	void OnPhotonJoinRoomFailed () {
		Debug.Log("入室に失敗");

		/*//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 4;
		//　入室に失敗したらDefaultRoomを作成し入室
		PhotonNetwork.JoinOrCreateRoom ("DefaultRoom", ro, TypedLobby.Default);*/
	}
}