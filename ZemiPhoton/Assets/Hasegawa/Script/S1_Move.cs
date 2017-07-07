using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1_Move : Photon.MonoBehaviour {
	// 同期スクリプト参照
	N3_SyncMove N_syncMove;
	Vector3 N_SyncPos = Vector3.zero;

	float S_Speed = 0.1f;
	byte S_Type = 0;

	[SerializeField]
	Animator S_Animator;
	float S_Motion = 0;

	void Start(){
		
		// 同期処理の呼び出し
		if (!photonView.isMine) {
			N_syncMove = GetComponent<N3_SyncMove> ();
			StartCoroutine ("SyncPosition");
		}
	}

	// Update is called once per frame
	void Update () {
		// キー移動
		if (photonView.isMine) {
			S_KeyMove ();
		
			// ジャンプ
			if (Input.GetKeyDown (KeyCode.Space)) {
				S_Jump ();
			}
		} else
			N_SyncPos = N_syncMove.GetSyncPos ();

		// アニメーション
		S_UnityChanAnimation ();

	}



	// ジャンプ
	void S_Jump(){
		StartCoroutine (S_Jumping ());
	}

	// ジャンプの中身
	IEnumerator S_Jumping(){
		Vector3 S_Jump = new Vector3 (0, 0.1f, 0);
		while (true) {
			transform.position += S_Jump;
			S_Jump.y -= 0.001f;
			if (S_Jump.y <= 0)
				break;
			yield return new WaitForSeconds (0.01f);
		}
	}

	// Unityちゃんモーション
	void S_UnityChanAnimation(){
		S_Animator.SetFloat ("Speed", S_Motion);
	}

	// キー移動判定
	void S_KeyMove(){
		S_Type = Key.NONE;
		// 走る
		if (Input.GetKey (KeyCode.LeftShift))
			S_Speed = 0.15f;
		else
			S_Speed = 0.1f;
		// キー判定
		if (Input.GetKey (KeyCode.W)) {
			S_Type += Key.FORWARD;
		}
		if (Input.GetKey (KeyCode.S)) {
			S_Type += Key.BACK;
		}
		if (Input.GetKey (KeyCode.A)) {
			S_Type += Key.LEFT;
		}
		if (Input.GetKey (KeyCode.D)) {
			S_Type += Key.RIGHT;
		}
		S_Move ();
	}

	// 移動
	void S_Move(){
		Vector3 pos = Vector3.zero;
		// 移動
		switch (S_Type) {
		case Key.FORWARD:
			pos += transform.forward * S_Speed;
			break;
		case Key.BACK:
			pos += -(transform.forward * S_Speed);
			break;
		case Key.RIGHT:
			pos += transform.right * S_Speed;
			break;
		case Key.LEFT:
			pos += -(transform.right * S_Speed);
			break;
		case Key.FORWARDLEFT:
			pos += transform.forward * S_Speed;
			pos += -(transform.right * S_Speed);
			break;
		case Key.FORWARDRIGHT:
			pos += transform.forward * S_Speed;
			pos += transform.right * S_Speed;
			break;
		case Key.BACKLEFT:
			pos += -(transform.forward * S_Speed);
			pos += -(transform.right * S_Speed);
			break;
		case Key.BACKRIGHT:
			pos += -(transform.forward * S_Speed);
			pos += transform.right * S_Speed;
			break;
		case Key.NONE:
			break;
		default:
			Debug.Log ("Error :: Player move S_Type");
			break;
		}
		transform.localPosition += pos;
		// モーション更新
		if (S_Type != Key.NONE)
			S_Motion = 1;
		else
			S_Motion = 0;
	}

	// 座標同期
	IEnumerator SyncPosition(){
		while (true) {
//			Debug.Log ("Sync position  " + N_SyncPos);
			// 移動処理とアニメーション処理
			if (N_SyncPos != Vector3.zero) {//N_SyncPos.x != 0 || N_SyncPos.y != 0 || N_SyncPos.z != 0) {
				S_Motion = 1;
				transform.position += new Vector3 (N_SyncPos.x * S_Speed, 0, N_SyncPos.z * S_Speed);
			} else
				S_Motion = 0;
			//
			yield return new WaitForSeconds(0.01655f);
		}
	}
}