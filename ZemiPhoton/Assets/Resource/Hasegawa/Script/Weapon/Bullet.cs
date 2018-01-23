using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int ID;

	[SerializeField]protected float speed = 1;	// 弾速
	Vector3 movement;							// 移動量
	[SerializeField]protected short pow = 50;	// 威力
	public short Pow{ get { return pow; } set { pow = value; } }
	[SerializeField]protected float LifeTime = 1;	// 削除されるまでの時間
	float time;									// 経過時間

	/// 弾の向きから移動量を計算して保存する
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

	/// 経過時間分弾が落ちていく
	void Update () {
		time += Time.deltaTime;
		// 移動
		transform.position += new Vector3(movement.x,movement.y - ((9.8f * time)/200),movement.z);
	}

	/// 当たり判定（自分を消す）
	void OnCollisionEnter(){
		Destroy (gameObject);
	}

}