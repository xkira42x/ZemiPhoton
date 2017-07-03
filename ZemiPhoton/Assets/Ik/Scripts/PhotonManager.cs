using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {
	private Vector3[] initPos = new Vector3[4]{
		new Vector3(5,0,5),
		new Vector3(-5,0,5),
		new Vector3(-5,0,-5),
		new Vector3(5,0,-5)
	};

	private GameObject cnvs;
	void Start(){
		cnvs = GameObject.Find ("Canvas");
	}
		

	public void ConnectPhoton(){
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}

	void OnJoinedLobby ()
	{
		Debug.Log ("PhotonManager OnJoinedLobby");
		//ボタンを押せるようにする
		GameObject.Find ("CreateRoomB").GetComponent<Button> ().interactable = true;
	}

	//ルーム作成
	public void CreateRoom(){
		string userName = "ユーザ1"+Time.time;
		string userId = "user1";
		PhotonNetwork.autoCleanUpPlayerObjects = false;
		//カスタムプロパティ
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName); //ユーザ名
		customProp.Add ("userId", userId); //ユーザID
		PhotonNetwork.SetPlayerCustomProperties(customProp);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.customRoomProperties = customProp;
		//ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
		roomOptions.customRoomPropertiesForLobby = new string[]{ "userName","userId"};
		roomOptions.maxPlayers = 3; //部屋の最大人数
		roomOptions.isOpen = true; //入室許可する
		roomOptions.isVisible = true; //ロビーから見えるようにする
		//userIdが名@前のルームがなければ作って入室、あれば普通に入室する。
		PhotonNetwork.JoinOrCreateRoom (userId, roomOptions, null);
	}
		
	public void JoinRoom(){
		PhotonNetwork.JoinRoom("user1");
	}
	private GameObject cube;
	//ルーム入室した時に呼ばれるコールバックメソッド
	void OnJoinedRoom() {
		Destroy (cnvs);
		Debug.Log ("PhotonManager OnJoinedRoom");
		GameObject.Find ("StatusText").GetComponent<Text> ().text
		= "OnJoinedRoom";
		Vector3 Pos = initPos [PhotonNetwork.countOfPlayersInRooms];

		cube = PhotonNetwork.Instantiate ("Cube", Pos,
			Quaternion.Euler (Vector3.zero), 0);
	}
}