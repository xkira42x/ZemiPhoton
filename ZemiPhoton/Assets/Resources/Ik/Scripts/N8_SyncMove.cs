using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N8_SyncMove : Photon.MonoBehaviour {

	//IK追記
	N15_SizeOf SO;

	PhotonView phview;
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
			Debug.Log ("N8.SyncPosition:送信");
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
			yield return new WaitForSeconds (0.25f);
		}
	}

	/// <summary>
	/// 座標を同期する
	/// </summary>
	/// <param name="pos">同期する座標.</param>
	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;

		//IK追記
		//送信したバイト数を保存する
		SO.AddSize((int)pos.x);
		SO.AddSize((int)pos.y);
		SO.AddSize((int)pos.z);
		SO.AddSize(3);

		phview.RPC ("Receive", PhotonTargets.MasterClient,(byte)1,(byte)8);
	}


}