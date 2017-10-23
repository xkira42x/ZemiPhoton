using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N3_SyncMove : Photon.MonoBehaviour {

	S1_Move S_Move;

	//IK追記
	N15_SizeOf SO;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();

		S_Move = GetComponent<S1_Move> ();
	}

	void Update(){
		if (photonView.isMine) {
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;

		//IK追記

		SO.AddSize ((int)pos.x);
		SO.AddSize ((int)pos.y);
		SO.AddSize ((int)pos.z);
		SO.AddSize (3);
	}
}