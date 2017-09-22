using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

	[SerializeField]protected GameObject AmmoObj;
	protected int MaxAmmo;
	protected int Ammo;
	protected bool Next = true;
	[SerializeField]public Transform Muzzle;
	[SerializeField]public Transform Collection;
	[SerializeField]public ParticleSystem[] MuzzleFlash;

	// ショット
	public virtual void Action(){
		if (Next) {
			Instantiate (AmmoObj, Muzzle.position, transform.localRotation * Collection.localRotation);
			for (int i = 0; i < MuzzleFlash.Length; i++)
				MuzzleFlash [i].Play ();
			Next = false;
			Delay (.1f);
		}
	}

	public void Delay(float interval){
		StartCoroutine ("Del", interval);
	}

	IEnumerator Del(float il){
		yield return new WaitForSeconds (il);
		Next = true;
	}

	public void ShotSetting(S3_Shot S_Shot){
		S_Shot.Shot = Action;
		Collection = S_Shot.S_Collection;
	}

}
