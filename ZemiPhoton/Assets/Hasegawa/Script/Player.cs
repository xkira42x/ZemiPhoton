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
		Eye ();
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
		if (Input.GetMouseButton (0)) {
			Shot ();
		}
	}

	void Move(){
		if (move) {
			transform.position += new Vector3 (
				Mathf.Sin ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed, 
				0, 
				Mathf.Cos ((transform.localEulerAngles.y + angle) * 3.14f / 180) * speed);
			move = false;
		}
	}

	void Eye(){
		mouseAngle += new Vector3 ((-(Input.GetAxis ("Mouse Y"))), (Input.GetAxis ("Mouse X")), 0);
		mainAngle = Quaternion.Euler (mouseAngle);
		transform.localRotation = mainAngle;
	}

	void Shot(){
		Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
	}
}
