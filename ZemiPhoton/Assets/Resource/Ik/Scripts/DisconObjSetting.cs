using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconObjSetting : MonoBehaviour {
	[SerializeField]
	float HP;
	public float health{ get { return HP; } set { HP = value;} }

	PlayerStatusUI StatusUI;
	public PlayerStatusUI statusUI{ get { return StatusUI; } set { StatusUI = value; } }


	[PunRPC]
	public void DisconName(string name){
		this.transform.name = name;
	}

	[PunRPC]
	public void DisconHP(float hp){
		health = hp;
	}
	[PunRPC]
	public void DisconStatusUI(PlayerStatusUI status){
		statusUI = status;
	}
}
