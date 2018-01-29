using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Boomer : E_Animation {

	/// 死ぬ際の爆発に使うオブジェクトを登録
	[SerializeField] GameObject Explosion;

	/// 自分が死んだ際のコールバック
	public override void OnDied (){
		Instantiate (Explosion, new Vector3 (transform.position.x, 1, transform.position.z), Quaternion.identity);
	}
}
