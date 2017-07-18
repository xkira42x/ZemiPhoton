using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Verification;
public class a : MonoBehaviour {
	int aa = 231;
	float bb = 234;
	Vector3 cc = Vector3.up;
	// Use this for initialization
	void Start () {
		verifi.ShowResult (aa);
		verifi.ShowResult (bb);
		verifi.ShowResult (cc);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
