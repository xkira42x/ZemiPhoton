using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	[SerializeField]
	Camera camera;

	// Use this for initialization
	void Start () {
		if (photonView.isMine) {
			camera.gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
