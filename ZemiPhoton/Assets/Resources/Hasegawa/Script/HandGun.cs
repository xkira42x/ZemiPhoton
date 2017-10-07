using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : GunBase {

	public override void Action ()
	{
		if (Input.GetMouseButtonDown(0)) {
			base.Action ();
		}
	}


}
