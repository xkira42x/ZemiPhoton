using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N3_SyncMove : Photon.MonoBehaviour {

	[SerializeField]S1_Move S_Move;

	void Update(){
		if (photonView.isMine) {
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;
	}
}