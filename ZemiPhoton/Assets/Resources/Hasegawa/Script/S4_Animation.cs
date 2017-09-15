using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S4_Animation : MonoBehaviour {

	[SerializeField] S1_Move player;
	[SerializeField] Animator animator;
	float _speed;
	float Speed{get{ return _speed;}set{ _speed = value;}}
	bool _isJump;
	bool IsJump{get{ return _isJump;}set{ _isJump = value;}}

	void Update () {
		SetAnimation ();
	}

	void SetAnimation(){
		animator.SetFloat ("Speed", _speed);
		animator.SetBool ("IsJumping", _isJump);
	}
}
