using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ContinueLogin : Photon.MonoBehaviour {

	string logindatapath;

	[SerializeField]
	List<string> readtextData=new List<string>();

	[SerializeField]
	GameObject ContinUI;

	MenuManager MM;

	void Start(){
		logindatapath=Application.dataPath+"/LoginData.txt";	//保存ファイルのパス
		MM = this.gameObject.GetComponent<MenuManager> ();		//プレイヤーのスポーン用
	}
	void Update(){
		//	テスト用
		if (Input.GetKeyDown (KeyCode.K)) {
			WriteLoginData ("test:"+System.DateTime.Now.ToString());
		}
		if (Input.GetKeyDown (KeyCode.I)) {
			ReadLoginData ();
		}

	}


	//パスワードの暗号化


	//合流UIの表示/////////////////////////////////////////////////////////////////////////
	/// </summary>
	public void Continue(){
		ReadLoginData ();
//		Debug.Log ("Pname:"+PlayerInfo.playerName);
		if(readtextData[1]==PlayerInfo.playerName){			//読み込んだファイルの名前とログインした名前が一致していれば
			if(readtextData[0]!="")							//読み込んだファイルに部屋データがあれば
			ContinUI.SetActive(true);
		}
	}

	////////合流ボタン関数////////////////////////////////
	/// <summary>
	/// 部屋に戻ろうとする場合
	/// </summary>
	public void ContinButton(){

		RoomInfo[] RoomList = PhotonNetwork.GetRoomList ();//部屋一覧を取得
		//部屋の全検索
		for (int i = 0; i < RoomList.Length; i++) {
			Debug.Log (RoomList [i].Name+":"+readtextData[0]);
			if (RoomList [i].Name == readtextData[0]) {	//前回の部屋と同じ部屋名があれば
				PhotonNetwork.JoinRoom (readtextData[0]);	//その部屋に入室
				StartCoroutine("ReSpawnPlayer");
				break;
			} else {
				Debug.Log ("部屋なし");
				//部屋が無かった場合の処理
			}
		}
		WriteLoginData ("");	//使用済みなので部屋のデータを初期化
		ContinUI.SetActive(false);
	}
	IEnumerator ReSpawnPlayer(){
		yield return new WaitForSeconds (2f);	//処理が早いとロビーでこの関数が実行サれてしまうため
												//遅延を掛ける
		MM.SpawnOnePlayer();					//プレイヤーをすぽーん
	}

	/// <summary>
	/// 部屋に戻らない場合
	/// </summary>
	public void NoContinButton(){
		ContinUI.SetActive(false);
	}

	/////////////書き込み、読み込み//////////////////////
	/// <summary>
	/// 最後にログインした部屋名を保存
	/// </summary>
	/// <param name="data">保存する文字列.</param>
	public void WriteLoginData(string data){
		StreamWriter SW=new StreamWriter(data);
		SW.Write (data);
		SW.Flush ();	//書き出し残しがないかを確認
		SW.Close();		//書き出しを終了
	}

	void ReadLoginData(){
		StreamReader SR = new StreamReader (logindatapath);
		string line;
		while ((line = SR.ReadLine ()) != null) {	//１行ずつ読み込み
			readtextData.Add (line);				//リストに追加していく
		}
		SR.Close ();

		Debug.Log (readtextData[0]);
	}
}