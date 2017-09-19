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
	float S_Speed;
	public float Speed{ get { return S_Speed; } set { S_Speed = value; } }
	// 移動方向
	byte S_Type = 0;
	[SerializeField] Transform myTransform;
	//[SerializeField] Animator S_Animator;
	//[SerializeField]S4_Animation S_Animation;
	float S_Motion = 0;

	// ジャンプ判定群
	const byte NONE=0,UP=1,DOWN=2;
	byte S_Jtype = NONE;
	// 重力
	float JumpGravity;
	// ジャンプしている
	bool isJumping = false;
	public bool IsJumping{ get { return isJumping; } set { isJumping = value; } }
	[SerializeField] bool isGround;
	void IsGround(){isGround = Physics.Raycast (transform.position, Vector3.down, 0.3f);}

	//IK追記
	[SerializeField] bool mineflg;

	void Start(){
		N_SyncPos = transform.position;
		if (!photonView.isMine)
			N_syncMove = GetComponent<N3_SyncMove> ();
	}

	void Update(){
		if (photonView.isMine||mineflg==true) {
			MyMain ();
		} else {
			// 同期処理の呼び出し
			SyncPosition();
		}
	}


	// Unityちゃんモーション
	void S_UnityChanAnimation(){
		//S_Animator.SetFloat ("Speed", S_Motion);
		//S_Animator.SetBool ("IsJumping",isJumping);
	}

	// キー移動判定
	void S_KeyMove(){
		S_Type = Key.NONE;
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
		int angle = 0;
		// 移動
		switch (S_Type) {
		case Key.FORWARD		:angle=0;	break;
		case Key.BACK			:angle=180;	break;
		case Key.RIGHT			:angle=90;	break;
		case Key.LEFT			:angle=270;	break;
		case Key.FORWARDLEFT	:angle=315;	break;
		case Key.FORWARDRIGHT	:angle=45;	break;
		case Key.BACKLEFT		:angle=215;	break;
		case Key.BACKRIGHT		:angle=135;	break;
		case Key.NONE:break;
		default:Debug.Log ("Error :: Player move S_Type");break;
		}
		if (S_Type != NONE) {
			transform.position += new Vector3 (
				Mathf.Sin ((myTransform.localEulerAngles.y + transform.localEulerAngles.x + angle) * 3.14f / 180) * 0.1f, 
				0, 
				Mathf.Cos ((myTransform.localEulerAngles.y  + angle) * 3.14f / 180) * 0.1f);
		}
		// モーション更新
		S_Speed = (S_Type != Key.NONE)? 1 : 0;
	}

	// ジャンプ
	void S_Jump(){
		// ジャンプスイッチ
		if (isGround && Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine (DelayForJumping ());
			isJumping = true;
		}
		// 判定分岐
		switch (S_Jtype) {
		case UP:
			S_ToJump ();
			break;
		case DOWN:
			S_DropDown ();
			break;
		}
	}
	// ジャンプアニメーション用の遅延
	IEnumerator DelayForJumping(){
		yield return new WaitForSeconds (0.3f);
		S_Jtype = UP;
		JumpGravity = 0.3f;
	}
	// 上昇処理
	void S_ToJump(){
		// 重力
		AddGravity ();
		// 頂点判定
		if (JumpGravity <= 0.1f)
			S_Jtype = DOWN;
	}
	// 下降処理
	void S_DropDown(){
		// 床判定
		if (!isGround)
			// 重力
			AddGravity ();
		else {
			isJumping = false;
			S_Jtype = NONE;
		}
	}
	// 重力を加える
	void AddGravity(){
		transform.position += Vector3.up * JumpGravity;
		JumpGravity -= 0.98f * Time.deltaTime;		
	}

	// メイン処理
	void MyMain(){

		// キー移動
		S_KeyMove ();

		// ジャンプ
		//S_Jump();

		IsGround ();

		// アニメーション
		S_UnityChanAnimation ();
	}

	// 座標同期
	void SyncPosition(){
		// 同期座標取得
		N_SyncPos = N_syncMove.GetSyncPos ();
		Vector3 movement = (N_SyncPos - transform.position) * 0.5f;
		movement = new Vector3 (movement.x, 0, movement.z);
		// 移動処理とアニメーション処理
		if (movement != Vector3.zero) {
			S_Motion = 1;
			transform.position += movement;
		} else
			S_Motion = 0;
		isJumping = N_syncMove.IsJump;
		// アニメーション
		S_UnityChanAnimation ();
	}
}