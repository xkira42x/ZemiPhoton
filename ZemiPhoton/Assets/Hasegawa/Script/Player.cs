using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	float S_Speed = 0.1f;
	byte S_Type = 0;
	bool S_Move = false;
	Vector3 S_MouseAngle = Vector3.zero;
	Quaternion S_MainAngle;

	[SerializeField]
	GameObject S_Bullet;
	[SerializeField]
	Transform S_Muzzle;
	[SerializeField]
	Transform S_Collection;
	[SerializeField]
	Animator S_Animator;

	float S_Motion = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// 視線移動
		Eye ();
		// キー移動
		KeyMove ();
		// ショット
		if (Input.GetMouseButton (0)) {
			Shot ();
		}
		// ジャンプ
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}

		UnityChanAnimation ();
	}

	// 視線移動
	void Eye(){
		// マウス移動量を保存
		S_MouseAngle += new Vector3 (-(Input.GetAxis ("Mouse Y")), (Input.GetAxis ("Mouse X")), 0);
		if (S_MouseAngle.x <= -10)
			S_MouseAngle.x = -10;
		else if (S_MouseAngle.x >= 30)
			S_MouseAngle.x = 30;
		// 角度に変換
		S_MainAngle = Quaternion.Euler (S_MouseAngle);

		transform.localRotation = new Quaternion (0, S_MainAngle.y, 0, S_MainAngle.w);
		S_Collection.localRotation = new Quaternion (S_MainAngle.x, S_Collection.localRotation.y, S_Collection.localRotation.z, S_Collection.localRotation.w);
	}
	// 撃つ
	void Shot(){
		Instantiate (S_Bullet, S_Muzzle.position, transform.localRotation);
	}
	// ジャンプ
	void Jump(){
		StartCoroutine (Jumping ());
	}
	// ジャンプの中身
	IEnumerator Jumping(){
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
	void UnityChanAnimation(){
		S_Animator.SetFloat ("Speed", S_Motion);
	}

	// キー移動判定
	void KeyMove(){
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
