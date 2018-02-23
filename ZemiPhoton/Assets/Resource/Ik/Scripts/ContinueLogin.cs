using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ContinueLogin : Photon.MonoBehaviour {

	string logindatapath;

	[SerializeField]
	List<string> readtextData=new List<string>();	//エディタ上で確認できるようシリアライズ化

	[SerializeField]
	GameObject ContinUI;							//合流質問UI

	MenuManager MM;									//スポーンさせる時に使う

	void Start(){
		logindatapath=Application.dataPath+"/LoginData.txt";	//保存ファイルのパス
		MM = this.gameObject.GetComponent<MenuManager> ();		//プレイヤーのスポーン用
	}
	void Update(){
		//	テスト用
		if (Input.GetKeyDown (KeyCode.K)) {
			WriteLoginData ("test"+System.Environment.NewLine+System.DateTime.Now.ToString());
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
		ContinUI.transform.FindChild ("ContenText").GetComponent<Text> ().text = "前回のプレイデータが残っています\n合流しますか？";
		//部屋の全検索
		if (RoomList.Length > 0) {			//もしも部屋があれば
			
			for (int i = 0; i < RoomList.Length; i++) {
				Debug.Log (RoomList [i].Name + ":" + readtextData [0]);
				if (RoomList [i].Name == readtextData [0]) {	//前回の部屋と同じ部屋名があれば
					PhotonNetwork.JoinRoom (readtextData [0]);	//その部屋に入室
					StartCoroutine ("ReSpawnPlayer");
					WriteLoginData ("");	//使用済みなので部屋のデータを初期化
					ContinUI.SetActive (false);
					break;
				} else {
					Debug.Log ("部屋なし");
					ContinUI.transform.FindChild ("ContenText").GetComponent<Text> ().text = "合流する部屋がありませんでした";
					//部屋が無かった場合の処理
				}
			}
		} else {		//もしも部屋が無ければ
			Debug.Log ("部屋なし");
			ContinUI.transform.FindChild ("ContenText").GetComponent<Text> ().text = "合流する部屋がありませんでした";
		}
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
		StreamWriter SW=new StreamWriter(logindatapath,false);
		SW.WriteLine (data);
		SW.Flush ();	//書き出し残しがないかを確認
		SW.Close();		//書き出しを終了
	}

	void ReadLoginData(){
		readtextData.Clear ();	//取得した情報を上書き保存する為、前回のデータを消す

		StreamReader SR = new StreamReader (logindatapath);
		string line;
		while ((line = SR.ReadLine ()) != null) {	//１行ずつ読み込み
			readtextData.Add (line);				//リストに追加していく
		}
		SR.Close ();

		Debug.Log (readtextData[0]);
	}
}