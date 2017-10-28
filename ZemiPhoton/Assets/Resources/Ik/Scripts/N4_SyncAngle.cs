using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//コンポーネントの追加だけでおｋ
public class N4_SyncAngle : Photon.MonoBehaviour {

	private PhotonView N_photonView;
	// 更新する角度の保存
	private Quaternion N_MainAngle;
	private Quaternion N_CameraAngle;

	[SerializeField]
	Transform N_Collection;
	// 角度制御のクラス取得
	S2_Angle S_Angle;

	void Start(){
		S_Angle = GetComponent<S2_Angle> ();
		N_CameraAngle = Quaternion.identity;
		N_MainAngle = Quaternion.identity;
	}

	void Update(){
		if (!photonView.isMine) {
			// 角度の更新
			transform.localRotation = N_MainAngle;
			N_Collection.localRotation = N_CameraAngle;
		}

	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			//座標の差分値を送信
			stream.SendNext (S_Angle.S_mainAngle);
			stream.SendNext (S_Angle.S_cameraAngle);

		} else {
			//データの受信
			//移動後の座標が送られてくる 例：(0.1,0,0.1)
			N_MainAngle = (Quaternion)stream.ReceiveNext ();
			N_CameraAngle = (Quaternion)stream.ReceiveNext ();
		}
	}
}