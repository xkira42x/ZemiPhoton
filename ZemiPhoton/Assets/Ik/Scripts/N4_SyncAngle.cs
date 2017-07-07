using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//コンポーネントの追加だけでおｋ
public class N4_SyncAngle : Photon.MonoBehaviour {

	private PhotonView N_photonView;

	private Quaternion N4_PlayerAngle=new Quaternion(0,0,0,0);

	[SerializeField]
	Transform N4_Camera;

	void Uppdate(){
		this.transform.rotation = N4_PlayerAngle;

		transform.localRotation = new Quaternion (0, N4_PlayerAngle.y, 0, N4_PlayerAngle.w);
		N4_Camera.localRotation = new Quaternion (N4_PlayerAngle.x, N4_Camera.localRotation.y, N4_Camera.localRotation.z, N4_Camera.localRotation.w);
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			//座標の差分値を送信
			stream.SendNext (this.GetComponent<S2_Angle>().S_GetMainAngle());

		} else {
			//データの受信
			//移動後の座標が送られてくる 例：(0.1,0,0.1)
			this.N4_PlayerAngle = (Quaternion)stream.ReceiveNext ();
		}
	}
}