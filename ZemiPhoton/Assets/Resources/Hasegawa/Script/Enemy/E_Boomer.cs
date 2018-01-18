using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Boomer : E_AI {

	/// 死ぬ際の爆発に使うオブジェクトを登録
	[SerializeField] GameObject Explosion;

	void Start () {
		base.Start ();
	}
	
	void Update () {
		base.Update ();
	}

	public override void AttackedTheTarget ()
	{
		base.AttackedTheTarget ();
	}

	public override void OnDied ()
	{
		Instantiate (Explosion, transform.position, Quaternion.identity);
	}
}
