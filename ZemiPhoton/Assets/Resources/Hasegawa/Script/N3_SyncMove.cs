using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N3_SyncMove : Photon.MonoBehaviour {

	[SerializeField]S1_Move S_Move;

	//IK追記
	N15_SizeOf SO;
	void Awake(){SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();}

	void Update(){
		if (photonView.isMine) {
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;

		//IK追記
		SO.SizeLog ((int)pos.x);
		SO.SizeLog ((int)pos.y);
		SO.SizeLog ((int)pos.z);
		SO.SizeLog (3);
	}
}