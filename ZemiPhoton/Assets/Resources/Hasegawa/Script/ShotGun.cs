using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase {
	float angle = 2;
	public override void Action (){
		if (Next && Magazine > 0) {
			Magazine--;
			for (int i = 0; i < 10; i++)
				Instantiate (AmmoObj, CameraT.position, Quaternion.Euler (Vec3Rand)*CameraT.rotation);
			PlayEffect ();
			Next = false;
			Delay (.5f);
		}
	}
	Vector3 Vec3Rand { get { return new Vector3 (Random.Range (-angle, angle), Random.Range (-angle, angle), Random.Range (-angle, angle)); } }
}