using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour {

	// 移動速度
	float S_Speed = 0.1f;
	// 移動方向
	byte S_Type = 0;

	const byte NONE=0,UP=1,DOWN=2;
	byte S_Jtype = NONE;
	float JumpGravity;

	[SerializeField]
	bool isGround;
	void IsGround(){isGround = Physics.Raycast (transform.position, Vector3.down, 1.6f);}

	void Start(){
		
	}

	void Update(){
			MyMain ();
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
		case Key.FORWARD:pos += transform.forward * S_Speed;break;
		case Key.BACK	:pos -= transform.forward * S_Speed;break;
		case Key.RIGHT	:pos += transform.right * S_Speed;break;
		case Key.LEFT	:pos -= transform.right * S_Speed;break;
		case Key.FORWARDLEFT	:pos += (transform.forward - transform.right) * S_Speed;break;
		case Key.FORWARDRIGHT	:pos += (transform.forward + transform.right) * S_Speed;break;
		case Key.BACKLEFT		:pos -= (transform.forward + transform.right) * S_Speed;break;
		case Key.BACKRIGHT		:pos -= (transform.forward - transform.right) * S_Speed;break;
		case Key.NONE:break;
		default:Debug.Log ("Error :: Player move S_Type");break;
		}
		transform.localPosition += pos;
	}
	// ジャンプ
	void S_Jump(){
		// ジャンプスイッチ
		if (isGround && Input.GetKeyDown (KeyCode.Space)) {
			S_Jtype = UP;
			JumpGravity = 0.3f;
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
		S_Jump();

		IsGround ();
	}
}
