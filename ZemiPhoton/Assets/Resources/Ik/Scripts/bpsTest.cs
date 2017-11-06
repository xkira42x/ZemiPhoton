using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bpsTest : Photon.MonoBehaviour {
	[SerializeField]
	int maxspn=10;
	[SerializeField]
	int addmaxspn=1;

	[SerializeField]
	GameObject testobj;

	[SerializeField]string roomName = "myRoomName";
	// 接続状況表示用テキスト
	Text ConnectResult;

	// Use this for initialization
	void Start () {
		ConnectResult = GameObject.Find ("ConnectResult").GetComponent<Text> ();
		// 初期接続
		ConnectPhoton ();
		StartCoroutine ("ConnectPhotonTimeWait");
	}
	IEnumerator ConnectPhotonTimeWait(){
		yield return new WaitForSeconds (2f);
		//ルーム作成
		CreateRoom();
		if (PhotonNetwork.room == null) {
		}else{
			PhotonNetwork.JoinRoom (roomName);
		}
		Debug.Log ("ConnetState:" + PhotonNetwork.connectionState);
	}
	public void ConnectPhoton(){
		PhotonNetwork.ConnectUsingSettings ("v1.0");
	}

	//ルーム作成
	public void CreateRoom(){
		Debug.Log ("Create room");
		string userName = "ユーザ1"+Time.time;
		string userId = "user1";
		PhotonNetwork.autoCleanUpPlayerObjects = false;
		//カスタムプロパティ
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName); //ユーザ名
		customProp.Add ("userId", userId); //ユーザID
		PhotonNetwork.SetPlayerCustomProperties(customProp);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.CustomRoomProperties = customProp;
		//ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
		roomOptions.CustomRoomPropertiesForLobby = new string[]{ "userName","userId"};
		roomOptions.MaxPlayers = 3; //部屋の最大人数
		roomOptions.IsOpen = true; //入室許可する
		roomOptions.IsVisible = true; //ロビーから見えるようにする
		//userIdが名@前のルームがなければ作って入室、あれば普通に入室する。
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, null);
	}
	void OnJoinedRoom() {
		Debug.Log ("PhotonManager OnJoinedRoom");
		if(PhotonNetwork.isMasterClient)
		spn ();
	}
	void spn(){
		GameObject obj;
		Vector3 spnpos;
		for (int i=0;i < maxspn; i++) {
			for (int j=0; j < maxspn; j++) {
				if (GameObject.Find ("testobj" + i + "_" + j)==null||
					GameObject.Find ("testobj" + i + "_" + j).name != "testobj" + i + "_" + j) {
					spnpos = new Vector3 (i, 0, j);
					obj = PhotonNetwork.Instantiate (testobj.name, spnpos, Quaternion.Euler (Vector3.zero), 0);
					obj.GetComponent<bpstestobjscript>().name ="testobj" + i + "_" + j;
				}

			}
		}
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.S)) {
			maxspn += addmaxspn;
			spn ();
			Debug.Log ("現在の生成量:" + maxspn*maxspn);
		}
		ConnectResult.text="ConnetState:" + PhotonNetwork.connectionState;
	}
}
