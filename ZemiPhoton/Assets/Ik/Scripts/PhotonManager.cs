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

	[SerializeField]
	GameObject[] MenuItems;

	//体力表示ＵＩ
	public GameObject suppoters;
	// 接続状況表示用テキスト
	Text ConnectResult;

	void Start(){
		ConnectResult = GameObject.Find ("ConnectResult").GetComponent<Text> ();
		// 初期接続
		ConnectPhoton ();
	}
		

	public void ConnectPhoton(){
		PhotonNetwork.ConnectUsingSettings ("v1.0");
	}

	void OnJoinedLobby ()
	{
		ConnectResult.text = "Connection success";
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
		ConnectResult.text = "";
	}
		
	public void JoinRoom(){
		PhotonNetwork.JoinRoom("user1");
	}
	private GameObject Player;
	//ルーム入室した時に呼ばれるコールバックメソッド
	void OnJoinedRoom() {

		int No = PhotonNetwork.countOfPlayersInRooms;

		// メニュー項目の削除
		foreach (GameObject g in MenuItems)	Destroy (g);
		Debug.Log ("PhotonManager OnJoinedRoom");
		GameObject.Find ("StatusText").GetComponent<Text> ().text = "OnJoinedRoom";
		// プレイヤー生成
		Vector3 Pos = initPos [No];
		Player = PhotonNetwork.Instantiate ("myPlayer", Pos,Quaternion.Euler (Vector3.zero), 0);
		// プレイヤーステータス生成
		GameObject gObj;
		gObj = Instantiate (suppoters);
		gObj.transform.parent=GameObject.Find ("Canvas").transform;

		Player.GetComponent<N2_status> ().No = No + 1;

		ConnectResult.text = "";
	}
}