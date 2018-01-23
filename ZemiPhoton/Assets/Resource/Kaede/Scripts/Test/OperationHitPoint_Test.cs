using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationHitPoint_Test : MonoBehaviour {

	private GameObject HitPointPlate;	//　HPを表示しているプレート
	public Slider HitPoint;

	private short HP = 100;

	void Start() {
		HitPointPlate = HitPoint.transform.parent.gameObject;
	}

	void LateUpdate() {
		HitPoint.transform.rotation = Camera.main.transform.rotation;
	}

	[PunRPC]
	void SetHP(short hp){
		HitPoint.value = hp;
	}

	[PunRPC]
	void Damage( short d){
		HP -= d;
		HitPoint.value = HP;
	}
}
