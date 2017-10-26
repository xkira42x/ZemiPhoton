using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class PhotonManager : Photon.MonoBehaviour {
/*	private Vector3[] initPos = new Vector3[4]{
		new Vector3(5,0,5),
		new Vector3(-5,0,5),
		new Vector3(-5,0,-5),
		new Vector3(5,0,-5)
	};
*/
//	[SerializeField]string roomName = "myRoomName";

	[SerializeField]
	GameObject[] MenuItems;

	//体力表示ＵＩ
//	public GameObject suppoters;
	// 接続状況表示用テキスト
	Text ConnectResult;
	// 入室中判定
	public static bool EnteringTheRoom = false;

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


		string hostname = Dns.GetHostName ();
		IPAddress[] adrList = Dns.GetHostAddresses (hostname);
		foreach (IPAddress address in adrList){
			ipAddress = address.ToString ();
		}
		ServerAddress = ipAddress+"/3zemi/DB_test_unity_input.php";
		//Debug.Log (ipAddress);
		StartCoroutine ("DataAccess");

	}

	public Text UserMessage;
	public Text UserName;
	public Text NewPass;
	public Text Button_Message;

	//public int Button_flg=0;

	//string ServerAddress = "localhost/3zemi/DB_test_unity_input.php";

//	string ipAddress;
//	string ServerAddress;
	//string ServerAddress = "10.22.1.156/3zemi/DB_test_unity_input.php";

	private IEnumerator DataAccess(){


		Dictionary<string,string> dic = new Dictionary<string,string> ();

		dic.Add ("name", UserName.GetComponent<Text> ().text);
		//dic.Add ("pass", NewPass.GetComponent<Text> ().text);
		StartCoroutine(DataPost(ServerAddress,dic));

		yield return 0;
	}

	private IEnumerator DataPost(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string>post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);

			WWW www = new WWW (url, form);

			yield return StartCoroutine (CheckTimeOut (www, 3f));	//TimeOutSecond=3s

			if (www.error != null) {
				UserMessage.GetComponent<Text> ().text = "ConnectingError";
				Debug.Log ("HttpPost NG: " + www.error);
				//そもそも接続ができていないとき

				loginUI.SetActive (true);
				createUI.SetActive (false);

			} else if (www.isDone) {
				if (www.bytesDownloaded != 7) {
					UserMessage.GetComponent<Text> ().text = "Registration Complete";

					loginUI.SetActive (true);
					createUI.SetActive (false);

				} else {
					UserMessage.GetComponent<Text> ().text = "Sorry\nPlease other name";
				}
			}
		}
	}

	private IEnumerator CheckTimeOut(WWW www, float timeout) {
		float requestTime = Time.time;
		while (!www.isDone) {
			if (Time.time - requestTime < timeout)
				yield return null;
			else {
				UserMessage.GetComponent<Text>().text="TimeOut";  //タイムアウト
				//タイムアウト処理
				//
				//
				break;
			}
		}
		yield return null;
	}


	//　ログインボタンを押した時に実行するメソッド
	public void LoginGame() {
		string hostname = Dns.GetHostName ();
		IPAddress[] adrList = Dns.GetHostAddresses (hostname);
		foreach (IPAddress address in adrList){
			ipAddress = address.ToString ();
		}
		ServerAddress = ipAddress+"/3zemi/DB_test_unity_select_name.php";
		StartCoroutine ("Access");	//Accessコルーチンの開始


	}
	public Text ResultText_;	//結果格納用テキスト
	public Text InputName;		//IDを入力するインプットフィールド
	string ipAddress;
	string ServerAddress;

	private IEnumerator Access(){

		Dictionary<string,string> dic = new Dictionary<string,string> ();

		//インプットフィールドからIDの所得
		dic.Add("name",InputName.GetComponent<Text>().text);
		//dic.Add ("pass", InputPass.GetComponent<Text> ().text);
		//複数phpに送信したいデータがある場合はdic.Add("hoge",value)のように足す

		StartCoroutine (Post (ServerAddress, dic));//POST
		yield return 0;
	}

	private IEnumerator Post(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string> post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);
		}
		WWW www = new WWW (url, form);

		yield return StartCoroutine (CheckTimeOut (www, 3f));	//TimeOutSecond=3s

		if (www.error != null) {
			ResultText_.GetComponent<Text>().text="ConnectingError";
			Debug.Log("HttpPost NG: " + www.error);
			//そもそも接続ができていないとき

		} else if (www.isDone) {

			//送られてきたデータをテキストに反映
			ResultText_.GetComponent<Text> ().text = www.text;
			//デバッグ用(PHPから送られるデータ量の確認)
			//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();
			//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();

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
		player = PhotonNetwork.Instantiate("FPSPlayer", Vector3.up, Quaternion.identity, 0);

		player.GetPhotonView ().RPC ("SetName", PhotonTargets.AllBuffered, PhotonNetwork.player.NickName);
//		player.GetPhotonView ().RPC ("SetHP", PhotonTargets.AllBuffered, 100);

		// メニュー項目の削除
		foreach (GameObject g in MenuItems)	Destroy (g);

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

	public void GameStart(){
		EnteringTheRoom = true;
	}
	void Update(){
		if(Input.GetKeyUp(KeyCode.P)){
			GameStart ();
		}
	}
/*	void Start(){
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
		ConnectResult.text = "";
	}
	public void JoinRoom(){
		PhotonNetwork.JoinRoom (roomName);//"user1");
	}
	private GameObject Player;
	//ルーム入室した時に呼ばれるコールバックメソッド
	void OnJoinedRoom() {
//		int No = PhotonNetwork.countOfPlayersInRooms;
		int No= PhotonNetwork.player.ID;
		// メニュー項目の削除
		foreach (GameObject g in MenuItems)	Destroy (g);
		Debug.Log ("PhotonManager OnJoinedRoom");
		//GameObject.Find ("StatusText").GetComponent<Text> ().text = "OnJoinedRoom";
		// プレイヤー生成
		Vector3 Pos = initPos [No-1];
		Player = PhotonNetwork.Instantiate ("FPSPlayer", Pos,Quaternion.Euler (Vector3.zero), 0);
		Player.name = "Player" + No.ToString ();
		// プレイヤーステータス生成
		GameObject gObj;
		gObj = Instantiate (suppoters);
		gObj.transform.parent=GameObject.Find ("Canvas").transform;

		Player.GetComponent<N2_status> ().No=No;

		ConnectResult.text = "";

	}
*/

}