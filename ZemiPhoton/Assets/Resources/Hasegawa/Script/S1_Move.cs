using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.InteropServices;

public class S1_Move : MonoBehaviour {

	const byte IDOL = 0,WALK = 1,JUMP = 2,CROUCH = 3,CROUCHMOVE = 4;
	byte status = IDOL;
	public byte Status{ get { return status; } }

	[SerializeField]Transform myCollection;
	// 移動速度
	[SerializeField]float speed = 0.1f;

	float _motion = 0;
	public float motion{ get { return _motion; } }

	// 重力
	float JumpGravity;
	// ジャンプしている
	bool isJumping = false;
	public bool IsJumping{ get { return isJumping; } set { isJumping = value; } }
	bool isGround;

	void IsGround(){
		isGround = Physics.Raycast (transform.position, Vector3.down, 0.3f);
	}
	bool isCrouch = false;

	Rigidbody myRigidbody;

	void Start(){
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void Update(){

		// キー移動
		S_KeyMove ();

		// ジャンプ
		S_Jump();

		// しゃがみ
		Crouch ();

		IsGround ();
		status = (!isGround) ? JUMP : (_motion == 1) ? WALK : (isCrouch) ? CROUCH : IDOL;
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
		if (isGround && Input.GetKeyDown (KeyCode.Space))
			myRigidbody.velocity = Vector3.up * 5;
	}

	// しゃがむ
	void Crouch(){
		float width = 0f;
		if (Input.GetKey (KeyCode.LeftControl)) {
			width = -1f;
			isCrouch = true;
		} else
			isCrouch = false;
		myCollection.localPosition = new Vector3 (0, width, 0);
	}
}