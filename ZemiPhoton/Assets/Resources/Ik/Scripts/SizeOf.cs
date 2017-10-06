using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Verification;

public class SizeOf : MonoBehaviour {
	[DllImport("sample-dll")]private static extern int CountUp ();
/*	int I = 1;
	float F = 1;
	double D = 1;
	Vector3 V3 = new Vector3(10,10,10);
*/
	// Use this for initialization
	void Start () {
/*		hello.HellWorld ();
		verifi.ShowResult(I);
		verifi.ShowResult(F);
		verifi.ShowResult(D);
		verifi.ShowResult (V3.x);
		verifi.ShowResult(V3);
*/	}
	
	// Update is called once per frame
	void Update () {
	}
	public void SizeLog(int hh){
		verifi.ShowResult(hh);
	}
	public void SizeLog(float hh){
		verifi.ShowResult(hh);
	}
	public void SizeLog(double hh){
		verifi.ShowResult(hh);
	}
	public void SizeLog(Vector3 hh){
		verifi.ShowResult(hh);
	}
}
