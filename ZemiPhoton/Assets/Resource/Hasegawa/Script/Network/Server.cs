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
		Debug.Log("StartUp");
		GetIpAddress ();	//ホストのみの処理
		//↓ipAdress送信処理
		photonView.RPC("SyncIPAddress",PhotonTargets.OthersBuffered,ipAddress);
	}

	///自身のipAddress取得（ホストのみが行う処理）
	/// これ以降の処理（IPアドレス送信を除く)はホスト側は実行されない
	/// そのため、ホストの入力したプレイヤーの名前はDBへの接続関連の処理は行われない
	void GetIpAddress(){
		Debug.Log("GetIP");
		string hostname = Dns.GetHostName ();
		IPAddress[] adrList = Dns.GetHostAddresses (hostname);
		foreach (IPAddress address in adrList){
			ipAddress = address.ToString ();
		}

		Debug.Log(ipAddress);
		//photonView.RPC ("IpAddressSet", PhotonTargets.AllBuffered, ipAddress);
	}

	/// <summary>
	/// ここ移行はクライアント側が行う処理
	/// DB接続関連の処理が行われる
	/// </summary>
	[PunRPC]
	void SyncIPAddress(string adrs){
		Debug.Log ("SyncIP");
		// ここで送られてきた値を受け取る
		ipAddress = adrs;
		//ログイン処理実行
		LogIn_Button_Push ();
		//ConnectToServer ();
	}

	//ログイン処理
	void LogIn_Button_Push(){
		Debug.Log("LogInPush");
		Debug.Log ("ipAddress=" + ipAddress);
		ServerAddress = ipAddress + "/3zemi/DB_test_unity_select_name.php";
		StartCoroutine ("Access");	//Accessコルーチンの開始
	}

	//新規作成処理
	void NewData_Button_Push(){
		Debug.Log("NewDataPush");
		ServerAddress = ipAddress+"/3zemi/DB_test_unity_input.php";
		StartCoroutine ("Access");

	}
		
	//DBへの接続と名前の送信
	IEnumerator Access(){
		Debug.Log("Access");
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		dic.Add ("name", PlayerInfo.playerName);
		StartCoroutine(DataPost(ServerAddress,dic));
		yield return 0;
	}

	//DBへ名前送信
	IEnumerator DataPost(string url,Dictionary<string,string>post){
		Debug.Log("DataPost");
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string>post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);

			WWW www = new WWW (url, form);

			yield return StartCoroutine (CheckTimeOut (www, 10f));	//TimeOutSecond=3s

			if (www.error != null) {
				Debug.Log ("HttpPost NG: " + www.error);
				//そもそも接続ができていないとき
			}
			else if (www.isDone) 
			{//接続が成功

				//DBから送られてきた情報のbyte量で処理振り分け
				switch (www.bytesDownloaded) {
				case 8://DBに名前が存在してログイン成功
                       //ログイン時の処理がここに必要な場合以下に追記
                        float ScoreData = PlayerPrefs.GetFloat("Score");
                        if (ScoreData > 0){
                            //データベースにスコアを送信、スコアはプラスしていく
                            //ScoreDataにスコアが保存されてます。
                        }
                        PlayerPrefs.SetFloat("Score", 0);
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
					//DBから送られてくる情報が以上以外の場合
					Debug.Log ("Unknown Error");
					break;
				}

			}
		}
	}
		
	//タイムアウト処理
	IEnumerator CheckTimeOut(WWW www, float timeout) {
		float requestTime = Time.time;
		while (!www.isDone) {
			if (Time.time - requestTime < timeout) {
				yield return null;
			}
			else 
			{
				Debug.Log ("TimeOut");
				break;
			}
		}
		yield return null;
	}

    //ゲーム終了時(Timer.cs)にスコアをデータベースに送る
    public void Score()
    {
        //プレイヤーごとの撃破数
        //PlayerInfo.killCount;

        //スコアの送信

        /*if (データベースに送信できなかったら)
        {
            PlayerPrefs.SetFloat("Score", PlayerInfo.killCount);
        }*/
    }
}
