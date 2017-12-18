using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N8_SyncMove : Photon.MonoBehaviour {

	// 通信量をまとめるクラスを宣言
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
/*			short[] hoge = new short[3] {(short)transform.position.x,
				(short)transform.position.y, (short)transform.position.z
			};
			photonView.RPC ("SyncPosition", PhotonTargets.Others, hoge[0]);
*/
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
//		transform.position = new Vector3((float)pos[0],(float)pos[1],(float)pos[2]);
		transform.position = pos;

		//送信したバイト数を保存する
		SO.AddSize((int)pos[0]);
		SO.AddSize((int)pos[1]);
		SO.AddSize((int)pos[2]);
		SO.AddSize(3);

		//送信された返答を送る
		phview.RPC ("Receive", PhotonTargets.MasterClient,(byte)1,(byte)8);
	}


}