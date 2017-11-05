using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class DemoPhotonManager : Photon.MonoBehaviour {

	[SerializeField]Text Message;
	[SerializeField]RoomMenuControl roomMenuControl;
	/*
	// ルーム作成のメニュー画面
	[SerializeField]GameObject NewRoomSettings;
	[SerializeField]InputField RoomName;

	[SerializeField]GameObject RoomView;
	[SerializeField]Transform Content;
	// ルーム一覧表示用のオブジェクト
	[SerializeField]GameObject item;
	*/
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

		//　ルームオプションを設定
		//RoomOptions ro = new RoomOptions ();
		//　部屋の入室最大人数
		//ro.MaxPlayers = 4;

		//PhotonNetwork.JoinOrCreateRoom ("DefaultRoom", ro, TypedLobby.Default);
	}
		
	//　部屋が更新された時の処理
	void OnReceivedRoomListUpdate() {
		Debug.Log ("部屋更新");
		/*
		//　部屋情報を取得する
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();

		//　ドロップダウンリストに追加する文字列用のリストを作成
		List<string> list = new List <string> ();

		//　部屋情報を部屋リストに表示
		foreach (RoomInfo room in rooms) {
			//　部屋が満員でなければ追加
			if (room.PlayerCount < room.MaxPlayers) {
				list.Add (room.Name);
			}
		}
		*/
		/*if (PhotonNetwork.GetRoomList ().Length == 0) {
			Debug.Log ("部屋なし");
		} else {
			foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
				GameObject obj = Instantiate (item, new Vector3 (0, 0, 0), Quaternion.identity);
				obj.GetComponent<RoomItem> ().SetRoomInfo (room.Name, room.PlayerCount, room.MaxPlayers);
				obj.transform.parent = Content;
			}
		}*/
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

	/*public void OnClickCreateANewRoomButton(){
		RoomView.SetActive (false);
		NewRoomSettings.SetActive (true);
	}*/

	// 新しくルーム作成ボタンが押された時
	/*public void OnClickNewRoomSettingsButton(){
		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 4;
		// ルーム作成、もしくは参加
		PhotonNetwork.JoinOrCreateRoom (RoomName.text, ro, TypedLobby.Default);

		NewRoomSettings.SetActive (false);
	}*/
}