﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Verification;
public class Target: MonoBehaviour {
	A_normal_enemy_move_typeR AER;
	[SerializeField]
	GameObject Object;
	A_normal_enemy_move AE;
	TextMesh TM;
	// Use this for initialization
	void Start () {
//		AER = gameObject.transform.parent.GetComponent<A_normal_enemy_move_typeR> ();
		AE = gameObject.transform.parent.GetComponent<A_normal_enemy_move> ();
		TM = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		ShowTarget ();	
	}
	void ShowTarget(){
//		TM.text="Target:"+AER.TargetGet();
		TM.text="Target:"+AE.TargetGet();
	}
}
