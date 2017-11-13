using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAndResponse : Photon.MonoBehaviour {

	bool check = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 接続が確立されているかを判定する
	/// </summary>
	void Check(){
		if (PlayerInfo.isClient ()) {
			check = false;
			photonView.RPC ("Call", PhotonTargets.Others);
			StartCoroutine ("Latency");
		}
	}

	/// <summary>
	/// 待ち時間、接続が確立されているかを判定する
	/// </summary>
	IEnumerator Latency(){
		yield return new WaitForSeconds (3);
		if (!check) {
			Debug.LogError ("Synchronization is not established.");
		}
	}

	/// <summary>
	/// 接続判定の初期呼び出し、コールを受け取りレスポンスを返す
	/// </summary>
	[PunRPC]
	void Call(){
		photonView.RPC ("Response", PhotonTargets.MasterClient);
	}

	/// <summary>
	/// 接続判定の第二呼び出し、
	/// </summary>
	[PunRPC]
	void Response(){
		check = true;
	}
}


/*
 * string PhotonNetwork.NetworkStaticsToString();
 * ネットワーク動態統計の文字列
 *
 *
 * void OnCreatedRoom();
 * クライアントが部屋を作成し、入室した際に呼び出される
 * [OnMasterClientSwitched()]
 * ↑マスタークライアントが設定された際の受け取りメソッド
 *  プロパティや開始信号が必要な場合に使う
 * 
 * 
 * PhotonPlayer.isMasterClient;
 * このプレイヤーが、ルームのマスタークライアントの場合Trueを返す
*/