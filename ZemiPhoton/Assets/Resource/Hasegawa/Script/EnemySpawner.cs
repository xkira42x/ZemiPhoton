using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	const int Zombie = 0,Boomer = 1,Spitter = 2;
	public GameObject[] enemy;					// 敵オブジェクト
	[SerializeField]Transform[] SpawnPosition;	// 生成位置

	[SerializeField] int maxNum = 20;			// 最大生成数
	[SerializeField] int numGenerated = 0;		// 現在の敵数

	bool DoOnce = false;						// 一度だけ実行するフラグ

	[SerializeField] bool ZombieOnly = true;

	/// 初期化
	void Start(){
		
	}

	/// メインループ
	void Update(){
		//DoOnceがfalseの最初だけコルーチンを動かす
		if (PlayerInfo.Spawn && !DoOnce) {
			StartCoroutine ("GenerateIntervals");
			DoOnce = true;
		}
	}

	/// 生成する敵の最大数から、足りない分の敵を生成する
	IEnumerator Spawn () {
		int index = 0;			// スポーン位置のインデックス
		Vector3 target;			// ターゲット座標
		float dist;				// スポーン位置とターゲットの距離
		int type;				// 敵のタイプ

		// ターゲット（プレイヤー）の座標をランダムで取得
		target = PlayerList.GetPlayerPosition_Shuffle ();

		// ターゲットと距離が一番近いスポーン位置を探索
		dist = Vector3.Distance (target, SpawnPosition [0].position);
		for(int ii = 1;ii < SpawnPosition.Length;ii++){
			float dd = Vector3.Distance (target, SpawnPosition [ii].position);
			if (dd < dist) {
				dist = dd;
				index = ii;
			}
		}

		type = 0;

		// フィールド上にいる敵の数が最大生成数になるまで生成する
		for (int jj = numGenerated; jj < maxNum; jj++) {
			if (!ZombieOnly) {
				int rand = Random.Range (0, 100);
				if (rand < 80)
					type = Zombie;
				else if (rand >= 80 && rand < 90)
					type = Boomer;
				else if (rand >= 90 && rand < 100)
					type = Spitter;
			}
			
			/*GameObject obj =*/ PhotonNetwork.Instantiate (enemy [type].name,
				                 new Vector3 (Random.Range (-3f, 3f), 0, Random.Range (-3f, 3f)) + SpawnPosition [index].position,
				                 Quaternion.identity, 0);
			//obj.GetPhotonView ().RPC ("SyncSpeed", PhotonTargets.All, Random.Range (2.5f, 6.5f));

			yield return new WaitForSeconds (.1f);
		}
	}

	/// 定期的にフィールド上にいる敵の数を求めて、生成数が一定以下になった際に生成を再開する
	public IEnumerator GenerateIntervals(){
		while (true) {
			yield return new WaitForSeconds (10f);
			numGenerated = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (numGenerated < maxNum && PlayerList.length > 0)
				StartCoroutine (Spawn ());
		}
	}
}
