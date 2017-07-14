using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N6_SyncShot : Photon.MonoBehaviour {

	//PhotonView N_PhotonView;
	S3_Shot S_Shot;

	// Use this for initialization
	void Start () {
		S_Shot = GetComponent<S3_Shot> ();
		//N_PhotonView = PhotonView.Get (this);
	}
	// 同期処理
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			// 送信
			stream.SendNext(S_Shot.S_Shooting);
		}else{
			// 受信
			S_Shot.S_Shooting = (bool)stream.ReceiveNext();
		}
	}
}
