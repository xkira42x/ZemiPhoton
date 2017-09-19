using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGun : MonoBehaviour {

	[SerializeField]GameObject bullet;
	[SerializeField]testShot tShot;

	// Use this for initialization
	void Start () {
		tShot.action = SpawnBullet;
	}

	void SpawnBullet(){
		Instantiate (bullet, transform.position, Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
