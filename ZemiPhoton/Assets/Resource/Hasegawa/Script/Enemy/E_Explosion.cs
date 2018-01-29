using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Explosion : MonoBehaviour {

    /// ダメージ
	[SerializeField]float dmg = 10;

    /// エフェクトの当たり判定（コールバック）
	void OnParticleCollision(GameObject obj){
		if (obj.layer == 10) {
			obj.GetComponent<S2_Status> ().Damage (dmg);
		}
	}
}
