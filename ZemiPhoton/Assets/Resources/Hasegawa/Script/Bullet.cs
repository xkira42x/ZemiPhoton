using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	protected float speed = 1;
	Vector3 angle;
	Vector3 movement;
	protected short pow = 50;
	public short Pow{ get { return pow; } set { pow = value; } }
	float time;

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
		Destroy (gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		// 移動
		transform.position += new Vector3(movement.x,movement.y - ((9.8f * time)/100),movement.z);
	}
}
