using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PhotonNetworkTest : Photon.MonoBehaviour {
	// 結果表示
	Text Result;

	// 同期する際のポオストの様な役割
	PhotonView N_photonView;
	PhotonStream stream;
	PhotonMessageInfo info;

	// Use this for initialization
	void Start () {
		Result = GameObject.Find ("Result").GetComponent<Text> ();
		// ネットワーク設定
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
		stream = ss;
		info = ii;

		Hello ();

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
	float startTime,endTime,toWait;
	public float GetWaitTime(){return toWait;}
	void TestCommunicationInterval(){
			if (stream.isWriting) {
				// 通信開始の時間を同期
				startTime = System.DateTime.Now.Millisecond;
				stream.SendNext (startTime);
			} else {
				// 送信物を受け取った時間
				endTime = System.DateTime.Now.Millisecond;
				startTime = (float)stream.ReceiveNext ();
			}
		if (!photonView.isMine) {
			// 秒値で結果を出す
			toWait = ((Mathf.Abs (endTime - startTime)) / 1000);
			Result.text += "Communication interval : " + toWait.ToString () + "s\n" +
			"Start : " + (startTime / 1000).ToString () + "s\n" +
			"End : " + (endTime / 1000).ToString () + "s\n";
		}
	}
	//******************************************************************//
	//******************************************************************//
	string hello = "";
	void Hello(){
		if (stream.isWriting) {
			// 通信開始の時間を同期
			stream.SendNext ("Hello\n");
		} else {
			// 送信物を受け取った時間
			hello = (string)stream.ReceiveNext ();
		}
		if (!photonView.isMine)
			Result.text = hello;
	}
		
}
