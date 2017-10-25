using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimatorController : MonoBehaviour {

	Animator animator;

	readonly string[] AnimationName = { "Idol", "Walk", "Jump", "Crouch", "CrouchMove" };

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void Update () {
	}

	public void AnimationPlay(byte index){
		animator.Play (AnimationName [index]);
	}
}
