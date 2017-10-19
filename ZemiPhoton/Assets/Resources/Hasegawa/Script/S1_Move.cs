using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.InteropServices;

public class S1_Move : MonoBehaviour {

	public enum STATUS{Idol,Walk,Jump,Squat};
	STATUS status = STATUS.Idol;
	public string Status{ get { return status.ToString (); } }

	[SerializeField]Transform myCollection;
	// 移動速度
	[SerializeField]float speed = 0.1f;

	float _motion = 0;
	public float motion{ get { return _motion; } }

	// ジャンプ判定群
	const byte NONE=0,UP=1,DOWN=2;
	byte S_Jtype = NONE;
	// 重力
	float JumpGravity;
	// ジャンプしている
	bool isJumping = false;
	public bool IsJumping{ get { return isJumping; } set { isJumping = value; } }
	[SerializeField] bool isGround;
	void IsGround(){
		isGround = Physics.Raycast (transform.position, Vector3.down, 0.3f);
	}
	bool isSquat = false;

	[SerializeField]Rigidbody rigid;

	void Start(){
		rigid = GetComponent<Rigidbody> ();
	}

	void Update(){

		// キー移動
		S_KeyMove ();

		// ジャンプ
		S_Jump();

		// しゃがみ
		Squat ();

		IsGround ();
		status = (!isGround) ? STATUS.Jump : (_motion == 1) ? STATUS.Walk : (isSquat) ? STATUS.Squat : STATUS.Idol;
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
			rigid.velocity = Vector3.up * 5;
		}
	}

	// しゃがむ
	void Squat(){
		float width = 0f;
		if (Input.GetKey (KeyCode.LeftControl)) {
			width = -1f;
			isSquat = true;
		} else
			isSquat = false;
		myCollection.localPosition = new Vector3 (0, width, 0);
	}
}