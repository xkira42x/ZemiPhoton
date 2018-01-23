using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Demo : MonoBehaviour {

	DateTime dTime = DateTime.Now;
	long dt;

	// Use this for initialization
	void Start () {
		dt = dTime.ToBinary ();
		Debug.Log ("DateTime : " + dTime.ToString () + "  DateTime(int) : " + dt.ToString ());		
	}
	
	// Update is called once per frame
	void Update () {
		LogText.UpdateLog (DateTime.Now.ToString ());
	}
}
