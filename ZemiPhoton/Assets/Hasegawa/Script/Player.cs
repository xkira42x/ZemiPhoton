using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	float angle;
	float speed = 0.1f;
	bool move = false;
	Vector3 mouseAngle = Vector3.zero;
	Quaternion mainAngle;

	[SerializeField]
	GameObject myBullet;
	[SerializeField]
	GameObject muzzle;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// 視線移動
		Eye ();
		// 走る
		if (Input.GetKey (KeyCode.LeftShift))
			speed = 0.2f;
		else
			speed = 0.1f;
		// 移動
		if (Input.GetKey (KeyCode.A)) {
			angle = 270;
			move = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			angle = 90;
			move = true;
		}
		Move ();
		if (Input.GetKey (KeyCode.W)) {
			angle = 0;
			move = true;
		}
		if (Input.GetKey (KeyCode.S)) {
			angle = 180;
			move = true;
		}
		Move ();
		// ショット
		if (Input.GetMouseButton (0)) {
			Shot ();
		}
		// ジャンプ
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
	}
	// 移動
	void Move(){
		if (move) {
			transform.position += new Vector3 (
				Mathf.Sin ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed, 
				0, 
				Mathf.Cos ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed);
			move = false;
		}
	}
	// 視線移動
	void Eye(){
		mouseAngle += new Vector3 ((-(Input.GetAxis ("Mouse Y"))), (Input.GetAxis ("Mouse X")), 0);
		mainAngle = Quaternion.Euler (mouseAngle);
		transform.localRotation = mainAngle;
	}
	// 撃つ
	void Shot(){
		Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
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
}
