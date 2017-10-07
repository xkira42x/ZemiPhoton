using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField]protected float speed = 1;
	Vector3 movement;
	[SerializeField]protected short pow = 50;
	public short Pow{ get { return pow; } set { pow = value; } }
	[SerializeField]protected float LifeTime = 1;
	float time;

	void Start () {
		// 角度の保持
		Vector3 angle = transform.localEulerAngles;
		// 移動量計算
		movement = new Vector3(
			Mathf.Sin (angle.y * 3.14f / 180) * speed, 
			-(Mathf.Tan (angle.x * 3.14f / 180) * speed), 
			Mathf.Cos (angle.y * 3.14f / 180) * speed);
		// 削除処理
		Destroy (gameObject, LifeTime);
	}
	
	void Update () {
		time += Time.deltaTime;
		// 移動
		transform.position += new Vector3(movement.x,movement.y - ((9.8f * time)/200),movement.z);
	}
}