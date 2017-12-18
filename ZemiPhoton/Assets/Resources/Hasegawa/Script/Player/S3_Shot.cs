using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour {
	
	[SerializeField]Transform GunSpot;					// 持っている銃が置かれる位置
	[SerializeField]GunBase MyGun;						// 銃情報インターフェイス
	[SerializeField]public Transform CameraT;			// カメラの位置情報
	[SerializeField]Text UI;							// 弾切れなどを表示するUI
    [SerializeField]Text NumberofRemainingBullets;		// 残弾数の表示
    bool ShowUI = false;								// UIの表示判定
	[SerializeField]S3_Shot shot;						// S3_Shotクラスのキャッシュ

	RaycastHit hitInfo;									// レイキャストで衝突した情報を格納する

	Item_State2 iState;									// 所持アイテムの表示先

	/// 初期化
	void Start(){
		// 銃が初期設定されていたら、撃てる状態にする
		if (MyGun != null)
			MyGun.ShotSetting (shot);
		// クラスのキャッシュ取得
		shot = GetComponent<S3_Shot> ();
		// 所持アイテムの表示先設定
		iState = GameObject.Find ("UI").GetComponent<Item_State2> ();
	}

	/// メインループ
	void Update () {
		
		// プレイヤーコントロール設定
		if (photonView.isMine) {
			
			// アタック(メッセージを送る)
			if (Input.GetMouseButton (0)) {
				gameObject.SendMessage ("ToAttackMSG", SendMessageOptions.DontRequireReceiver);
			}

			// リロード
			if (Input.GetKeyDown (KeyCode.R)) {
				MyGun.ReloadRequest ();
				UI.text = "";
			}

			// アイテムを取得できるか、レイキャストで判定する
			if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
				
				// Eキーを入力してもらう事を促すテキストを表示する
				WriteUIText ("Pick up with E key");

				// アイテム取得(メッセージを送る)
				if (Input.GetKeyDown (KeyCode.E)) {
					gameObject.SendMessage ("PickUpItemMSG", hitInfo.collider.gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}

			// 銃を持っている間、残弾数を表示する
			if (MyGun != null)
				iState.Number_of_remaining_bullets = MyGun.GetMagazine ();
		}
	}

	/// アイテムの取得
	public void PickUpItem(){
		if (Physics.Raycast (CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer ("Item"))) {
			PickUpItemMSG (hitInfo.collider.gameObject);
		}
	}

	/// アタックメッセージ
	/// 持っているアイテムを使用する。アイテムを持っていなければ持っていない事をUI表示する
	public void ToAttackMSG(){
		if (MyGun != null)
			MyGun.Action ();
		else
			WriteUIText ("I do not have weapons");
	}

	/// 
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