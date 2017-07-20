using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ｙ座標を固定させてます
public class N3_Yfreez : MonoBehaviour {

	Vector3 FreezePos=Vector3.zero;
	// Use this for initialization
	void Start () {
		FreezePos.y = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x,
			FreezePos.y,
			this.transform.position.z);
	}
}
