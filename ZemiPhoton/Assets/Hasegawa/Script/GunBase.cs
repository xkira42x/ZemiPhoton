using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {
	protected byte S_type=0;
	protected int S_MaxAmmo;
	protected int S_Ammo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ショット
	protected virtual void S_Shot(){
	}

	protected byte S_GetType(){return S_type;}
}
