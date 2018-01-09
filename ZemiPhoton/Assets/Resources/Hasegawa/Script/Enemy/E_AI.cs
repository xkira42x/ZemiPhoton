using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AI : Photon.MonoBehaviour {

	/// ステータス
	public const byte IDOL=0,RUN=1,ATTACK=2,HIT=3,DIE=4;
	[SerializeField]byte state = RUN;
	public byte State{ get { return state; } set { state = value; } }
	/// ステータスを通常状態（走る）に戻し、ターゲットを再設定する
	public void MakeThenRun(){
		state = RUN;
		if (targetTransform != null)
			agent.SetDestination (targetTransform.position);
	}
	/// 攻撃力
	[SerializeField]float pow = 10;
	/// 体力
	[SerializeField]float health = 100;

	/// ナビメッシュエージェント（コンポーネント）
	[SerializeField]NavMeshAgent agent;
	/// ターゲットのトランスフォーム
	[SerializeField]Transform targetTransform;
	/// ターゲットの座標
	Vector3 targetPos;
	/// ターゲットの識別番号
	int targetIndex;


	/// 初期化
	void Start () {

		// ナビエージェントの取得
		agent = GetComponent<NavMeshAgent> ();
		// ターゲットの設定
		if(photonView.isMine)SetTarget ();
		// 目標地点の設定
		StartCoroutine (SetDesti ());
	}

	/// メインループ
	void Update () {
		
		// ステータスの設定
		AIState ();

		if (Input.GetKeyDown (KeyCode.L))
		if (photonView.isMine)
			SetTarget ();

	}

	/// 何番目のプレイヤーをターゲットにするかを設定する
	/// その番号を同期して、ターゲットの共有をする
	void SetTarget(){
		Debug.Log ("Resetting the target");
		// ターゲットを設定していない && プレイヤー数が0以上の時
		if (/*targetTransform == null &&*/ PlayerList.length > 0) {
			// ターゲット番号の設定
			targetIndex = Random.Range (0, PlayerList.length);
			Debug.Log ("target : " + targetIndex);
			// ターゲットの同期
			photonView.RPC ("SyncTarget", PhotonTargets.AllBufferedViaServer, targetIndex);
		}
	}

	/// ターゲットとの距離を返す
	public float DistanceToTarger(){
		return agent.remainingDistance;
	}

	/// ステータス制御
	/// 攻撃範囲外なら走り、範囲内に入ったら攻撃をする
	void AIState(){
		if (state != ATTACK) {
			if (DistanceToTarger() > 2.2f)
				state = RUN;
			else
				state = ATTACK;
		}
	}

	/// 移動のための目的地を設定する
	/// 処理が重いとの事でコルーチンで回数制御をする
	IEnumerator SetDesti(){
		while (true) {
			// ターゲットが設定されている && ステータスが走る状態の時
			if (targetTransform != null && state == RUN) {
				// このオブジェクトがナビメッシュが設定された場所にいるかを判定する
				//if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
				// 目的地の設定
				agent.SetDestination (targetTransform.position);
			}
			yield return new WaitForSeconds (1);
		}
	}

	/// ターゲットの同期
	[PunRPC]
	void SyncTarget(int index){
		targetIndex = index;
		targetTransform = PlayerList.GetPlayerList (index).transform;
	}

	/// プレイヤーにダメージを与える
	public void AttackedTheTarget(){
		PlayerList.GetPlayerList (targetIndex).GetComponent<S2_Status> ().Damage (pow);
	}

	/// 当たり判定
	void OnCollisionEnter(Collision collision){
		// 弾と当たった時
		if (collision.gameObject.tag == "Bullet" && state != DIE) {
			// 弾情報を取得
			Bullet bbb = collision.gameObject.GetComponent<Bullet> ();

			// 体力を減らし、0以下になったら死亡する
			health -= bbb.Pow;
			if (health <= 0)
				photonView.RPC ("SyncDie", PhotonTargets.AllBuffered);
		}
	}

	/// 死亡の同期
	[PunRPC]
	void SyncDie(){
		state = DIE;
	}
}
