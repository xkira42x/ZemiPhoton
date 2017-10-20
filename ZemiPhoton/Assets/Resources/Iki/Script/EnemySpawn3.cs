using UnityEngine;
using System.Collections;

public class EnemySpawn3 : Photon.MonoBehaviour {

	public GameObject enemy;     //敵オブジェクト
	public Transform Spawner_A;     //地面オブジェクト
	public Transform Spawner_B;     //地面オブジェクト
	public Transform Spawner_C;     //地面オブジェクト
	public Transform Spawner_D;     //地面オブジェクト

	public Transform enemyground;     //地面オブジェクト
	public float A_count = 50;   //一度に何体のオブジェクトをスポーンさせるか
	public float A_interval = 10;//何秒おきに敵を発生させるか
	public float A_enemy_max = 100;//最大湧数
	private float A_timer;      //経過時間
	int A_enemy_cnt = 1; //生成された敵の数
	int A_enemy_i = 1;
	int j;

	private object[] args;

	bool spnflg=true;

	void Update () {
		A_timer += Time.deltaTime;    //経過時間加算
		if (A_timer >= A_interval) { //時間がインターバルをすぎたら
			if (PhotonManager.EnteringTheRoom) {//
				j = Random.Range (1,4);//スポナー１～４からランダムで１つ選択
				Spawn ();    //スポーン実行
				A_timer = 0;  //時間初期化
				if (spnflg == true) {
					Spawn_stage ();
					spnflg = false;
				
				}
			}
		}
	}

	void Spawn () {
		float x=0;
		float z=0;

		if (A_enemy_cnt <= A_enemy_max) {//敵の最大値が生成された敵以上だったら
			for (int i = 0; i < A_count; i++) {
				x = Random.Range (-25f, 25f);
				z = Random.Range (-25f, 25f);

				Vector3 pos = new Vector3 (x, 3, z) + enemyground.position;
				GameObject enemy_notClone = PhotonNetwork.Instantiate (enemy.name,pos,Quaternion.identity,0).gameObject;
				enemy_notClone.name = enemy.name + A_enemy_cnt.ToString();
				A_enemy_cnt++;

			}
		}

	}


	void Spawn_stage (){
		float x = 0;
		float z = 0;
		for (int i = A_enemy_i; i < A_enemy_max; i++) {
			GameObject obj = GameObject.Find ("Enemy" + i.ToString ());
			obj.SetActive (true);

			//do {
				x = Random.Range (-3f, 3f);
				z = Random.Range (-3f, 3f);
			//} while(x <= 120 && x >= -120 && z <= 120 && z >= -120);

			switch(j){
			case 1:
				obj.transform.position = new Vector3 (x, 3, z) + Spawner_A.position;
				break;
			case 2:
				obj.transform.position = new Vector3 (x, 3, z) + Spawner_B.position;
				break;
			case 3:
				obj.transform.position = new Vector3 (x, 3, z) + Spawner_C.position;
				break;
			case 4:
				obj.transform.position = new Vector3 (x, 3, z) + Spawner_D.position;
				break;
			}
			A_enemy_i++;

		}
	}



}
