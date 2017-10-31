using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimatio : MonoBehaviour {
	// 同期するステータスを取得する
	S1_Move S_Move;
	// 同期後にアニメーションを更新するために
	// アニメーターを取得
	Animator animator;
	// アニメーションの名前を格納
	readonly string[] AnimationName = { "Idol", "Walk", "Jump", "Crouch", "CrouchMove" };
	// Use this for initialization
	void Start () {
		S_Move = GetComponent<S1_Move> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.Play (AnimationName [S_Move.Status]);
	}
}
