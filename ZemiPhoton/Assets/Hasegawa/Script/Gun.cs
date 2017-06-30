using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	// 種類
	byte type = 0;
	// なし、ハンドガン、サブマシンガン、アサルトライフル、ショットガン、スナイパーライフル
	const byte NONE = 0,HG = 1,SMG = 2,AR = 3,SG = 4,SP = 5;
	// 撃つ
	bool shot = false;
	bool oneShot = false;

	[SerializeField]
	GameObject myBullet;
	[SerializeField]
	GameObject muzzle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (0))
			shot = true;
		else {
			shot = false;
			if(type == HG)
			oneShot = false;
		}

		if (shot) Shot ();
	}

	void Shot(){
		switch (type) {
		case NONE:
			break;
		case HG:
			if (!oneShot) {
				Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
				oneShot = true;
			}
			break;
		case SMG:
			Instantiate (myBullet, muzzle.transform.position, transform.localRotation);			break;
		case AR:
			Instantiate (myBullet, muzzle.transform.position, transform.localRotation);			break;
		case SG:
			Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
			Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
			Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
			break;
		case SP:
			if (!oneShot) {
				StartCoroutine (DelayShot ());
			}
			break;
		default:
			break;
		}
	}

	IEnumerator DelayShot(){
		Instantiate (myBullet, muzzle.transform.position, transform.localRotation);
		yield return new WaitForSeconds (1);
		oneShot = false;
	}
		
}
