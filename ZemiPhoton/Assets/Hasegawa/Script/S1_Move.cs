using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class S1_Move : Photon.MonoBehaviour {
	// 同期スクリプト参照
	N3_SyncMove N_syncMove;
	// 同期座標
	Vector3 N_SyncPos;
	// 移動速度
	float S_Speed = 0.1f;
	// 移動方向
	byte S_Type = 0;

	[SerializeField]
	Animator S_Animator;
	float S_Motion = 0;

	const byte NONE=0,UP=1,DOWN=2;
	byte S_Jtype = NONE;
	int count = 0;

	bool isGround;
	void IsGround(){isGround = Physics.Raycast (transform.position, Vector3.down, 1.6f);}

	void Start(){
		N_SyncPos = transform.position;
		if (!photonView.isMine)
			N_syncMove = GetComponent<N3_SyncMove> ();

	}

	void Update(){
		if (photonView.isMine) {
			MyMain ();
		} else {
			// 同期処理の呼び出し
			SyncPosition();
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

	void S_Jump(){
		if (isGround && Input.GetKeyDown (KeyCode.Space)) {
			S_Jtype = UP;
		}
		switch (S_Jtype) {
		case UP:
			S_ToJump ();
			break;
		case DOWN:
			S_DropDown ();
			break;
		}
	}

	IEnumerator S_ToJump(){
		while (true) {
			if (count < 10)
				count++;
			else {
				S_Jtype = DOWN;
				break;
			}
			transform.position += Vector3.up * 0.1f;
			yield return new WaitForSeconds (1f);
		}
	}
	void S_DropDown(){
		if (!isGround)
			transform.position += Vector3.down * 0.5f;
		else
			S_Jtype = NONE;
	}

	// メイン処理
	void MyMain(){

		// キー移動
		S_KeyMove ();

		// ジャンプ
		S_Jump();

		// アニメーション
		S_UnityChanAnimation ();
	}

	// 座標同期
	void SyncPosition(){
		// 同期座標取得
		N_SyncPos += N_syncMove.GetSyncPos ();
		Vector3 movement = (N_SyncPos - transform.position) * 0.5f;

		// 移動処理とアニメーション処理
		if (movement != Vector3.zero) {
			S_Motion = 1;
			transform.position += movement;
		} else
			S_Motion = 0;
		// アニメーション
		S_UnityChanAnimation ();
	}
}