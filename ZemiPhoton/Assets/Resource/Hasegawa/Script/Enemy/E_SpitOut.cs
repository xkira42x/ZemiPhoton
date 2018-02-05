using UnityEngine;

public class E_SpitOut : MonoBehaviour {

    /// ParticleSystemのキャッシュ
    public ParticleSystem particleSystem = new ParticleSystem();
    /// パーティクルの当たり判定イベントを格納する
    ParticleCollisionEvent[] collisionEvents = null;

    /// げろ溜オブジェクト
    [SerializeField]
    GameObject geroPuddle;
    /// Transformのキャッシュ
    Transform myTrns;
    /// 吐き出す速度
    [SerializeField]
    float speed = 1f;

    /// 初期化
	void Start () {
        myTrns = transform;
	}

    /// げろを吐き出す
    public void ShotGero(Vector3 target)
    {
        // ターゲットとの距離を割り出して、そこに届くように初期速度を決定する
        // そして吐く
        float ss = Vector3.Distance(myTrns.position, target);
        particleSystem.startSpeed = ss * speed;
        particleSystem.Play();
    }

    /// パーティクルの当たり判定
    void OnParticleCollision(GameObject other)
    {
        // 当たり判定のイベント数を格納
        int safeLength = particleSystem.GetSafeCollisionEventSize();
        // 当たりイベントの数宇がイベント格納配列以下だった場合、イベント配列を取得する
        if (collisionEvents == null || collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];

        // パーティクルに当たったオブジェクトとコリジョンイベントの情報を照合して、要素数を取得する
        ///int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        // 当たった座標からげろ溜を生成する
        int ii = 0;
        while (ii < safeLength)
        {
            Instantiate(geroPuddle, collisionEvents[ii].intersection, Quaternion.Euler(new Vector3(0, 0, -90)));
            Debug.Log("Position => " + collisionEvents[ii].intersection);
            ii++;
        }
    }
}
