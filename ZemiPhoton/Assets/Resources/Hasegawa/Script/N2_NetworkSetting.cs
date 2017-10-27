using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	[SerializeField]GameObject myCamera;
	[SerializeField]GameObject myCanvas;
	[SerializeField]GameObject body;

	void Awake () {
		if (photonView.isMine) {
			myCamera.SetActive (true);
			myCanvas.SetActive (true);
			GetComponent<Rigidbody> ().useGravity = true;
			Destroy (body);
		} else {
			Destroy (GetComponent<S1_Move> ());
			Destroy (GetComponent<S2_Angle> ());
			Destroy (myCanvas);
		}

		Destroy (this);
	}
}