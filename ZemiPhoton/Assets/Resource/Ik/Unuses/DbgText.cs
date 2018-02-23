using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DbgText : Photon.MonoBehaviour {
	[SerializeField]
	GameObject TextBox;
	Text settext;

	// Use this for initialization
	void Start () {
		settext = TextBox.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		settext.text = "PhotonConnectState:"+PhotonNetwork.connectionState.ToString();
	}
}
