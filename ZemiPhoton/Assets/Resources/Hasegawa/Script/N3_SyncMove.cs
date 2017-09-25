using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
//using System;
//using System.Runtime.InteropServices;

public class N3_SyncMove : Photon.MonoBehaviour {

	//PhotonView N_photonView;
	Vector3 N_syncPos = Vector3.zero;
	//Vector3 N_nowPos = Vector3.zero;
	public Vector3 GetSyncPos(){return N_syncPos;}
	[SerializeField]S1_Move S_Move;

<<<<<<< HEAD
	public Vector3 pos;
	bool isJump = false;
	public bool IsJump{ get { return isJump; } set { isJump = value; } }

	//
	Vector3 N_nowPos;
	public Vector3 GetnowPos(){return N_nowPos;}

	[SerializeField]
	bool deltaflg;

	void Awake(){
		//初期生成時にも同期が起きてしまうため、前回の座標を生成時の座標へ
		N_nowPos = transform.position;
	}

	// Use this for initialization
	void Start () {
		//ネットワーク
		PhotonNetwork.NetworkStatisticsEnabled = true;
		//N_photonView = PhotonView.Get(this);
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			//座標の差分値を送信
			if (deltaflg == true) {
				stream.SendNext (transform.position - N_nowPos);
				N_nowPos = transform.position;
			} else {
				stream.SendNext (transform.position);
			}
			stream.SendNext(isJump);
		} else {
			//データの受信
			N_syncPos = (Vector3)stream.ReceiveNext ();
			if (deltaflg == true) {
				this.gameObject.GetComponent<S1_Move> ().SyncPosition ();
			}
			isJump = (bool)stream.ReceiveNext ();
		}
=======
	void Update(){
		if (photonView.isMine) {
			photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
		}
	}

	[PunRPC]
	void SyncPosition(Vector3 pos){
		transform.position = pos;
>>>>>>> origin/Hasegawa
	}
}
}