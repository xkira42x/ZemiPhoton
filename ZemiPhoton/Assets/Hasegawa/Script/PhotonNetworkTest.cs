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
		if (useCommunicationIntervalTest)
			CommunicationIntervalTest ();

		// 通信するデータ量の最大値のテスト
		if (useStressTest)
			StressTest ();
		
	}
	//******************************************************************//
	//******************************************************************//


	//******************************************************************//
	//通信同期リクエストを送ってから届くまでの時間を計る					//
	//受信側でのみ結果が出る（受信した時間から計算をするため）			//
	//******************************************************************//
	// 関数アクティブスイッチ
	[SerializeField]
	bool useCommunicationIntervalTest = false;
	// 始めの時間/終わりの時間/待ち時間
	float startTime,endTime,toWait;
	public float GetWaitTime(){return toWait;}
	void CommunicationIntervalTest(){
		// 同期処理
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


	//******************************************************************//
	//可変長配列を用いて、一度の通信量の負荷を与えるテスト				//
	//																	//
	//******************************************************************//
	// 関数アクティブスイッチ
	[SerializeField]
	bool useStressTest = false;
	// 負荷値
	[SerializeField]
	int LoadValue = 10;
	string addString;
	void StressTest(){
		// 送信データの準備
		if (photonView.isMine) {

		}
		// 同期処理
		for (int i = 0; i < LoadValue; i++) {
			if (stream.isWriting) {
				// 送信
				stream.SendNext ("Data" + i.ToString () + "\n");
			} else {
				// 受信
				Result.text += ((string)stream.ReceiveNext ()).ToString();
			}
		}
	}
	//******************************************************************//
	//******************************************************************//


	string hello = "";
	void Hello(){
		// 同期処理
		if (stream.isWriting) {
			// 送信
			stream.SendNext ("Hello\n");
		} else {
			// 受信
			hello = (string)stream.ReceiveNext ();
		}
		if (!photonView.isMine)
			Result.text = hello.ToString();
	}
		
}
