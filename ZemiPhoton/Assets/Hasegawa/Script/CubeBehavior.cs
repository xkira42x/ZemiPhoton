using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CubeBehavior : MonoBehaviour {
	[DllImport("sample-dll")]private static extern int CountUp ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (CountUp ());
	}
}
