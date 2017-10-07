using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

	// カメラのTransform情報
	protected Transform CameraT;
	// 弾丸オブジェクト
	[SerializeField]protected GameObject AmmoObj;
	// 弾丸最大数
	protected int MaxAmmo;
	// 弾数
	protected int Ammo;
	// 攻撃の間隔フラグ
	protected bool Next = true;
	// マズルフラッシュエフェクト
	[SerializeField]public ParticleSystem[] MuzzleFlash;
	// 銃を取得した際の向きを指定
	[SerializeField]Vector3 Rotate;

	/// <summary>
	/// <para>名前 　Action</para>
	/// <para>概要 　射撃アクションを記載する</para>
	/// <para>引数 　なし</para>
	/// <para>戻り値 なし</para>
	/// <para></para>
	/// <para>＊手引き＊</para>
	/// <para>void PlayEffect()</para>
	/// <para>弾を発射する時のマズルフラッシュ用エフェクトを再生する</para>
	/// <para>bool Next</para> 
	/// <para>弾が発射されて次の弾が装填される間のフラグ変数</para>
	/// <para>void Delay(interval)</para>
	/// <para>interval秒遅延をさせてNextフラグをtrueにする</para>
	/// <para>＊＊＊＊＊</para>
	/// </summary>
	public virtual void Action(){
		if (Next) {
			Instantiate (AmmoObj, CameraT.position, CameraT.rotation);
			PlayEffect ();
			Next = false;
			Delay (.1f);
		}
	}

	/// <summary>
	/// <para>名前　Delay</para>
	/// <para>概要　interval秒遅延してNextフラグをtrueにする</para>
	/// <para>引数　float interval　遅延時間(秒)</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void Delay(float interval){
		StartCoroutine ("Del", interval);
	}

	/// <summary>
	/// <para>名前　Del</para>
	/// <para>概要　Delayの内容を実際に行うメソッド</para>
	/// <para>このメソッドは隠すことを前提としたて、Delayに遅延秒数を渡すだけで済ませるための作り</para>
	/// <para>引数　float il　遅延時間(秒)</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	IEnumerator Del(float il){
		yield return new WaitForSeconds (il);
		Next = true;
	}

	/// <summary>
	/// <para>名前　ShotSetting</para>
	/// <para>概要　プレイヤーが銃を獲得した際に銃の位置や発射するための設定</para>
	/// <para>引数　S3_Shot S_Shot　弾を発射するためのクラス</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void ShotSetting(S3_Shot S_Shot){
		transform.localRotation = Quaternion.Euler (Rotate);
		transform.localPosition = Vector3.zero;
		S_Shot.action = Action;
		CameraT = S_Shot.CameraT;
	}

	/// <summary>
	/// <para>名前　ThrowAway</para>
	/// <para>概要　銃を捨てる(親子関係を解消する)</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void ThrowAway(){
		transform.parent = null;
	}

	/// <summary>
	/// <para>名前　PlayEffect</para>
	/// <para>概要　マズルフラッシュエフェクトを再生する</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void PlayEffect(){
		for (int i = 0; i < MuzzleFlash.Length; i++)
			MuzzleFlash [i].Play ();
	}

}
