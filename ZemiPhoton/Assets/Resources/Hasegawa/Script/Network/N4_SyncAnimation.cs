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
    // 最後に同期したアニメーションステータス
	byte lastStatus = 0;

	//IK追記
	N15_SizeOf SO;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();

		S_Move = GetComponent<S1_Move> ();
		animator = GetComponent<Animator> ();
	}

	void Update () {
        // アニメーションステータスの取得
		byte status = S_Move.Status;
        // クライアントが制御するキャラかつ、ステータスが変更している時
			if (photonView.isMine && (lastStatus != status)) {
            // アニメーションステータスの同期
			photonView.RPC ("SyncAnimation", PhotonTargets.Others, status);
            // 同期したアニメーションステータスの更新
			lastStatus = status;
		}
	}
	
    /// <summary>
    /// アニメーションステータスの同期（受信）
    /// </summary>
    /// <param name="status"></param>
	[PunRPC]
	void SyncAnimation(byte status){
        // 受け取ったステータスでアニメーションを再生する
			animator.Play (AnimationName [status]);
			SO.AddSize ((int)status);
	}
}
