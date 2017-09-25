using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Angle : MonoBehaviour {
	Vector3 S_MouseAngle = Vector3.zero;
	Quaternion S_MainAngle;
	public Quaternion S_mainAngle{ get { return S_MainAngle; } set { S_MainAngle = value; } }
	Quaternion S_CameraAngle;
	public Quaternion S_cameraAngle{ get { return S_CameraAngle; } set { S_CameraAngle = value; } }

	[SerializeField]
	Transform S_Collection;

	void Awake(){
		Cursor.lockState = CursorLockMode.Locked;	// カーソルを固定
		Cursor.visible = true;						// カーソルを隠す
	}

	void Update () {
		// 視線移動
		S_Eye ();
	}

	// 視線移動
	void S_Eye(){
		// マウス移動量を保存
		S_MouseAngle += new Vector3 (-(Input.GetAxis ("Mouse Y")), (Input.GetAxis ("Mouse X")), 0);
		// カメラの移動制限
		if (S_MouseAngle.x <= -60)S_MouseAngle.x = -60;
		else if (S_MouseAngle.x >= 60)S_MouseAngle.x = 60;
		// 角度に変換
		S_MainAngle = Quaternion.Euler (transform.localEulerAngles.x,S_MouseAngle.y,0);
		S_CameraAngle = Quaternion.Euler (S_MouseAngle.x, S_Collection.localEulerAngles.y, 0);
		// 角度の更新
		transform.localRotation = S_MainAngle;
		S_Collection.localRotation = S_CameraAngle;
	}
}