using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemy;     //敵オブジェクト
	[SerializeField]Transform[] SpawnPosition;

	[SerializeField] int maxNum = 20;//最大湧数
	[SerializeField] int numGenerated = 0;

	bool startflg=false;
	void Start(){
//		StartCoroutine ("GenerateIntervals");
	}
		
	void Update(){
		//startflgがfalseの最初だけコルーチンを動かす
		if (PlayerInfo.Spawn && !startflg) {
			StartCoroutine ("GenerateIntervals");
			startflg = true;
		}
	}
	void Spawn () {
		if (PlayerInfo.Spawn) {
			int index = Random.Range (0, 4);
			for (int i = numGenerated; i < maxNum; i++) {
				PhotonNetwork.Instantiate (enemy [0].name,
					new Vector3 (Random.Range (-3f, 3f), 3, Random.Range (-3f, 3f)) + SpawnPosition [index].position,
					Quaternion.identity, 0).gameObject.name = enemy [0].name + (i + 1).ToString ();
			}
		}
	}

	public IEnumerator GenerateIntervals(){
		while (true) {
			yield return new WaitForSeconds (10f);
			numGenerated = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			Spawn ();
		}
	}
}
