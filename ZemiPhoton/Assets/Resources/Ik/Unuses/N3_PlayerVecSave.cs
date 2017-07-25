using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N3_PlayerVecSave : Photon.MonoBehaviour {

	private PhotonView N_photonView;

	public Vector3 N_hensu2=new Vector3(0,0,0);

	public Vector3 N_PlayerVec = Vector3.zero;

	// Use this for initialization
	void Start () {
		N_PlayerVec = this.transform.position;
		PhotonNetwork.NetworkStatisticsEnabled=true;
		N_photonView = PhotonView.Get(this);
	}



	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			//このオブジェクトの本座標を送信
//			stream.SendNext (PlayerVec);

		} else {
			//本座標の受信
//			this.hensu2 = (Vector3)stream.ReceiveNext ();
//			Debug.Log (Time.time);
		}
	}

}