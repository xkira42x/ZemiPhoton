using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager_ver2 : Photon.MonoBehaviour {

	private GameObject cube;

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
		string userName = "ユーザ1";
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
		roomOptions.maxPlayers = 8; //部屋の最大人数
		roomOptions.isOpen = true; //入室許可する
		roomOptions.isVisible = true; //ロビーから見えるようにする
		//userIdが名前のルームがなければ作って入室、あれば普通に入室する。
		PhotonNetwork.JoinOrCreateRoom (userId, roomOptions, null);
	}
	public void JoinRoom(){
		PhotonNetwork.JoinRoom("user1");
	}


	//ルーム入室した時に呼ばれるコールバックメソッド
	void OnJoinedRoom() {
		Debug.Log ("PhotonManager OnJoinedRoom");

		Vector3 initPos = new Vector3 (0, 3f, 3f);
		cube = PhotonNetwork.Instantiate ("Player", initPos,
			Quaternion.Euler (Vector3.zero), 0);

		GameObject.Find ("Main Camera").GetComponent<Camera>().enabled = false;
		GameObject.Find ("FirstPersonCharacter").GetComponent<Camera>().enabled = true;

	}

}