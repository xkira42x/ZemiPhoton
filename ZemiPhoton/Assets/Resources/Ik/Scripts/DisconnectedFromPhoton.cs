using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Photonサーバーから切断した際に呼び出されます
public class DisconnectedFromPhoton : Photon.MonoBehaviour {
	//自分が切断した時
	void OnDisconnectedFromPhoton(){
		Debug.Log ("DisconeFromPhoton");
		Debug.Log (this.gameObject.name+"の通信が切断された");

	}
	//ルーム内のだれかが切断した時
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer){
		Debug.Log ("PlayerDisconnected");
		Debug.Log (this.gameObject.name+"の通信が切断された");
		Debug.Log ("otherID:" + otherPlayer.ID);
		Debug.Log ("playerID:" + PhotonNetwork.player.ID);
		}
	[PunRPC]
	void CubeInstant(Vector3 pos){
		Debug.Log ("CubeInstant");
		//退出キューブを生成
		PhotonNetwork.Instantiate ("DisconCube", pos,new Quaternion(0,0,0,0),1);
	}
}
