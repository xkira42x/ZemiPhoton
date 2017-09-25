using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

	/// 体力ゲージ
	Slider slider;
	/// 体力値
	int Helth = 0;

	/// 体力パラメータの初期化を行う
	public void HPInit(){Helth=0;}
	/// 体力パラメータの初期化を指定した値で行う
	public void HPInit(int value){
		Helth = value;
	}
	/// ダメージを受けた際にパラメータ・UIに反映させる
	public void HPDec(int dmg){
		Helth -= dmg;				// 体力の更新
		slider.value = Helth;		// 体力ゲージの更新
	}
	/// 体力の取得
	public int HPGet(){return Helth;}	// 体力を返す
	/// 体力UIを表示する準備を行う
	public void HPShow(){
		GameObject obj = GameObject.Find ("PlayerName" + "PlayerNumber"); // シーン上の体力表示を検索する
		obj.SetActive (true);					// 体力表示を有効にする
		slider = obj.GetComponent<Slider> ();	// 体力バーを取得
	}
}