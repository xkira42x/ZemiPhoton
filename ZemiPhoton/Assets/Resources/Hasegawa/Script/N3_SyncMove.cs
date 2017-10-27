using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using System.Runtime.InteropServices;

public class N3_SyncMove : Photon.MonoBehaviour {

	S1_Move S_Move;

	//IK追記
	N15_SizeOf SO;

	Vector3 lastPosition;
	Vector3 syncPosition;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();

		S_Move = GetComponent<S1_Move> ();
		lastPosition = syncPosition = transform.position;
	}

	void Update(){
		if (photonView.isMine) {
			if (Vector3.Distance (transform.position, lastPosition) > .1f) {
				photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
				lastPosition = transform.position;
			}
		} else {
			transform.position = Vector3.Lerp (transform.position, syncPosition, .05f);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		syncPosition = pos;

		//IK追記

		SO.AddSize ((int)pos.x);
		SO.AddSize ((int)pos.y);
		SO.AddSize ((int)pos.z);
		SO.AddSize (3);
	}
}