using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Explosion : MonoBehaviour {

	[SerializeField]float dmg = 10;

	void OnParticleCollision(GameObject obj){
		if (obj.layer == 10) {
			obj.GetComponent<S2_Status> ().Damage (dmg);
		}
        Debug.Log("Explosion Collision " + obj.gameObject.name);
	}
}
