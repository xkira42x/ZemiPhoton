using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N4_SyncAnimation : Photon.MonoBehaviour {

	// 同期するステータスを取得する
	S1_Move S_Move;
	// 同期後にアニメーションを更新するために
	// アニメーターを取得
	Animator animator;

	/// 同期したアニメーションの値を格納
	/// （実際に受け取る値が、移動量のためfloat speedと表記をする）
	float speed = 0;
	/// 同期したジャンプフラグを格納する
	bool isJumping = false;

	//IK追記
	N15_SizeOf SO;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();

		S_Move = GetComponent<S1_Move> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (photonView.isMine) {
			// アニメーションの値を送信
			photonView.RPC ("SyncMotion", PhotonTargets.Others, S_Move.motion);
			// ジャンプアニメーションの値を送信
			photonView.RPC ("SyncJumping", PhotonTargets.Others, S_Move.IsJumping);
		} else {
			// アニメーションの再生をする
			SetAnimation ();
		}
	}

	/// アニメーション値の取得側の記述
	[PunRPC]
	void SyncMotion(float motion){
		speed = motion;
		//IK追記
		SO.AddSize ((int)motion);
		SO.AddSize (3);

	}

	/// ジャンプアニメーション値の取得側の記述
	[PunRPC]
	void SyncJumping(bool jump){
		isJumping = jump;
	}

	/// アニメーションの再生を行う
	void SetAnimation(){
		animator.SetFloat ("Speed", speed);
		animator.SetBool ("IsJumping", isJumping);
	}
}
