using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : Photon.MonoBehaviour {

	private PhotonView photonView;
	private PhotonTransformView photonTransformView;

	public Vector3 hensu1=new Vector3(0,0,0);
	private Vector3 OutVec;

	// Use this for initialization
	void Start () {
		PhotonNetwork.NetworkStatisticsEnabled=true;
		photonView = PhotonView.Get(this);
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			//座標の差分値を送信
			stream.SendNext (this.transform.position-OutVec);
			//今回の送信で送った座標を更新
			OutVec = this.transform.position;
		} else {
//			Debug.Log ("Serialize Juu"+Time.time);
			//データの受信
			this.hensu1 = (Vector3)stream.ReceiveNext ();
		}
	}
}