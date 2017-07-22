﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N6_SyncShot : Photon.MonoBehaviour {

	//PhotonView N_PhotonView;
	S3_Shot S_Shot;

	void Awake(){
		S_Shot = GetComponent<S3_Shot> ();
	}

	// Use this for initialization
	void Start () {
		//N_PhotonView = PhotonView.Get (this);
	}
	// 同期処理
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			// 送信
			stream.SendNext(S_Shot.S_Shoot);
		}else{
			// 受信
			S_Shot.S_Shoot = (bool)stream.ReceiveNext();
		}
	}
}
