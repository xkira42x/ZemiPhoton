using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N4_SyncAnimation : Photon.MonoBehaviour {

	// 同期するステータスを取得する
	S1_Move S_Move;
	// 同期後にアニメーションを更新するために
	// アニメーターを取得
	Animator animator;
	// アニメーションの名前を格納
	readonly string[] AnimationName = { "Idol", "Walk", "Jump", "Crouch", "CrouchMove" };

	//IK追記
	N15_SizeOf SO;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();

		S_Move = GetComponent<S1_Move> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (photonView.isMine) {
			photonView.RPC ("SyncAnimation", PhotonTargets.Others, S_Move.Status);
		}
	}
		
	[PunRPC]
	void SyncAnimation(byte status){
		animator.Play (AnimationName [status]);
	}
}
