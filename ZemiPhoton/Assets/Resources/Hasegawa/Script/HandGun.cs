using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : GunBase {

	void Start(){
		base.Start ();
	}

	public override void Action (){
		if (Input.GetMouseButtonDown (0)) {
			base.Action ();
		}
	}


}
