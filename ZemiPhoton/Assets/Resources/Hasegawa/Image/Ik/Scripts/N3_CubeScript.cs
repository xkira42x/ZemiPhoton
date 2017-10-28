using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サイズオブ使用まとめ
//using System.Runtime.InteropServices;
//Marshal.SizeOf();
//URL->https://msdn.microsoft.com/ja-jp/library/dn261467(v=vs.110).aspx

//コンポーネントの追加だけでおｋ
public class N3_CubeScript : Photon.MonoBehaviour {

	public Vector3 N_hensu1=Vector3.zero;
	private Vector3 N_OutVec=Vector3.zero;

	public bool N_deltaSyncFlg;


	void Awake(){
		//初期生成時にも同期が起きてしまうため、前回の座標を生成時の座標へ
		N_OutVec = this.transform.position;

	}
	// Use this for initialization
	void Start () {

		//ネットワーク
		PhotonNetwork.NetworkStatisticsEnabled=true;

		//カメラ設定
		if (photonView.isMine) {
			this.transform.Find ("Camera").GetComponent<Camera> ().depth = 1;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			if (N_deltaSyncFlg == true) {
				//座標の差分値を送信
				stream.SendNext (this.transform.position - N_OutVec);

				//今回の送信で送った座標を更新
				N_OutVec = this.transform.position;
			} else {
				//座標を直接送信
				stream.SendNext (this.transform.position);

				//今回の送信で送った座標を更新
				N_OutVec = this.transform.position;
			}

		} else {
//			Debug.Log ("Serialize Juu"+Time.time);
			//データの受信
			//移動後の座標が送られてくる 例：(0.1,0,0.1)
			this.N_hensu1 = (Vector3)stream.ReceiveNext ();
		}
	}
}