using UnityEngine;
using System.Collections;

public class EnemySpawn_2 : MonoBehaviour {

	public GameObject enemy;     //敵オブジェクト
	public Transform ground;     //地面オブジェクト
	public Transform enemyground;     //地面オブジェクト
	public float A_count = 50;   //一度に何体のオブジェクトをスポーンさせるか
	public float A_interval = 10;//何秒おきに敵を発生させるか
	public float A_enemy_max = 100;
	private float A_timer;      //経過時間
	int A_enemy_cnt = 1; //生成された敵の数

	// Use this for initialization
	void Start () {
		Spawn();    //初期スポーン
	}

	// Update is called once per frame
	void Update () {
		A_timer += Time.deltaTime;    //経過時間加算
		if(A_timer >= A_interval){
			Spawn();    //スポーン実行
			A_timer = 0;  //初期化
		}
	}

	void Spawn () {
		float x=0;
		float z=0;

		if (A_enemy_cnt <= A_enemy_max) {
			for (int i = 0; i < A_count; i++) {
				x = Random.Range (-50f, 50f);
				z = Random.Range (-50f, 50f);

				Vector3 pos = new Vector3 (x, 3, z) + enemyground.position;
				GameObject enemy_notClone = GameObject.Instantiate (enemy, pos, Quaternion.identity);
				enemy_notClone.name = enemy.name + A_enemy_cnt.ToString();
				Debug.Log (A_enemy_cnt);
				A_enemy_cnt++;

				/*
				//ステージの外周に敵をスポーンさせる
				do {
					x = Random.Range (-150f, 150f);
					z = Random.Range (-150f, 150f);
				} while(x <= 120 && x >= -120 && z <= 120 && z >= -120);

				Vector3 pos = new Vector3 (x, 3, z) + ground.position;
				GameObject enemy_notClone = GameObject.Instantiate (enemy, pos, Quaternion.identity);
				enemy_notClone.name = enemy.name + A_enemy_cnt.ToString();
				Debug.Log (A_enemy_cnt);
				A_enemy_cnt++;

				*/
			}

		}

	}



}
