using UnityEngine;

public class HandGun : GunBase {

	/// 初期化
	public override void Awake (){
		base.Awake ();
	}

	/// 銃を撃つ
	public override void Action (){
		if (Input.GetMouseButtonDown (0)) {
			base.Action ();
		}
	}
}
