using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_Shot : Photon.MonoBehaviour {
	[SerializeField]
	GameObject S_Bullet;
	[SerializeField]
	Transform S_Muzzle;
	[SerializeField]
	Transform S_Collection;
	// ショットフラグ
	bool S_SHOOTING = false;
	public bool S_Shooting{ get { return S_SHOOTING; } set { S_SHOOTING = value; } }

	
	// Update is called once per frame
	void Update () {
		// プレイヤーコントロール設定
		if (photonView.isMine) {
			// ショット
			if (Input.GetMouseButton (0))
				S_SHOOTING = true;
			else
				S_SHOOTING = false;
		}

		if (S_SHOOTING) {
			S_Shot ();
		}
	}
	// 撃つ
	void S_Shot(){
		Instantiate (S_Bullet, S_Muzzle.position, transform.localRotation * S_Collection.localRotation);
	}
}
