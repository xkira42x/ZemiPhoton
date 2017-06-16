using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	float speed = 1;
	Vector3 angle;
	// Use this for initialization
	void Start () {
		angle = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (
			Mathf.Sin (angle.y * 3.14f / 180) * speed, 
			-(Mathf.Tan (angle.x * 3.14f / 180) * speed), 
			Mathf.Cos (angle.y * 3.14f / 180) * speed);
	}
}
