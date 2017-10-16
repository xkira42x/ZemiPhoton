using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Verification;
using UnityEngine.UI;

public class N15_SizeOf : MonoBehaviour {
	[DllImport("sample-dll")]private static extern int CountUp ();
	int aa = 0;
	int masscount=0;
	int divcount=1;
	int ave=0;
	[SerializeField]
	GameObject SizeText;
	Text TM;
// Use this for initialization
	void Start () {
/*		hello.HellWorld ();
		verifi.ShowResult(I);
		verifi.ShowResult(F);
		verifi.ShowResult(D);
		verifi.ShowResult (V3.x);
		verifi.ShowResult(V3);
*/
//		StartCoroutine ("mass");
		TM=SizeText.GetComponent<Text> ();
		StartCoroutine ("Ave");
	}
		
	//引数のbit数を出力する(int)
	public void SizeLog(int ss){
		if (PhotonNetwork.isMasterClient) {
			//値が１以下になるまで２で割る
			while (ss > 1) {
				//２で割る
				ss /= 2;
				//割った回数を足していく
				divcount++;
			}
			Debug.Log ("Size:" + divcount);
			//計算したbit数を保存していく
			ave += divcount;
			//通信した項目数をカウントする
			masscount++;

			//カウンターを初期化
			divcount = 1;
		}
	}
	IEnumerator mass(){
		while (true) {
			Debug.Log (aa);
			SizeLog (aa);
			aa++;
			yield return new WaitForSeconds (1f);
		}
	}
	IEnumerator Ave(){
		while (true) {
			TM.text = ave/5 + "b /s" +"\n"+
				masscount/5+"回 /s";


			//10秒ごとの計測したbit数を出力
//			Debug.Log ("b/s:"+ave+" / 10");
			//10秒ごとの計測した回数を出力
//			Debug.Log ("c/s:"+masscount+" / 10");
	
			//次の１０秒間のデータを図るため初期化
			ave = 1;
			masscount = 1;
			yield return new WaitForSeconds (5f);
		}
	}
}
