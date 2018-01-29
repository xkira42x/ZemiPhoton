using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Photon.MonoBehaviour {

	[SerializeField]Text[] Names;				// ユーザ名
	[SerializeField]Text[] Status;				// 準備ステート
	[SerializeField]Text timerText;				// スタートカウントダウン
	[SerializeField]GameObject[] MenuItems;		// メニュー画面の一覧

	byte index = 0;				// ユーザ名番号
	bool doOnce_Ready = false;	// 準備判定

	// 初期位置
	[SerializeField]Vector3[] Pos = new Vector3[] {
		new Vector3 (0, 1, 0),
		new Vector3 (0, 1, 2),
		new Vector3 (0, 1, 4),
		new Vector3 (0, 1, 6),
		new Vector3 (0, 1, 8)
	};

	/// ユーザ名の設定と同期
	public void SetName(string name){
		photonView.RPC ("SetNameText", PhotonTargets.AllBufferedViaServer, name);
	}

	/// ユーザ名同期（受信）
	/// 一覧に順次名前を更新する
	[PunRPC]
	void SetNameText(string name){
		Names [index].text = name;
		if (PlayerInfo.playerName == name)
			PlayerInfo.playerNumber = index;
		index++;
	}

	/// Readyボタンが押された際に、準備できたことを他のクライアントに送る
	public void OnClickReadyButton(){
		// 一度だけ、準備できたことを送信する
		if (!doOnce_Ready) {
			photonView.RPC ("Ready", PhotonTargets.AllBufferedViaServer, PlayerInfo.playerNumber);
			doOnce_Ready = true;
		}
	}

	/// 受信した番号から、誰がReadyしたかをＵＩに反映する
	[PunRPC]
	void Ready(int no){
		Status[no].text = "Ready";

		// 全員の準備が完了したか判定
		bool flg = true;
		for (int i = 0; i < PhotonNetwork.playerList.Length - 1; i++) {
			if (Status [i].text != "Ready") {
				flg = false;
				break;
			}
		}

		// 全員の準備ができ、クライアントであればゲーム開始のカウントダウンを開始する
		if (flg && PlayerInfo.isClient ()) {
			StartCoroutine ("StartTimeCount");
		} else if(flg){
			PlayerInfo.Spawn = true;
		}
	}

	/// ゲーム開始のタイムカウント
	IEnumerator StartTimeCount(){

		int time = 4;		// カウント時間(秒)

		// タイムテキストの初期化
		timerText.text = time.ToString ();

		while(true){
			yield return new WaitForSeconds (1);
			// タイムの更新・表示
			time--;
			timerText.text = time.ToString ();
			// タイムが0になったらループを抜ける
			if (time < 0)
				break;
		}
		// プレイヤーを生成する
		PlayerSpawn ();
		PlayerInfo.onTimer = true;
	}

	/// プレイヤー生成
	void PlayerSpawn(){
		
		int nn = PlayerInfo.playerNumber;	// 番号のキャッシュ（ID）

		// プレイヤーの生成
		GameObject player = PhotonNetwork.Instantiate ("FPSPlayer", Pos[nn], Quaternion.Euler(new Vector3(0,-90,0)), 0);
		//既に自分がいれば生成をしない
		if (GameObject.Find (PlayerInfo.playerName)) {
			//　元の情報を取得
			Transform MyInstanceTrans = GameObject.Find (PlayerInfo.playerName).transform;
			//　座標を元いた場所に移動
			player.transform.position = new Vector3(MyInstanceTrans.position.x,Pos[nn].y,MyInstanceTrans.position.z);
			PhotonNetwork.Destroy (GameObject.Find (PlayerInfo.playerName));
		}
		
		// オブジェクト名を設定
		//player.name = "Player" + (nn + 1).ToString();
		// ユーザ名を同期する
		player.GetPhotonView ().RPC ("SetName", PhotonTargets.AllBuffered, PlayerInfo.playerName);
		// 表示しているメニューを非表示にする
		foreach (GameObject obj in MenuItems)
			obj.SetActive (false);
		// プレイヤーを生成した設定にする
		if (photonView.isMine)
			PlayerInfo.Spawn = true;

		// 最後の処理が終了したらソースを削除する
		// 再入室で使う為,非アクティブ化に変更
//		Destroy (this);
//		this.gameObject.SetActive(false);
	}

}
