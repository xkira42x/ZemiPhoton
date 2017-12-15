using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : Photon.MonoBehaviour {

	[SerializeField]Text RoomName;
	[SerializeField]Text NumberofPeople;

	string _roomName;
	int _numofPeople;
	byte _maxnumofPeople;

	public void SetRoomInfo(string name,int num,byte maxnum){
		_roomName = name;
		_numofPeople = num - 1;
		_maxnumofPeople = (byte)(maxnum - 1);

		RoomName.text = "ルーム名" + _roomName;
		NumberofPeople.text = "人数" + _numofPeople.ToString () + "/" + _maxnumofPeople.ToString ();
	}

	public void OnClickJoinButton(){
		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = _maxnumofPeople;
		// ルーム作成、もしくは参加
		PhotonNetwork.JoinOrCreateRoom (_roomName, ro, TypedLobby.Default);

		PlayerInfo.role = PlayerInfo.Client;
	}
}