using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconObjSetting : Photon.MonoBehaviour {
	[SerializeField]
	float HP;
	public float health{ get { return HP; } set { HP = value;} }

	[SerializeField]
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
	public void DisconStatusUI(){
		StartCoroutine ("InvDisconStateUI");
	}

	IEnumerator InvDisconStateUI(){
		while (true) {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			for (int i = 0; i < players.Length; i++) {
				
//				Debug.Log (players [i].GetComponent<S2_Status> ().UserName + ":" + this.transform.name);
				//退出キューブとPLyaerオブジェクトの名前が一致していたら
				if (players [i].GetComponent<S2_Status> ().UserName == this.transform.name) {

					//StatusUIを取得し保存
					statusUI = players [i].GetComponent<S2_Status> ().StatusUI;
//					Debug.Log ("PlayerStatusUI" + players [i].GetComponent<S2_Status> ().StatusUI);
					yield break;

				}
			}
			yield return new WaitForSeconds (0.2f);
		}
	}
}
