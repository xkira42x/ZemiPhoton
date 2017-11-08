using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class DemoPhotonManager : Photon.MonoBehaviour {

	[SerializeField]Text Message;
	[SerializeField]RoomMenuControl roomMenuControl;

	void Start () {

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
		//Debug.Log ("入室");
		Message.text = "Welcome to room " + PhotonNetwork.room.Name;
	}

	//　部屋の入室に失敗した
	void OnPhotonJoinRoomFailed () {
		//Debug.Log("入室に失敗");

		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 4;
		//　入室に失敗したらDefaultRoomを作成し入室
		PhotonNetwork.JoinOrCreateRoom ("DefaultRoom", ro, TypedLobby.Default);
	}

}