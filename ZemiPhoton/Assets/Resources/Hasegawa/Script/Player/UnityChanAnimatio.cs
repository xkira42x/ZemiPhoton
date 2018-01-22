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
	readonly string[] AnimationName = { "Idol", "Walk", "Jump", "Crouch", "CrouchMove","Die" };

	/// 初期化
	void Start () {
		// キャッシュ取得
		S_Move = GetComponent<S1_Move> ();
		animator = GetComponent<Animator> ();
	}

	/// アニメーション再生
	void Update () {
		animator.Play (AnimationName [S_Move.Status]);
	}
}
