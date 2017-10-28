using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N8_SyncMove : Photon.MonoBehaviour {

	//IK追記
	N15_SizeOf SO;
	void Awake(){SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();}

	void Start(){
		if (PhotonNetwork.isMasterClient)
		StartCoroutine ("SyncPos");
	}
	IEnumerator SyncPos(){
		while (true) {
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
			yield return new WaitForSeconds (0.25f);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;

		//IK追記
		SO.AddSize((int)pos.x);
		SO.AddSize((int)pos.y);
		SO.AddSize((int)pos.z);
		SO.AddSize(3);
	}
}