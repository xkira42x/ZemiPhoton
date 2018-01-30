using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AI : Photon.MonoBehaviour
{
    [SerializeField]
    GameObject Blood;
	protected Transform myTransform;
    /// ステータス
    public const byte IDOL = 0, RUN = 1, ATTACK = 2, HIT = 3, DIE = 4;
    [SerializeField]
    byte state = RUN;
    public byte State { get { return state; } set { state = value; } }
    /// ステータスを通常状態（走る）に戻し、ターゲットを再設定する
    public void MakeThenRun()
    {
        state = RUN;
        if (targetTransform != null)
            agent.SetDestination(targetTransform.position);
        AttackOnlyOnce = false;
		agent.Resume();
    }
    /// 速度
    [SerializeField]
    public float maxSpeed = 3.5f, minSpeed = 1f;
    /// 攻撃力
    [SerializeField]
    public float pow = 10;
    /// 体力
    [SerializeField]
    protected float health = 100;

    /// ナビメッシュエージェント（コンポーネント）
    [SerializeField]
    protected NavMeshAgent agent;
    /// ターゲットのトランスフォーム
    [SerializeField]
    protected Transform targetTransform;
    /// ターゲットの座標
    public Vector3 targetPos;
    /// ターゲットの識別番号
    protected int targetIndex;
    /// 射程
    [SerializeField]
    protected float range = 2.2f;

    public bool AttackOnlyOnce = false;

    /// 初期化
	public void Start()
    {
        // ナビエージェントの取得
        agent = GetComponent<NavMeshAgent>();
        // ターゲットの設定
        if (photonView.isMine) SetTarget();
        // 目標地点の設定
        StartCoroutine(SetDesti());
        // 速度の設定
        if (photonView.isMine)
            photonView.RPC("SyncSpeed", PhotonTargets.AllBufferedViaServer, Random.Range(minSpeed, maxSpeed));
        
        myTransform = transform;
    }

    /// メインループ
	public void Update()
    {
        // ステータスの設定
        AIState();

        // ターゲットの設定
        if (targetTransform == null) SetTarget();
    }

    /// 何番目のプレイヤーをターゲットにするかを設定する
    /// その番号を同期して、ターゲットの共有をする
    public void SetTarget()
    {
        // ターゲットを設定していない && プレイヤー数が0以上の時
        if (/*targetTransform == null &&*/ PlayerList.length > 0)
        {
            // ターゲット番号の設定
            targetIndex = Random.Range(0, PlayerList.length);
            // ターゲットの同期
            photonView.RPC("SyncTarget", PhotonTargets.AllBufferedViaServer, targetIndex);
        }
    }

    /// ターゲットとの距離を返す
    public float DistanceToTarger()
    {
        return agent.remainingDistance;
    }

    /// ステータス制御
    /// 攻撃範囲外なら走り、範囲内に入ったら攻撃をする
    public void AIState()
    {
        if (state != ATTACK && state != DIE)
        {
            if (DistanceToTarger() > range)
                state = RUN;
            else
            {
				state = ATTACK;
                agent.Stop();
            }
        }
    }

    /// 移動のための目的地を設定する
    /// 処理が重いとの事でコルーチンで回数制御をする
    public IEnumerator SetDesti()
    {
        while (true)
        {
            // ターゲットが設定されている && ステータスが走る状態の時
            if (targetTransform != null && state == RUN)
            {
                // このオブジェクトがナビメッシュが設定された場所にいるかを判定する
                //if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
                // 目的地の設定
                agent.SetDestination(targetTransform.position);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    /// ターゲットの同期
    [PunRPC]
    public void SyncTarget(int index)
    {
        targetIndex = index;
        targetTransform = PlayerList.GetPlayerList(index).transform;
		agent.SetDestination(targetTransform.position);
	}

    /// プレイヤーにダメージを与える
    public void AttackedTheTarget()
    {
        // 当たり判定で衝突したオブジェクトを格納
        Collider[] hit = Physics.OverlapSphere(transform.position, 1);
        // プレイヤーが当たっていたらダメージを与える
        for (int ii = 0; ii < hit.Length; ii++)
            if (hit[ii].gameObject.layer == 10)
                hit[ii].GetComponent<S2_Status>().Damage(pow);
    }

    /// 当たり判定
    public void OnCollisionEnter(Collision collision)
	{
		// 弾と当たった時
		if (collision.gameObject.tag == "Bullet") {
			var trns = collision.transform;
			Instantiate (Blood, trns.position, transform.rotation);

			if (health >= 0) {
				// 弾情報を取得
				Bullet bbb = collision.gameObject.GetComponent<Bullet> ();
				// 体力を減らし、0以下になったら死亡する
				health -= bbb.Pow;
				if (health <= 0 && state != DIE) {
					agent.Stop ();
					state = DIE;
					photonView.RPC ("SyncDie", PhotonTargets.Others);

					// 撃破数を保存
					if (collision.gameObject.GetComponent<Bullet> ().ID == PlayerInfo.playerNumber)
						PlayerInfo.killCount++;
				}
			}
		}
	}

    /// 死亡の同期
    [PunRPC]
    public void SyncDie()
    {
        agent.Stop();
        state = DIE;
    }

    [PunRPC]
    public void SyncSpeed(float spd)
    {
        if (agent != null)
            agent.speed = spd;
    }

}
