using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2_Status: Photon.MonoBehaviour {

	string userName = "Default";	// ユーザ名
	public string UserName{ get { return userName; } set { userName = value; } }

	PlayerStatusUI statusUI;		// ステータス表示UI

	float health = 100;				// ヒットポイント(0～100まで)
	public float Health { get { return health; }	set { health = value; } }

	/// 体力を引数分減らし、HPのUIゲージ更新と同期を行う
	public void Damage(float dmg){
		health -= dmg;
		statusUI.Health = health;
		photonView.RPC ("SyncHP", PhotonTargets.Others, health);
	}

	/// ステータス表示するオブジェクトを設定する
	void Awake(){
		// 操作しているキャラクタのステータスは必ず、右端のステータスに表示する。
		// それ以外は、右から順に設定する
		if (photonView.isMine) {
			statusUI = GameObject.Find ("PlayerStatusUI0").GetComponent<PlayerStatusUI> ();
			photonView.RPC ("SyncPlayerID", PhotonTargets.AllBuffered, PlayerInfo.playerNumber+1);
		}else {
			statusUI = GameObject.Find ("PlayerStatusUI" + (PlayerInfo.statusCount).ToString ()).GetComponent<PlayerStatusUI> ();
			PlayerInfo.statusCount++;
		}
	}

	/// ユーザ名の同期(初期呼び出し)
	[PunRPC]
	void SetName(string name) {
		statusUI.UserName = name;
	}

	/// ユーザIDからキャラクタの名前IDを設定する(初期呼び出し)
	[PunRPC]
	void SyncPlayerID(int id){
		string name = "Player" + id.ToString ();
		userName = name;
		gameObject.name = name;
		PlayerList.AddPlayerList (name);
	}

	/// ヒットポイントの同期して、HPのUIゲージも更新する
	[PunRPC]
	void SyncHP(short hp){
		statusUI.Health = health = hp;
	}
}
