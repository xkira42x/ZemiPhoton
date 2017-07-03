using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	float angle;
	float speed = 0.1f;
	byte type = 0;
	bool move = false;
	Vector3 mouseAngle = Vector3.zero;
	Quaternion mainAngle;

	[SerializeField]
	GameObject myBullet;
	[SerializeField]
	Transform muzzle;
	[SerializeField]
	Transform Head;
	[SerializeField]
	Transform myCollection;
	[SerializeField]
	Animator animator;

	float motion = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// 視線移動
		Eye ();
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
		mouseAngle += new Vector3 (-(Input.GetAxis ("Mouse Y")), (Input.GetAxis ("Mouse X")), 0);
		if (mouseAngle.x <= -10)
			mouseAngle.x = -10;
		// 角度に変換
		mainAngle = Quaternion.Euler (mouseAngle);

		transform.localRotation = new Quaternion (0, mainAngle.y, 0, mainAngle.w);
		myCollection.localRotation = new Quaternion (mainAngle.x, myCollection.localRotation.y, myCollection.localRotation.z, myCollection.localRotation.w);
	}
	// 撃つ
	void Shot(){
		Instantiate (myBullet, muzzle.position, transform.localRotation);
	}
	// ジャンプ
	void Jump(){
		StartCoroutine (Jumping ());
	}
	// ジャンプの中身
	IEnumerator Jumping(){
		Vector3 move = new Vector3 (0, 0.1f, 0);
		while (true) {
			transform.position += move;
			move.y -= 0.001f;
			if (move.y <= 0)
				break;
			yield return new WaitForSeconds (0.01f);
		}
	}
	// Unityちゃんモーション
	void UnityChanAnimation(){
		animator.SetFloat ("Speed", motion);
	}

	// キー移動判定
	void KeyMove(){
		type = Key.NONE;
		// 走る
		if (Input.GetKey (KeyCode.LeftShift))
			speed = 0.2f;
		else
			speed = 0.1f;
		// 移動
		if (Input.GetKey (KeyCode.A)) {
			type += Key.LEFT;
		}
		if (Input.GetKey (KeyCode.D)) {
			type += Key.RIGHT;
		}
		if (Input.GetKey (KeyCode.W)) {
			type += Key.UP;
		}
		if (Input.GetKey (KeyCode.S)) {
			type += Key.DOWN;
		}

		switch (type) {
		case Key.UP:
			angle = 0;
			break;
		case Key.DOWN:
			angle = 180;
			break;
		case Key.RIGHT:
			angle = 90;
			break;
		case Key.LEFT:
			angle = 270;
			break;
		case Key.UPLEFT:
			angle = 315;
			break;
		case Key.UPRIGHT:
			angle = 45;
			break;
		case Key.DOWNLEFT:
			angle = 225;
			break;
		case Key.DOWNRIGHT:
			angle = 135;
			break;
		case Key.NONE:
			break;
		default:
			Debug.Log ("Error :: Player move type");
			break;
		}
		if (type != 0)
			move = true;

		Move ();
	}

	// 移動
	void Move(){
		Vector3 movement = Vector3.zero;
		if (move) {
			movement = new Vector3 (
				Mathf.Sin ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed, 
				0, 
				Mathf.Cos ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed);
			transform.position += movement;
			move = false;
			motion = 1;
		} else
			motion = 0;
	}

}
