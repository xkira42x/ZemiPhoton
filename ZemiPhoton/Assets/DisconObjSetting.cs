using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconObjSetting : MonoBehaviour {

	[PunRPC]
	public void DisconName(string name){
		this.transform.name = name;
	}

}
