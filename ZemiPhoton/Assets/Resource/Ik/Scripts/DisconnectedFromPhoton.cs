using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Photonサーバーから切断した際に呼び出されます
public class DisconnectedFromPhoton : Photon.MonoBehaviour {
	[SerializeField]
	bool Disconflg=true;	//途中退室の処理の有り無し

	GameObject Disconcube;
	PhotonView DisconView;

	//自分が切断した時
	void OnDisconnectedFromPhoton(){
		Debug.Log ("DisconeFromPhoton");
		Debug.Log (this.gameObject.name+"の通信が切断された");
	}

	//ルーム内のだれかが切断した時
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer){
		if (Disconflg) {		
			if (PhotonNetwork.player.IsMasterClient) {
				Debug.Log ("PlayerDisconnected");
				Debug.Log (this.gameObject.name + "の通信が切断された");
				Debug.Log ("otherID:" + otherPlayer.ID);						//切断したプレイヤーのID
				Debug.Log ("playerID:" + PhotonNetwork.player.ID);				//自分のプレイヤーID
				Debug.Log ("ThisViewID:" + 
					this.gameObject.GetComponent<PhotonView> ().viewID / 1000);	//このオブジェクトのID
				if (otherPlayer.ID == this.gameObject.GetComponent<PhotonView> ().viewID / 1000) {
					CubeInstant (this.transform.position);						//待機キューブの生成
					DestroyPlayerObj ();										//このオブジェクトを消す
				}
			}
		}
	}

	//退出キューブを生成
	void CubeInstant(Vector3 pos){
		Debug.Log ("CubeInstant()");
		Disconcube = PhotonNetwork.Instantiate ("DisconCube", pos,new Quaternion(0,0,0,0),0).gameObject;
		DisconView = Disconcube.GetPhotonView ();
		DisconView.RPC ("DisconName", PhotonTargets.AllBuffered, this.gameObject.GetComponent<S2_Status> ().UserName);

		GetPlayerState ();
	}

	//オリジナルオブジェクトを消す
	void DestroyPlayerObj(){
		PhotonNetwork.Destroy (this.gameObject);
	}

	//退出キューブにプレイヤーステータスを
	void GetPlayerState(){
		Debug.Log("GetPlayerState()");

		//HPの同期
		DisconView.RPC("DisconHP",PhotonTargets.AllBuffered,this.gameObject.GetComponent<S2_Status> ().Health);
		//ステータスUIの同期
//		DisconView.RPC("DisconStatusUI",PhotonTargets.AllBuffered, this.gameObject.GetComponent<S2_Status> ().StatusUI);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F10)) {
			if(photonView.isMine)
			KeyDisconnect ();
		}
	}
	//強制切断
	void KeyDisconnect(){
		Debug.Log ("DisConnect");
		PhotonNetwork.Disconnect();

	}
}
