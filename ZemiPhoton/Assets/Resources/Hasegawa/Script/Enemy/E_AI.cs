﻿using System.Collections;
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
	[SerializeField]protected float pow = 10;
	/// 体力
	[SerializeField]protected float health = 100;

	/// ナビメッシュエージェント（コンポーネント）
	[SerializeField]protected NavMeshAgent agent;
	/// ターゲットのトランスフォーム
	[SerializeField]protected Transform targetTransform;
	/// ターゲットの座標
	protected Vector3 targetPos;
	/// ターゲットの識別番号
	protected int targetIndex;
	/// 射程
	[SerializeField]protected float range = 2.2f;

	/// 初期化
	public void Start () {

		// ナビエージェントの取得
		agent = GetComponent<NavMeshAgent> ();
		// ターゲットの設定
		if(photonView.isMine)SetTarget ();
		// 目標地点の設定
		StartCoroutine (SetDesti ());
	}

	/// メインループ
	public void Update () {
		
		// ステータスの設定
		AIState ();
	}

	/// 何番目のプレイヤーをターゲットにするかを設定する
	/// その番号を同期して、ターゲットの共有をする
	public void SetTarget(){
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
	public void AIState(){
		if (state != ATTACK) {
			if (DistanceToTarger () > range)
				state = RUN;
			else
				state = ATTACK;
		}
	}

	/// 移動のための目的地を設定する
	/// 処理が重いとの事でコルーチンで回数制御をする
	public IEnumerator SetDesti(){
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
	public void SyncTarget(int index){
		targetIndex = index;
		targetTransform = PlayerList.GetPlayerList (index).transform;
	}

	/// プレイヤーにダメージを与える
	public virtual void AttackedTheTarget(){
		PlayerList.GetPlayerList (targetIndex).GetComponent<S2_Status> ().Damage (pow);
	}

	/// 当たり判定
	public void OnCollisionEnter(Collision collision){
		// 弾と当たった時
		if (collision.gameObject.tag == "Bullet" && state != DIE) {
			// 弾情報を取得
			Bullet bbb = collision.gameObject.GetComponent<Bullet> ();

			// 体力を減らし、0以下になったら死亡する
			health -= bbb.Pow;
			if (health <= 0) {
				photonView.RPC ("SyncDie", PhotonTargets.AllBuffered);


				if (collision.gameObject.GetComponent<Bullet> ().ID == PlayerInfo.playerNumber) {
					PlayerInfo.killCount++;
				}
			}
		}
	}

	/// 死亡の同期
	[PunRPC]
	public void SyncDie(){
		OnDied ();
		state = DIE;
	}

	/// 倒された時に呼ばれる
	public virtual void OnDied(){
	}
}
