﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PhotonTest : Photon.MonoBehaviour {
	Text Result;

	// 同期する際のポオストの様な役割
	PhotonView N_photonView;
	PhotonStream stream;
	PhotonMessageInfo info;

	// Use this for initialization
	void Start () {
		Result = GameObject.Find ("Result").GetComponent<Text> ();
		// ネットワーク設定a\
		PhotonNetwork.NetworkStatisticsEnabled = true;
		N_photonView = PhotonView.Get (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//******************************************************************//
	//通信同期の呼び出し関数												//
	//******************************************************************//
	void OnPhotonSerializeView(PhotonStream ss,PhotonMessageInfo ii){
		stream = ss;info = ii;

		// 通信同期リクエストを送ってから届くまでの時間を計る
		if (testCommunicationInterval)
			TestCommunicationInterval ();
		
	}
	//******************************************************************//
	//******************************************************************//


	//******************************************************************//
	//通信同期リクエストを送ってから届くまでの時間を計る					//
	//受信側でのみ結果が出る（受信した時間から計算をするため）			//
	//******************************************************************//
	[SerializeField]
	bool testCommunicationInterval = false;
	bool RunOnce = false;
	int startTime,endTime;
	void TestCommunicationInterval(){
		if (!RunOnce) {
			if (stream.isWriting) {
				// 通信開始の時間を同期
				startTime = System.DateTime.Now.Second;
				stream.SendNext (startTime);
				RunOnce = true;
			} else {
				// 送信物を受け取った時間
				endTime = System.DateTime.Now.Second;
				startTime = (int)stream.ReceiveNext ();
				RunOnce = true;
			}
			Result.text += "Communication interval : " + (endTime - startTime).ToString() + "\n";
			if (endTime == 0)
				Result.text += "Communication interval : It is not a correct result.";
		}
	}
	//******************************************************************//
	//******************************************************************//

}
