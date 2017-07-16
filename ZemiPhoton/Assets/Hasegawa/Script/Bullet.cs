using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	float speed = 1;
	Vector3 angle;
	Vector3 movement;
	float pow = 1;
	public float Pow{ get { return pow; } set { pow = value; } }
	// Use this for initialization
	void Start () {
		// 角度の保持
		angle = transform.localEulerAngles;
		// 移動量計算
		movement = new Vector3(
			Mathf.Sin (angle.y * 3.14f / 180) * speed, 
			-(Mathf.Tan (angle.x * 3.14f / 180) * speed), 
			Mathf.Cos (angle.y * 3.14f / 180) * speed);
		// 削除処理
		Destroy (this.gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		// 移動
		transform.position += movement;
	}
}
