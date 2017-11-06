using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bpstestobjscript : Photon.MonoBehaviour {

	enum udflg{
		STOP,
		FORWORD,
		BACKWORD
	}
	[SerializeField]
	float movevec=1;	//移動量を格納

	Vector3 initpos;	//初期座標を格納
	int moveflg;		//移動状態を格納

	string myname;
	public string name{set{myname = value;}}

	//初期座標を取得
	void Start(){
		initpos = this.transform.position;
		if(PhotonNetwork.isMasterClient)
		photonView.RPC ("TestSyncName", PhotonTargets.AllBuffered, myname);
	}

	void Update(){
		FlgCheng ();
		if(PhotonNetwork.isMasterClient)
		Move ();
	}

	//キー入力を受けフラグを変える
	void FlgCheng(){
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			moveflg = (int)udflg.FORWORD;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			moveflg = (int)udflg.BACKWORD;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			moveflg = (int)udflg.STOP;
		}

	}

	//状態に応じて移動する
	void Move(){
		switch (moveflg) {
		case (int)udflg.STOP:
			photonView.RPC ("TestTransSync", PhotonTargets.All, initpos-this.transform.position);
			break;
		case (int)udflg.FORWORD:
			photonView.RPC ("TestTransSync", PhotonTargets.All, new Vector3 (0, 0, -movevec));
			break;
		case (int)udflg.BACKWORD:
			photonView.RPC ("TestTransSync", PhotonTargets.All, new Vector3 (0, 0, movevec));
			break;
		}
	}

	//引数(tt)の座標に同期
	[PunRPC]
	void TestTransSync(Vector3 tt){
		this.transform.position += tt;
	}

	[PunRPC]
	void TestSyncName(string ss){
		this.transform.name = ss;
	}
}
