using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour {
	
	[SerializeField]Action _action;
	public Action action{ set { _action = value; } }
	[SerializeField]public Transform S_Collection;
	[SerializeField]Transform GunSpot;
	[SerializeField]public Transform MyTransform;
	[SerializeField]GunBase MyGun;
	[SerializeField]LayerMask mask;
	[SerializeField]public Transform Camera;
	[SerializeField]S3_Shot shot;
	[SerializeField]Text UI;

	public GameObject obj;

	RaycastHit hitInfo;

	void Start(){
		shot = GetComponent<S3_Shot> ();
		MyGun.ShotSetting (shot, GunSpot);
	}

	void Update () {
		// プレイヤーコントロール設定
		//if (photonView.isMine) {
		// ショット
		if (Input.GetMouseButton (0)) {
			_action ();
		}
		//}
		if (Physics.Raycast (Camera.position, Camera.forward, out hitInfo, 10, 1 << LayerMask.NameToLayer ("Item"))) {
			UI.text = "Pick uo with E key";
			if (Input.GetKeyDown (KeyCode.E)) {
				GameObject obj = hitInfo.collider.gameObject;
				MyGun.ThrowAway ();
				obj.transform.parent = gameObject.transform;
				MyGun = obj.GetComponent<GunBase> ();
				MyGun.ShotSetting (shot, GunSpot);
			}
		} else
			UI.text = "";
			
		obj.transform.position = (Camera.position + (Camera.forward * 10));
	}

}