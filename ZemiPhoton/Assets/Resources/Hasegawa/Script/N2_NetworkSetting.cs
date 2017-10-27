using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	[SerializeField]GameObject camera;
	[SerializeField]GameObject body;
	[SerializeField]GameObject canvas;

	void Awake () {
		if (photonView.isMine) {
			camera.SetActive (true);
			canvas.SetActive (true);
			GetComponent<Rigidbody> ().useGravity = true;
			Destroy (body);
		} else {
			Destroy (GetComponent<S1_Move> ());
			Destroy (GetComponent<S2_Angle> ());
			//Destroy (camera);
			Destroy (canvas);
		}

		Destroy (this);
	}
}