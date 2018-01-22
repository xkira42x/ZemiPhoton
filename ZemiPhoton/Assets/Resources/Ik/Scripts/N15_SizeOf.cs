﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Verification;
using UnityEngine.UI;

public class N15_SizeOf : MonoBehaviour {
	[DllImport("sample-dll")]private static extern int CountUp ();
	//通信した個数
	int masscount=0;

	//各スクリプトで通信した量を格納する
	int sizecnt=0;

	//通信量
	int ave=0;

	public int Syncmass=0;
	//通信量を出力するテキストを格納
	[SerializeField]
	GameObject SizeText;
	Text TM;

	//通信量を算出する秒数
	[SerializeField]
	float second;


	void Start () {

		//クライアントがルームを離れる時、プレイヤーが生成したオブジェクトを破棄しないよう変更
		PhotonNetwork.autoCleanUpPlayerObjects=false;

		//クライアントがルームを途中で切断していた場合、同ルームへの再入室を試みる
		//※正常な動作が確認できていない
//		PhotonNetwork.ReconnectAndRejoin();

		//出力先を取得
		TM=SizeText.GetComponent<Text> ();
		//second秒ごとの平均通信量を出力する
		StartCoroutine ("Ave");
	}
		
	//通信量を足していく
	public void AddSize(int ss){
		int divcount=1;
		while (ss > 1) {
			//２で割る
			ss /= 2;
			//割った回数を足していく
			divcount++;
		}

		//実足し
		ave+=divcount;
		//通信した個数を増やす
		masscount++;
	}

	//現在使用していない
	//引数のbit数を出力する(int)
	public void SizeLog(int ss){
//		if (PhotonNetwork.isMasterClient) {
			Debug.Log ("size count:"+ss);
			//bit算出に使用
			int divcount=1;
			//値が１以下になるまで２で割る
			while (ss > 1) {
				//２で割る
				ss /= 2;
				//割った回数を足していく
				divcount++;
			}
			//計算したbit数を保存していく
			ave += divcount;
			Debug.Log ("ave:"+ave);

//		}
	}

	//second秒ごとの平均通信量を出力する
	IEnumerator Ave(){
		while (true) {
			//通信数(int)から通信量(byte)に変換
//			SizeLog (sizecnt);

			int average = ave / (int)second;
			int count = masscount / (int)second;
							
			//出力
			TM.text = average + " bps" + "\n" +
				count + " 個ps";

			//10秒ごとの計測したbit数を出力
//			Debug.Log ("b/s:"+ave+" / 10");
			//10秒ごとの計測した回数を出力
//			Debug.Log ("c/s:"+masscount+" / 10");
	
			//次の１０秒間のデータを図るため初期化
			ave = 0;
			masscount = 0;
			sizecnt = 0;
			Syncmass = average;	//データ量更新
			yield return new WaitForSeconds (second);
		}
	}


}
