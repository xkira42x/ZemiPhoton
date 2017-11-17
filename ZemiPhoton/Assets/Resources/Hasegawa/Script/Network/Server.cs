using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class Server : Photon.MonoBehaviour {

	//-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
	// 型枠だけを作った感じです。追加/改変OK
	// サーバー接続→ログイン・新規作成の流れをここに記述してください
	//-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
	[System.NonSerialized]public string ipAddress;

	string ServerAddress;	//サーバアドレス格納用

	void Start () {
		
	}
	
	void Update () {
		
	}

	/// <summary>
	/// サーバーとして接続した時に、呼ばれるメソッド
	/// </summary>
	public void StartUp(){
		// ここにデータベースのIPアドレスを同期する処理を書く
		// ipAddress = GetIPAddress(); // みたいな？

		GetIpAddress ();	//ホストのみの処理
		//↓ipAdress送信処理
		photonView.RPC("SyncIPAddress",PhotonTargets.OthersBuffered,ipAddress);
	}

	//自身のipAddress取得（ホストのみが行う処理）
	void GetIpAddress(){
		string hostname = Dns.GetHostName ();
		IPAddress[] adrList = Dns.GetHostAddresses (hostname);
		foreach (IPAddress address in adrList){
			ipAddress = address.ToString ();
		}
		photonView.RPC ("IpAddressSet", PhotonTargets.AllBuffered, ipAddress);
	}

	[PunRPC]
	void SyncIPAddress(string adrs){
		// ここで送られてきた値を受け取る
		ipAddress = adrs;
		ConnectToServer ();
	}

	void ConnectToServer(){
		// ここでサーバー接続とログイン・新規作成をする
		// PlayerInfo.playerName; // プレイヤーの名前を格納している
		LogIn_Button_Push();
	}

	void LogIn_Button_Push(){//ログイン処理
		ServerAddress = DBManager.ipAddress + "/3zemi/DB_test_unity_select_name.php";
		StartCoroutine ("Access");	//Accessコルーチンの開始
	}

	void NewData_Button_Push(){//新規作成処理
		ServerAddress = DBManager.ipAddress+"/3zemi/DB_test_unity_input.php";
		StartCoroutine ("Access");
	}

	IEnumerator Access(){
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		dic.Add ("name", PlayerInfo.playerName);
		//dic.Add ("pass", NewPass.GetComponent<Text> ().text);
		StartCoroutine(DataPost(ServerAddress,dic));

		yield return 0;
	}

	IEnumerator DataPost(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string>post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);

			WWW www = new WWW (url, form);

			yield return StartCoroutine (CheckTimeOut (www, 3f));	//TimeOutSecond=3s

			if (www.error != null) {
				Debug.Log ("HttpPost NG: " + www.error);
				//そもそも接続ができていないとき

			} else if (www.isDone) {//接続が成功

				switch (www.bytesDownloaded) {
				case 8://DBに名前が存在してログイン成功
					//ログイン時の処理がここに必要な場合以下に追記
					break;
				case 9://DBに名前が存在せず、ログイン失敗－＞新規作成の関数を実行
					NewData_Button_Push ();
					break;
				case 11://新規作成が名前の入力がないため失敗
					Debug.Log ("Not Input Name");
					break;
				case 134://新規作成完了－＞ログイン関数実行
					LogIn_Button_Push();
					break;
				default:
					Debug.Log ("Unknown Error");
					break;
				}
			}
		}
	}

	IEnumerator CheckTimeOut(WWW www, float timeout) {
		float requestTime = Time.time;
		while (!www.isDone) {
			if (Time.time - requestTime < timeout)
				yield return null;
			else {
				Debug.Log ("TimeOut");

				//UserMessage.GetComponent<Text>().text="TimeOut";  //タイムアウト
				//タイムアウト処理
				//
				//
				break;
			}
		}
		yield return null;
	}

}
