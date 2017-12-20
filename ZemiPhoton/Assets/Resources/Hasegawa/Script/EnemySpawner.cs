using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemy;					// 敵オブジェクト
	[SerializeField]Transform[] SpawnPosition;	// 生成位置

	[SerializeField] int maxNum = 20;			// 最大生成数
	[SerializeField] int numGenerated = 0;		// 現在の敵数

	bool startflg=false;
		
	void Update(){
		//startflgがfalseの最初だけコルーチンを動かす
		if (PlayerInfo.Spawn && !startflg) {
			StartCoroutine ("GenerateIntervals");
			startflg = true;
		}
	}
	void Spawn () {
		int index = 0;
		Vector3 pos, target;
		float dist;

		target = PlayerList.GetPlayerPosition_Shuffle ();
		dist = Vector3.Distance (target, SpawnPosition [0].position);

		for(int ii = 1;ii < SpawnPosition.Length;ii++){
			float dd = Vector3.Distance (target, SpawnPosition [ii].position);
			if (dd < dist) {
				dist = dd;
				index = ii;
			}
		}
		
		for (int jj = numGenerated; jj < maxNum; jj++) {
			PhotonNetwork.Instantiate (enemy [0].name,
				new Vector3 (Random.Range (-3f, 3f), 3, Random.Range (-3f, 3f)) + SpawnPosition [index].position,
				Quaternion.identity, 0).gameObject.name = enemy [0].name + (jj + 1).ToString ();
		}
	}

	public IEnumerator GenerateIntervals(){
		while (true) {
			yield return new WaitForSeconds (10f);
			numGenerated = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (numGenerated < maxNum)
				Spawn ();
		}
	}
}
