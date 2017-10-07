using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour {
	
	[SerializeField]Action _action;
	public Action action{ set { _action = value; } }
	public void ShotAction (){_action ();}
	[SerializeField]Transform GunSpot;
	[SerializeField]GunBase MyGun;
	[SerializeField]LayerMask mask;
	[SerializeField]public Transform CameraT;
	[SerializeField]S3_Shot shot;
	[SerializeField]Text UI;

	RaycastHit hitInfo;

	void Start(){
		if (MyGun != null)
			MyGun.ShotSetting (shot);
	}

	void Update () {
		// プレイヤーコントロール設定

		// ショット
		if (Input.GetMouseButton (0)) {
			if (_action != null)
				_action ();
			else
				WriteUIText ("I do not have weapons");
		}

		if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
			WriteUIText ("Pick up with E key");
			if (Input.GetKeyDown (KeyCode.E)) {
				SendMessage ("PickUpItem", hitInfo.collider.gameObject);
			}
		}
	}

	void PickUpItem(GameObject obj){
		if (MyGun != null)
			MyGun.ThrowAway ();
		obj.transform.parent = GunSpot;
		MyGun = obj.GetComponent<GunBase> ();
		MyGun.ShotSetting (shot);
	}

	void WriteUIText(string UIText){
		UI.text = UIText;
		StartCoroutine ("ClearUIText");
	}
	IEnumerator ClearUIText(){
		yield return new WaitForSeconds (1);
		UI.text = "";
	}

}