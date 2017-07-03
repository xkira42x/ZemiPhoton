using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_Shot : MonoBehaviour {
	[SerializeField]
	GameObject S_Bullet;
	[SerializeField]
	Transform S_Muzzle;
	[SerializeField]
	Transform S_Collection;
	
	// Update is called once per frame
	void Update () {
		// ショット
		if (Input.GetMouseButton (0)) {
			S_Shot ();
		}
	}
	// 撃つ
	void S_Shot(){
		Instantiate (S_Bullet, S_Muzzle.position, transform.localRotation * S_Collection.localRotation);
	}
}
