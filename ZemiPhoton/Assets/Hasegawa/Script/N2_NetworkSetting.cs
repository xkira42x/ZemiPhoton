using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	[SerializeField]
	Camera myCamera;

	// Use this for initialization
	void Start () {
		if (photonView.isMine) {
			myCamera.gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
