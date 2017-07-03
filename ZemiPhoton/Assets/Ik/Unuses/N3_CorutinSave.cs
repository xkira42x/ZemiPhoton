using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N3_CorutinSave : Photon.MonoBehaviour {

	N3_PlayerVecSave N_PVS;
	void Start(){
		N_PVS = this.GetComponent<N3_PlayerVecSave> ();
		StartCoroutine ("OneTimeSave");
	}

	IEnumerator OneTimeSave(){
		while (true) {
//			Debug.Log ("本更新");
			//現在の本座標を更新
//			PVS.PlayerVec=this.transform.position;

			//１秒語とに更新
			yield return new WaitForSeconds (5.0f);
		}
	}
}