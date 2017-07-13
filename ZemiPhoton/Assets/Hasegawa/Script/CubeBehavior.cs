using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Verification;

public class CubeBehavior : MonoBehaviour {
	[DllImport("sample-dll")]private static extern int CountUp ();
	[SerializeField]
	int LoadValue = 10;
	int value = 10;
	int I = 10;
	float F = 10;
	double D = 10;
	Vector3 V3 = new Vector3(123,123,123);

	// Use this for initialization
	void Start () {
		hello.HellWorld ();
		//Debug.Log (Convert.ToString(value,2).PadLeft(8,'0'));
		//Debug.Log (Convert.ToString ((int)Fvalue, 2).PadLeft (8));
		verifi.ShowResult(I);
		verifi.ShowResult(F);
		verifi.ShowResult(D);
		verifi.ShowResult (V3.x);
		verifi.ShowResult(V3);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
