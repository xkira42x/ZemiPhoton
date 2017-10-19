using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour {
	
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
			SendMessage ("ToAttackMSG", SendMessageOptions.DontRequireReceiver);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			MyGun.ReloadRequest ();
		}

		if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
			WriteUIText ("Pick up with E key");
			if (Input.GetKeyDown (KeyCode.E)) {
				SendMessage ("PickUpItemMSG", hitInfo.collider.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void ToAttackMSG(){
		if (MyGun != null) {
			MyGun.Action ();
		}
		else
			WriteUIText ("I do not have weapons");
	}

	void PickUpItemMSG(GameObject obj){
		if (MyGun != null)
			MyGun.ThrowAway ();
		obj.transform.parent = GunSpot;
		MyGun = obj.GetComponent<GunBase> ();
		MyGun.ShotSetting (shot);
	}

	void OutOfAmmoMSG(){
		WriteUIText ("Reload with R key");
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