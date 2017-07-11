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
	int I = 125;
	float F = 125;
	double D = 125;

	// Use this for initialization
	void Start () {
		hello.HellWorld ();
		//Debug.Log (Convert.ToString(value,2).PadLeft(8,'0'));
		//Debug.Log (Convert.ToString ((int)Fvalue, 2).PadLeft (8));
		Debug.Log ("int    : " + verifi.BitCalculation(I) + "bit   0x" + verifi.BitConversion(I));
		Debug.Log ("float  : " + verifi.BitCalculation(F) + "bit   0x" + verifi.BitConversion(F));
		Debug.Log ("double : " + verifi.BitCalculation(D) + "bit   0x" + verifi.BitConversion(D));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
