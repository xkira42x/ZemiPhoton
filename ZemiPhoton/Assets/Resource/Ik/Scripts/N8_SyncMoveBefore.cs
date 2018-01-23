using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N8_SyncMoveBefore : Photon.MonoBehaviour {


	N15_SizeOf SO;		// 通信量をまとめるクラスを宣言
	PhotonView phview;	// 通信の送信を確認する為の関数を呼ぶためのPhotonViewを宣言
	short pointup=100;	//同期する小数点以下の数

	//通信量を計測するコンポーネントの取得
	void Awake(){SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();
		phview=GameObject.Find("PhotonManager").GetPhotonView();
	}

	//部屋のマスターが
	//座標同期を開始する
	void Start(){
		if (PhotonNetwork.isMasterClient)
		StartCoroutine ("SyncPos");
	}
	/// <summary>
	/// １秒間に４回座標を同期する
	/// </summary>
	IEnumerator SyncPos(){
		while (true) {
			Vector3 enemypos=this.transform.position;
			photonView.RPC ("SyncPosition", PhotonTargets.Others, enemypos);
//			Debug.Log ("N8.SyncPosition:送信"+enemypos [0]);
			yield return new WaitForSeconds (0f);
		}
	}

	/// <summary>
	/// 座標を同期する
	/// </summary>
	/// <param name="pos">同期する座標.</param>
	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;
//		Debug.Log ("N8.SyncPosition:受信:" + (float)pos [0] / pointup);

		//送信したバイト数を保存する
		SO.AddSize((int)pos[0]/pointup);
		SO.AddSize((int)pos[1]/pointup);
		SO.AddSize((int)pos[2]/pointup);
		SO.AddSize(3);

		//送信された返答を送る
		phview.RPC ("Receive", PhotonTargets.MasterClient,(byte)1,(byte)8);
	}


}