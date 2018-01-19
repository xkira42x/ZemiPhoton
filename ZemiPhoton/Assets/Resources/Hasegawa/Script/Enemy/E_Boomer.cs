using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Boomer : E_AI {

	/// 死ぬ際の爆発に使うオブジェクトを登録
	[SerializeField] GameObject Explosion;

	/// 初期化
	void Start () {
		base.Start ();
	}

	/// メインループ
	void Update () {
		base.Update ();
	}

	/// ダメージを与える処理
	public override void AttackedTheTarget (){
		base.AttackedTheTarget ();
	}

	/// 自分が死んだ際のコールバック
	public override void OnDied (){
		Instantiate (Explosion, transform.position, Quaternion.identity);
	}
}
