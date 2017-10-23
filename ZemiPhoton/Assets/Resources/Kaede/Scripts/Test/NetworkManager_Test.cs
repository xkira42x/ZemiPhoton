using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager_Test : Photon.MonoBehaviour {

	public GameObject loginUI;		//　ログイン画面
	public GameObject roomUI;		//　ルーム作成/入室画面
	public GameObject createUI;		//　アカウント作成画面
	public GameObject playerUI;		//　プレイヤー画面

	public Dropdown roomList;		//　部屋リストを表示するドロップダウン
	public InputField roomName;		//　部屋の名前
	public InputField playerName;	//　プレイヤーの名前
	public InputField createplayerName;		//新規プレイヤーネーム

	private GameObject player;


	void Start () {

		//　ロビーに自動で入る
		PhotonNetwork.autoJoinLobby = true;

		//　ゲームのバージョン設定
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	//　マスターサーバに接続された時に呼ばれる
	void OnConnectedToMaster() {
		Debug.Log ("マスターサーバに接続");

	}

	//　ロビーに入った時に呼ばれる
	void OnJoinedLobby (){
		Debug.Log ("ロビーに入る");
		loginUI.SetActive (true);
	}

	//	アカウント作成ボタンを押した時の処理
	public void CrateAccount(){
		loginUI.SetActive (false);
		createUI.SetActive (true);
	}

	//　アカウント作成画面での処理
	public void MakingAccount(){
		loginUI.SetActive (true);
		createUI.SetActive (false);
	}

	//　ログインボタンを押した時に実行するメソッド
	public void LoginGame() {

		//プレイヤー画面UI
		playerUI.SetActive(true);

		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 4;

		if (roomName.text != "") {
			//　部屋がない場合は作って入室
			PhotonNetwork.JoinOrCreateRoom (roomName.text, ro, TypedLobby.Default);
		} else {
			//　部屋が存在すれば
			if (roomList.options.Count != 0) {
				Debug.Log (roomList.options [roomList.value].text);
				PhotonNetwork.JoinRoom (roomList.options [roomList.value].text);
				//　部屋が存在しなければDefaultRoomという名前で部屋を作成
			} else {
				PhotonNetwork.JoinOrCreateRoom ("DefaultRoom", ro, TypedLobby.Default);
			}
		}
	}

	//　部屋が更新された時の処理
	void OnReceivedRoomListUpdate() {
		Debug.Log ("部屋更新");

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

		//　ドロップダウンリストをリセット
		roomList.ClearOptions ();

		//　部屋が１つでもあればドロップダウンリストに追加
		if (list.Count != 0) {
			roomList.AddOptions (list);
		}
	}

	//　部屋に入室した時に呼ばれるメソッド
	void OnJoinedRoom() {
		loginUI.SetActive (false);
		Debug.Log ("入室");

		//　InputFieldに入力した名前を設定
		if (playerName.text != "") {
			PhotonNetwork.player.NickName = playerName.text;
		} else {
			PhotonNetwork.player.NickName = "DefaultPlayer";
		}

		//ログインを監視する
		StartCoroutine("SetPlayer",0f);
	}

	IEnumerator SetPlayer(float time){
		yield return new WaitForSeconds (time);
		//ネットワークごしにplayerをインスタンス化する
		player = PhotonNetwork.Instantiate("PlayerTest", Vector3.up, Quaternion.identity, 0);

		player.GetPhotonView ().RPC ("SetName", PhotonTargets.AllBuffered, PhotonNetwork.player.NickName);
		player.GetPhotonView ().RPC ("SetHP", PhotonTargets.AllBuffered, 100);
	}

	//　部屋の入室に失敗した
	void OnPhotonJoinRoomFailed () {
		Debug.Log("入室に失敗");

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
