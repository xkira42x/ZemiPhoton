using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemy;     //敵オブジェクト
	[SerializeField]Transform[] SpawnPosition;

	public int A_enemy_max = 100;//最大湧数
	public int now_enemy_count = 0;

	void Start(){
		StartCoroutine ("GenerateIntervals");
	}
		
	void Spawn () {
		if (PhotonManager.EnteringTheRoom) {
			int index = Random.Range (0, 4);
			for (int i = now_enemy_count; i < A_enemy_max; i++) {
				GameObject enemy_notClone = PhotonNetwork.Instantiate (enemy [0].name,
					                            new Vector3 (Random.Range (-3f, 3f), 3, Random.Range (-3f, 3f)) + SpawnPosition [index].position,
					                            Quaternion.identity, 0).gameObject;
				enemy_notClone.name = enemy [0].name + (i + 1).ToString ();
			}
		}
	}

	public IEnumerator GenerateIntervals(){
		while (true) {
			now_enemy_count = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			Spawn ();
			Debug.Log ("Call");
			yield return new WaitForSeconds (10f);
		}
	}
}
