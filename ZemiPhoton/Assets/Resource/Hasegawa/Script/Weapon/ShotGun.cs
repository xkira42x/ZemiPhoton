﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase {
	[SerializeField]float Diffusivity = 4; // 拡散率

	/// 初期化
	public override void Awake (){
		base.Awake ();
	}

	/// 弾を撃つ
	public override void Action (){
		// 残弾があり、次の弾が装填されたら攻撃する
		if (Magazine > 0) {
			if (Next && !Reloading) {
				// 残弾を減らす
				Magazine--;
				// 弾をランダムな方向に弾を生成する
				for (int i = 0; i < 10; i++)
					Instantiate (AmmoObj, CameraT.position, Quaternion.Euler (Vec3Rand) * CameraT.rotation).GetComponent<Bullet> ().ID = PlayerInfo.playerNumber;
				// エフェクトの再生
				PlayEffect ();
				// フラッシュの再生
				StartCoroutine (PlayFlash ());
				// 音の再生
				audioSource.PlayOneShot (ShotSound);
				// 次弾装填
				Next = false;
				Delay ();
			}
		} else // リロードの催促メッセージ
			gameObject.SendMessageUpwards ("OutOfAmmoMSG", SendMessageOptions.DontRequireReceiver);
	}
	/// ランダムにベクトルを吐き出す
	Vector3 Vec3Rand { get { return new Vector3 (Random.Range (-Diffusivity, Diffusivity), Random.Range (-Diffusivity, Diffusivity), Random.Range (-Diffusivity, Diffusivity)); } }
}