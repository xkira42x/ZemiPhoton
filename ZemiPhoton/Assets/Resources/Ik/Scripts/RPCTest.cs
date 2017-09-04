using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Verification;

public class RPCTest : Photon.MonoBehaviour {

	/// <summary>
	/// The photon view.
	/// </summary>
	[SerializeField]
	GameObject obj;

	[PunRPC]
	void Hoge(){
		Instantiate (obj);
	}

	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				photonView.RPC ("Hoge", PhotonTargets.All);
			}
		}
	}
}
