using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_Shot : Photon.MonoBehaviour {
	[SerializeField]Action _Shot;
	public Action Shot{ set { _Shot = value; } }
	[SerializeField]public Transform S_Collection;
	[SerializeField]GameObject GunSpot;
	[SerializeField]GunBase Gun;
	[SerializeField]LayerMask mask;

	void Start(){
		Gun.ShotSetting(this.GetComponent<S3_Shot>());
	}

	void Update () {
		// プレイヤーコントロール設定
		if (photonView.isMine) {
			// ショット
			if (Input.GetMouseButton (0)/* && !loading*/) {
				_Shot ();
			}
		}
	}
}