using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour {
	
	[SerializeField]Transform GunSpot;
	[SerializeField]GunBase MyGun;
	[SerializeField]public Transform CameraT;
	[SerializeField]Text UI;
	bool ShowUI = false;
	[SerializeField]S3_Shot shot;

	RaycastHit hitInfo;

	Item_State2 iState;

	void Start(){
		if (MyGun != null)
			MyGun.ShotSetting (shot);

		shot = GetComponent<S3_Shot> ();

		iState = GameObject.Find ("UI").GetComponent<Item_State2> ();
	}

	void Update () {
		// プレイヤーコントロール設定
		if (photonView.isMine) {
			// ショット
			if (Input.GetMouseButton (0)) {
				gameObject.SendMessage ("ToAttackMSG", SendMessageOptions.DontRequireReceiver);
			}

			if (Input.GetKeyDown (KeyCode.R)) {
				MyGun.ReloadRequest ();
				UI.text = "";
			}

			if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
				WriteUIText ("Pick up with E key");
				if (Input.GetKeyDown (KeyCode.E)) {
					gameObject.SendMessage ("PickUpItemMSG", hitInfo.collider.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}

			if (MyGun != null)
				iState.Number_of_remaining_bullets = MyGun.GetMagazine ();
		}
	}

	public void PickUpItem(){
		if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
			PickUpItemMSG (hitInfo.collider.gameObject);
		}
	}

	public void ToAttackMSG(){
		if (MyGun != null) {
			MyGun.Action ();
		}
		else
			WriteUIText ("I do not have weapons");
	}

	public void PickUpItemMSG(GameObject obj){
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
		if (UI != null) {
			UI.text = UIText;
			StartCoroutine ("ClearUIText");
			ShowUI = true;
		}
	}
	IEnumerator ClearUIText(){
		yield return new WaitForSeconds (1);
		if (ShowUI)
			ShowUI = false;
		else
			UI.text = "";
	}

}