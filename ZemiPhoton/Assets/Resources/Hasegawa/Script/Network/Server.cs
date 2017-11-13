using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : Photon.MonoBehaviour {

	//-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
	// 型枠だけを作った感じです。追加/改変OK
	// サーバー接続→ログイン・新規作成の流れをここに記述してください
	//-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/
	[System.NonSerialized]public int ipAddress;

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
		photonView.RPC("SyncIPAddress",PhotonTargets.OthersBuffered,ipAddress);
	}

	[PunRPC]
	void SyncIPAddress(int adrs){
		// ここで送られてきた値を受け取る
		ipAddress = adrs;
		ConnectToServer ();
	}

	void ConnectToServer(){
		// ここでサーバー接続とログイン・新規作成をする
		// PlayerInfo.playerName; // プレイヤーの名前を格納している

	}
}
