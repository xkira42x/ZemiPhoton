using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N3_SyncMove : Photon.MonoBehaviour {

	//IK追記
	N15_SizeOf SO;

    // 最後に同期した座標
	Vector3 lastPosition;
    // 同期した値を格納
	Vector3 syncPosition;

	void Awake(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();
        // 変数の初期化
		lastPosition = syncPosition = transform.position;
	}

	void Update(){

		if (photonView.isMine) {
            // 最後の座標より.1f以上移動がされた時
			if (Vector3.Distance (transform.position, lastPosition) > .1f) {
                // 座標の送信
				photonView.RPC ("SyncPosition", PhotonTargets.Others, transform.position);
				//IK追記 送信量を取得
				SO.AddSize ((int)transform.position.x);
				SO.AddSize ((int)transform.position.y);
				SO.AddSize ((int)transform.position.z);
				SO.AddSize (3);
                // 最後に同期した座標の更新
				lastPosition = transform.position;
			}
		} else {
            // 同期受信した座標と、現在の座標を補間する　Vector3.Lerp(現在の座標,同期受信した座標,どっちに寄せて補間をするかの値)
			transform.position = Vector3.Lerp (transform.position, syncPosition, .05f);
		}
	}

    /// <summary>
    /// 座標の同期（受信）
    /// </summary>
    /// <param name="pos"></param>
	[PunRPC]
	void SyncPosition(Vector3 pos){
        // 同期受信した座標の更新
		syncPosition = pos;

		//IK追記　受信量を取得
		SO.AddSize ((int)pos.x);
		SO.AddSize ((int)pos.y);
		SO.AddSize ((int)pos.z);
		SO.AddSize (3);
	}
}