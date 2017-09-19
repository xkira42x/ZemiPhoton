using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4_Animation : MonoBehaviour {

	[SerializeField] S1_Move S_Move;
	[SerializeField] Animator animator;

	void Update () {
		SetAnimation ();
	}

	void SetAnimation(){
		animator.SetFloat ("Speed", S_Move.Speed);
		animator.SetBool ("IsJumping", S_Move.IsJumping);
	}
}
