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

	public string WeaponName;						// 銃の名前
	protected Transform CameraT;					// カメラのTransform情報
	[SerializeField]protected GameObject AmmoObj;	// 弾丸オブジェクト
	[SerializeField]protected int MaxAmmo;			// 所持総弾数
	[SerializeField]protected int MaxMagazine;		// マガジンの最大数
	protected int Magazine;							// 今のマガジン内弾数
    public int GetMagazine() { return Magazine; }
	public bool Reloading = false;					// リロード判定
	[SerializeField]protected float ReloadTime = 1;	// リロードする時間
	protected bool Next = true;						// 次に攻撃する時間間隔(フラグ)
	[SerializeField]public ParticleSystem[] MuzzleFlash;	// マズルフラッシュエフェクト
	[SerializeField]Vector3 Rotate;					// 銃を取得した際の向きを指定
	[SerializeField]Rigidbody myRigidbody;			// 物理処理コンポーネントのキャッシュ
	[SerializeField]protected float interval = .1f;

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
		if (Next && !Reloading) {
			// 残弾があれば
			if (Magazine > 0) {
				// 残弾を減らす
				Magazine--;
				// 弾を生成
				Instantiate (AmmoObj, CameraT.position, CameraT.rotation).GetComponent<Bullet>().ID = PlayerInfo.playerNumber;
				// エフェクトの再生
				PlayEffect ();
				Next = false;
				Delay ();
			} else // 弾切れの際のメッセージ
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
		Reloading = true;
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
		Reloading = false;
	}

	/// <summary>
	/// <para>名前　Delay</para>
	/// <para>概要　interval秒遅延してNextフラグをtrueにする</para>
	/// <para>引数　float interval　遅延時間(秒)</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void Delay(){
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
		// 重力を無効化
		myRigidbody.useGravity = false;
		// 物理処理を無効化
		myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		// 銃の向き・座標を初期化
		transform.localRotation = Quaternion.Euler (Rotate);
		transform.localPosition = Vector3.zero;
		// カメラ位置の設定（弾の撃たれる位置）
		CameraT = S_Shot.CameraT;
		// 残弾の初期化
		Magazine = MaxMagazine;
		// レイヤー変更（衝突する対象のいないレイヤー）
		gameObject.layer = LayerMask.NameToLayer ("Possessing");
	}

	/// <summary>
	/// <para>名前　ThrowAway</para>
	/// <para>概要　銃を捨てる(親子関係を解消する)</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void ThrowAway(){
		// 重力を有効化
		myRigidbody.useGravity = true;
		// 物理処理を有効化
		myRigidbody.constraints = RigidbodyConstraints.None;
		// 銃とプレイヤーの親子関係を切る
		transform.parent = null;
		// レイヤーを変更（拾えるアイテムの当たり判定を与える）
		gameObject.layer = LayerMask.NameToLayer ("Item");
	}

	/// <summary>
	/// <para>名前　PlayEffect</para>
	/// <para>概要　マズルフラッシュエフェクトを再生する</para>
	/// <para>引数　なし</para>
	/// <para>戻り値　なし</para>
	/// </summary>
	public void PlayEffect(){
		try{
		for (int i = 0; i < MuzzleFlash.Length; i++)
			MuzzleFlash [i].Play ();
		}
		catch{
			for (int i = 0; i < MuzzleFlash.Length; i++)
				MuzzleFlash [i] = null;
		}
	}

}
