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
	[SerializeField]
	GameObject MuzzleFlash;
	ParticleSystem[] MuzzleFlashEffect = new ParticleSystem[2];

	// インターバル
	float interval = 0.1f;
	public float Interval{ get { return interval; } set { interval = value; } }
	// ショットフラグ
	bool S_shoot = false;
	bool loading = false;
	public bool S_Shoot{ get { return S_shoot; } set { S_shoot = value; } }

	void Start(){
		MuzzleFlashEffect = MuzzleFlash.GetComponentsInChildren<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		// プレイヤーコントロール設定
		if (photonView.isMine) {
			// ショット
			if (Input.GetMouseButton (0) && !loading) {
				S_shoot = true;
				loading = true;
				StartCoroutine (NextLoading ());
			} else
				S_shoot = false;
		}

		if (S_shoot) {
			S_Shot ();
		} else {
			for (int i = 0; i < MuzzleFlashEffect.Length; i++) {
				MuzzleFlashEffect [i].Stop ();
			}
		}
	}
	// 撃つ
	void S_Shot(){
		for (int i = 0; i < MuzzleFlashEffect.Length; i++) {
			MuzzleFlashEffect [i].Play ();
		}
		Instantiate (S_Bullet, S_Muzzle.position, transform.localRotation * S_Collection.localRotation);
	}
	// 装填中
	IEnumerator NextLoading(){
		yield return new WaitForSeconds (interval);
		loading = false;
	}
}
