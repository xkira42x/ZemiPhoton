using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

	// 弾丸オブジェクト
	[SerializeField]protected GameObject AmmoObj;
	// 弾丸最大数
	protected int MaxAmmo;
	// 弾数
	protected int Ammo;
	// 攻撃の間隔フラグ
	protected bool Next = true;
	// マズルの座標
	[SerializeField]public Transform Muzzle;
	// プレイヤーの座標
	[SerializeField]public Transform Collection;
	// 自分の座標
	[SerializeField]public Transform MyTransform;
	// マズルフラッシュエフェクト
	[SerializeField]public ParticleSystem[] MuzzleFlash;

	[SerializeField]Vector3 Rotate;

	protected Transform Camera;

	/// <summary>
	/// <para>名前 　Action</para>
	/// <para>概要 　射撃アクションを記載する</para>
	/// <para>引数 　なし</para>
	/// <para>戻り値 なし</para>
	/// </summary>
	public virtual void Action(){
		if (Next) {
			Instantiate (AmmoObj, Muzzle.position, MyTransform.localRotation * Collection.localRotation);
			PlayEffect ();
			Next = false;
			Delay (.1f);
		}
	}

	public void Delay(float interval){
		StartCoroutine ("Del", interval);
	}

	IEnumerator Del(float il){
		yield return new WaitForSeconds (il);
		Next = true;
	}

	public void ShotSetting(S3_Shot S_Shot,Transform parent){
		transform.parent = parent;
		transform.Rotate (Rotate);
		transform.position = Vector3.zero;
		S_Shot.action = Action;
		Collection = S_Shot.S_Collection;
		MyTransform = S_Shot.MyTransform;
		Camera = S_Shot.Camera;
	}

	public void ThrowAway(){
		transform.parent = null;
	}

	public void PlayEffect(){
		for (int i = 0; i < MuzzleFlash.Length; i++)
			MuzzleFlash [i].Play ();
	}

}
