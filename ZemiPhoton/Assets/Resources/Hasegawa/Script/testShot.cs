using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class testShot : MonoBehaviour {
	public Action action;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)&&action!=null) {
			action ();
		}
	}
}
