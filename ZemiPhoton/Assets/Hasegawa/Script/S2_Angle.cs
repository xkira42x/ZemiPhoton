using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Angle : MonoBehaviour {
	Vector3 S_MouseAngle = Vector3.zero;
	Quaternion S_MainAngle;

	[SerializeField]
	Transform S_Collection;
	
	// Update is called once per frame
	void Update () {
		// 視線移動
		S_Eye ();
	}
	// 視線移動
	void S_Eye(){
		// マウス移動量を保存
		S_MouseAngle += new Vector3 (-(Input.GetAxis ("Mouse Y")), (Input.GetAxis ("Mouse X")), 0);
		if (S_MouseAngle.x <= -60)
			S_MouseAngle.x = -60;
		else if (S_MouseAngle.x >= 60)
			S_MouseAngle.x = 60;
		// 角度に変換
		S_MainAngle = Quaternion.Euler (S_MouseAngle);

		transform.localRotation = new Quaternion (0, S_MainAngle.y, 0, S_MainAngle.w);
		S_Collection.localRotation = new Quaternion (S_MainAngle.x, S_Collection.localRotation.y, S_Collection.localRotation.z, S_Collection.localRotation.w);
	}
}
