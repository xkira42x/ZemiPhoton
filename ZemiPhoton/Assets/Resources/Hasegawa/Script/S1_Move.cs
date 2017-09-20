using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.InteropServices;

public class S1_Move : MonoBehaviour {

	// 移動速度
	[SerializeField]float speed = 0.1f;

	float _motion = 0;
	public float motion{get{ return _motion;}}

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


	void Start(){
	}

	void Update(){
		// キー移動
		S_KeyMove ();

		// ジャンプ
		//S_Jump();

		IsGround ();

	}
		
	// キー移動判定
	void S_KeyMove(){
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal") * speed;
		float vertical = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
		transform.Translate (horizontal, 0, vertical);
		_motion = (horizontal != 0 || vertical != 0) ? 1 : 0;
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
			ToJump ();
			break;
		case DOWN:
			DropDown ();
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
	void ToJump(){
		// 重力
		AddGravity ();
		// 頂点判定
		if (JumpGravity <= 0.1f)
			S_Jtype = DOWN;
	}
	// 下降処理
	void DropDown(){
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
}