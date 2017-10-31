using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase {
	[SerializeField]float colllectingRate = 4;

	public override void Awake (){
		base.Awake ();
	}

	public override void Action (){
		if (Next && Magazine > 0) {
			Magazine--;
			for (int i = 0; i < 10; i++)
				Instantiate (AmmoObj, CameraT.position, Quaternion.Euler (Vec3Rand)*CameraT.rotation);
			PlayEffect ();
			Next = false;
			Delay (.5f);
		}else
			gameObject.SendMessageUpwards ("OutOfAmmoMSG", SendMessageOptions.DontRequireReceiver);
	}
	Vector3 Vec3Rand { get { return new Vector3 (Random.Range (-colllectingRate, colllectingRate), Random.Range (-colllectingRate, colllectingRate), Random.Range (-colllectingRate, colllectingRate)); } }
}