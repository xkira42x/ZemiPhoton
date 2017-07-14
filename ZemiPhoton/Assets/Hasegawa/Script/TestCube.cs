using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {

	[SerializeField]
	Transform trans;
	[SerializeField]
	float f = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = trans.position + Vector3.down * f;
	}
}
