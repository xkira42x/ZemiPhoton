using UnityEngine;

public class N6_SyncShot : Photon.MonoBehaviour {

	// 送受信する情報の伝達先
	S3_Shot S_Shot;

	//IK追記
	N15_SizeOf SO;

    /// 初期化
	void Awake(){
		SO = GameObject.Find ("PhotonManager").GetComponent<N15_SizeOf> ();

		S_Shot = GetComponent<S3_Shot> ();
	}

    /// 銃攻撃メッセージ
	void ToAttackMSG(){
		photonView.RPC ("SyncShotAction", PhotonTargets.Others);
	}

    /// 銃の取得メッセージ
	void PickUpItemMSG(GameObject obj){
		photonView.RPC ("SyncPickUpIten", PhotonTargets.Others,obj.name);
	}

    /// 銃攻撃の同期
	[PunRPC]
	void SyncShotAction(){
		S_Shot.ToAttackMSG ();
		SO.AddSize (3);
	}

    /// 銃の取得同期
	[PunRPC]
	void SyncPickUpIten(string name){
		S_Shot.PickUpItemMSG (Instantiate (Resources.Load (name, typeof(GameObject))as GameObject));
		SO.AddSize (3);
	}

	// 同期処理
	/*void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
        // クライアントが操作するキャラクターの時、射撃判定を送信する
        // そうでない時、射撃判定を受信する
		if (stream.isWriting) {
			// 送信
			//stream.SendNext(S_Shot.S_Shoot);
		}else{
			// 受信
			//S_Shot.S_Shoot = (bool)stream.ReceiveNext();
		}
	}*/
}