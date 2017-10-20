using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	[SerializeField]Camera myCamera;
	[SerializeField]GameObject Body;
	[SerializeField]S1_Move move;
	[SerializeField]S2_Angle angle;
	[SerializeField]GameObject Canvas;
	void Start () {
		if (photonView.isMine) {
			myCamera.gameObject.SetActive (true);
			Body.SetActive (false);
			move.enabled = true;
			angle.enabled = true;
			Canvas.SetActive (true);
		}
	}
}