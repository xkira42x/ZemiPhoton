using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N6_SyncShot : Photon.MonoBehaviour {

	// 送受信する情報の伝達先
	S3_Shot S_Shot;

	void Awake(){
        // コンポーネントの取得
		S_Shot = GetComponent<S3_Shot> ();
	}

	// 同期処理
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
        // クライアントが操作するキャラクターの時、射撃判定を送信する
        // そうでない時、射撃判定を受信する
		if (stream.isWriting) {
			// 送信
			//stream.SendNext(S_Shot.S_Shoot);
		}else{
			// 受信
			//S_Shot.S_Shoot = (bool)stream.ReceiveNext();
		}
	}
}