using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// <para>名前　GunBase</para>
/// <para>概要　銃の派生親、銃の動作に必要な</para>
/// <para>次段装填の待ちや銃を拾った際の設定などがある</para>
/// </summary>
public class GunBase : MonoBehaviour {

	public string WeaponName;
	// カメラのTransform情報
	protected Transform CameraT;
	// 弾丸オブジェクト
	[SerializeField]protected GameObject AmmoObj;
	// 所持総弾数
	[SerializeField]protected int MaxAmmo;
	// マガジンの最大数
	[SerializeField]protected int MaxMagazine;
	// 今のマガジン内弾数
	protected int Magazine;
    public int GetMagazine() { return Magazine; }
	[SerializeField]protected float ReloadTime = 1;
	// 攻撃の間隔フラグ
	protected bool Next = true;
	// マズルフラッシュエフェクト
	[SerializeField]public ParticleSystem[] MuzzleFlash;
	// 銃を取得した際の向きを指定
	[SerializeField]Vector3 Rotate;
	// 物理処理
	[SerializeField]Rigidbody myRigidbody;

	/// <summary>
	/// <para>名前　Start</para>
	/// <para>概要　初期化処理</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public virtual void Awake(){
		myRigidbody = GetComponent<Rigidbody> ();
		gameObject.name = WeaponName;
	}

	/// <summary>
	/// <para>名前 　Action</para>
	/// <para>概要 　射撃アクションを記載する</para>
	/// <para>引数 　なし</para>
	/// <para>戻り値 なし</para>
	/// </summary>
	public virtual void Action(){
		if (Next) {
			if (Magazine > 0) {
				Magazine--;
				Instantiate (AmmoObj, CameraT.position, CameraT.rotation);
				PlayEffect ();
				Next = false;
				Delay (.1f);
			} else
				gameObject.SendMessageUpwards ("OutOfAmmoMSG", SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// <para>名前　ReloadRequest</para>
	/// <para>概要　リロード指示を出す</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void ReloadRequest(){
		StartCoroutine ("Reload", ReloadTime);
	}

	/// <summary>
	/// <para>名前　Reload</para>
	/// <para>概要　li秒遅延してマガジンの弾数を最大数にする</para>
	/// <para>引数　float il　遅延時間(秒)</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	IEnumerator Reload(float il){
		yield return new WaitForSeconds (il);
		Magazine = MaxMagazine;
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
		myRigidbody.useGravity = false;
		myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		transform.localRotation = Quaternion.Euler (Rotate);
		transform.localPosition = Vector3.zero;
		CameraT = S_Shot.CameraT;
		Magazine = MaxMagazine;
		gameObject.layer = LayerMask.NameToLayer ("Possessing");
	}

	/// <summary>
	/// <para>名前　ThrowAway</para>
	/// <para>概要　銃を捨てる(親子関係を解消する)</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void ThrowAway(){
		myRigidbody.useGravity = true;
		myRigidbody.constraints = RigidbodyConstraints.None;
		transform.parent = null;
		gameObject.layer = LayerMask.NameToLayer ("Item");
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
