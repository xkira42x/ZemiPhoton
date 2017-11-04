using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : Photon.MonoBehaviour {

	[SerializeField]Text RoomName;
	[SerializeField]Text NumberofPeople;

	string _roomName;
	string _numofPeople;

	// Use this for initialization
	void Start () {
		gameObject.name = "Item";
	}

	public void SetRoomInfo(string name,string num){
		_roomName = name;
		_numofPeople = num;

		RoomName.text = "ルーム名" + _roomName;
		NumberofPeople.text = "人数" + _numofPeople + "/4";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickJoinButton(){
		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 4;
		// ルーム作成、もしくは参加
		PhotonNetwork.JoinOrCreateRoom (_roomName, ro, TypedLobby.Default);
	}
}
