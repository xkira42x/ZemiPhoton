using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1_Move : MonoBehaviour {
	float S_Speed = 0.1f;
	byte S_Type = 0;

	[SerializeField]
	Animator S_Animator;
	float S_Motion = 0;
	
	// Update is called once per frame
	void Update () {
		// キー移動
		S_KeyMove ();
		// ジャンプ
		if (Input.GetKeyDown (KeyCode.Space)) {
			S_Jump ();
		}
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
		// 移動
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
		// 角度設定
		switch (S_Type) {
		case Key.FORWARD:
			transform.localPosition += transform.forward * S_Speed;
			break;
		case Key.BACK:
			transform.localPosition += -(transform.forward * S_Speed);
			break;
		case Key.RIGHT:
			transform.localPosition += transform.right * S_Speed;
			break;
		case Key.LEFT:
			transform.localPosition += -(transform.right * S_Speed);
			break;
		case Key.FORWARDLEFT:
			transform.localPosition += transform.forward * S_Speed;
			transform.localPosition += -(transform.right * S_Speed);
			break;
		case Key.FORWARDRIGHT:
			transform.localPosition += transform.forward * S_Speed;
			transform.localPosition += transform.right * S_Speed;
			break;
		case Key.BACKLEFT:
			transform.localPosition += -(transform.forward * S_Speed);
			transform.localPosition += -(transform.right * S_Speed);
			break;
		case Key.BACKRIGHT:
			transform.localPosition += -(transform.forward * S_Speed);
			transform.localPosition += transform.right * S_Speed;
			break;
		case Key.NONE:
			break;
		default:
			Debug.Log ("Error :: Player move S_Type");
			break;
		}
		// モーション更新
		if (S_Type != Key.NONE)
			S_Motion = 1;
		else
			S_Motion = 0;
	}
}
