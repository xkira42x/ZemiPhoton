using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconObjSetting : MonoBehaviour {
	[SerializeField]
	float helth;
	public float Helth{ get { return helth; } set { helth = value;} }

	[PunRPC]
	public void DisconName(string name){
		this.transform.name = name;
	}

	[PunRPC]
	public void DisconHP(float HP){
		Helth = HP;
	}
}
