using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Photon.MonoBehaviour {

	private Vector3 hensu2=new Vector3(0,0,0);

	CubeScript CS;
	// Use this for initialization
	void Start () {
		StartCoroutine ("move");
		CS = this.GetComponent<CubeScript> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (0.1f, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position -= new Vector3 (0.1f, 0, 0);
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.position += new Vector3 (0, 0, 0.1f);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= new Vector3 (0, 0, 0.1f);
		}
		//同期された値を拾う
		hensu2 = CS.hensu1;

	}
	IEnumerator move(){
		while(true){
			this.transform.position = new Vector3 (this.transform.position.x+hensu2.x, this.transform.position.y, this.transform.position.z+hensu2.z);
			yield return new WaitForSeconds(0.1f);
		}		
	}
}