using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySqwan : Photon.MonoBehaviour {


	private object[] args;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			PhotonNetwork.InstantiateSceneObject ("Enemy", new Vector3 (3.0f, 3.0f, 3.0f), Quaternion.identity, 0, args);
		}

	}
}
