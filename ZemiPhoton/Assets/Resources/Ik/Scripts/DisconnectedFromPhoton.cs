using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Photonサーバーから切断した際に呼び出されます
public class DisconnectedFromPhoton : Photon.MonoBehaviour {
	string rmName;
	MenuManager MM;		//メニューマネージャ
	[SerializeField]
	bool Disconflg=true;	//途中退室の処理の有り無し

	//自分が切断した時
	void OnDisconnectedFromPhoton(){
		Debug.Log ("DisconeFromPhoton");
		Debug.Log (this.gameObject.name+"の通信が切断された");
		MM = GameObject.Find("PhotonManager").GetComponent<MenuManager> ();
	}
	//ルーム内のだれかが切断した時
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer){
		if (Disconflg) {		
			if (PhotonNetwork.player.IsMasterClient) {
				Debug.Log ("PlayerDisconnected");
				Debug.Log (this.gameObject.name + "の通信が切断された");
				Debug.Log ("otherID:" + otherPlayer.ID);
				Debug.Log ("playerID:" + PhotonNetwork.player.ID);
				Debug.Log ("ThisID:" + this.gameObject.GetComponent<PhotonView> ().viewID / 1000);
				if (otherPlayer.ID == this.gameObject.GetComponent<PhotonView> ().viewID / 1000) {
					CubeInstant (this.transform.position);
				}
			}
		}
	}
	//退出キューブを生成
	void CubeInstant(Vector3 pos){
		Debug.Log ("CubeInstant");
		GameObject Disconcube = PhotonNetwork.Instantiate ("DisconCube", pos,new Quaternion(0,0,0,0),0).gameObject;
	
		Disconcube.GetComponent<PhotonView>().RPC ("DisconName",PhotonTargets.AllBuffered, this.gameObject.GetComponent<S2_Status> ().UserName);
//		Invoke ("Reconnect", 2f);
		//オリジナルオブジェクトを消す
		PhotonNetwork.Destroy (this.gameObject);

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
		rmName = PhotonNetwork.room.Name;
		Debug.Log ("RoomName:"+rmName);
		PhotonNetwork.Disconnect();
		Invoke("ConnectPhoton",2f);
	}
	void ConnectPhoton(){
		//　ゲームのバージョン設定
//		PhotonNetwork.ConnectUsingSettings ("v0.1");
		Debug.Log ("Connect");
		Invoke ("Reconnect", 2f);
	}
	void Reconnect(){
		PhotonNetwork.Reconnect();
		Debug.Log ("Reconnect");
		Invoke ("RejoinRoom", 3f);
	}
	void LeaveRoom(){
		PhotonNetwork.LeaveRoom ();
		Debug.Log ("LeaveRoom");
		Invoke ("Rejoin", 3f);
	}
	void RejoinRoom(){
		Debug.Log ("RejoinRoom");
		if (PhotonNetwork.JoinRoom (rmName)) {
			Debug.Log ("再入室成功");
//			PhotonNetwork.Instantiate
		} else {
			Debug.Log ("再入室失敗");
		}
	}

}
