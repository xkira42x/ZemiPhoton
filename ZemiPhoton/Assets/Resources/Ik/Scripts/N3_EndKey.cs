using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ゲームを強制終了させるキーです
public class N3_EndKey : Photon.MonoBehaviour {

	GameObject EndUI;

//	GameObject LoginUI;

//	GameObject Camera;
	// Update is called once per frame
	void Start(){
		EndUI=GameObject.Find ("UI").transform.Find("EndUI").gameObject;
//		LoginUI= GameObject.Find ("UI").transform.Find("loginUI").gameObject;
	}
	void Update () {
		//Escapeキーでゲーム終了ボタンを表示
		if (Input.GetKeyDown (KeyCode.Escape)) {
			EndUI.SetActive (true);
			Cursor.lockState=CursorLockMode.None;	//ロックなし
			ShowMouse (true);
		}
	}
	public void EndButton(){
		//このオブジェクトが自分のオブジェクトなら
		if(photonView.isMine)
			photonView.RPC ("CubeInstant", PhotonTargets.MasterClient, this.transform.position);
		PhotonNetwork.Disconnect ();
		SceneManager.LoadScene ("title");
/*		GameObject.Find ("PlayerName").SetActive (false);
		Camera =GameObject.Find ("PhotonManager").transform.Find("Camera").gameObject;
		Camera.SetActive(true);
		LoginUI =GameObject.Find ("UI").transform.Find("loginUI").gameObject;
		LoginUI.SetActive(true);
		GameObject.Find ("UI").transform.Find("loginUI").transform.Find("PlayerName").transform.Find("TextP").GetComponent<Text>().text = "bbb";

		EndUI = GameObject.Find ("EndUI");
		EndUI.SetActive (false);
		PhotonNetwork.LeaveRoom ();
		Destroy(this.gameObject);
*/
	}
	[PunRPC]
	void CubeInstant(Vector3 pos){
		Debug.Log ("CubeInstant");
		//退出キューブを生成
		PhotonNetwork.Instantiate ("DisconCube", pos,new Quaternion(0,0,0,0),1);
	}
	public void ContnButton(){
		EndUI = GameObject.Find ("EndUI");
		Cursor.lockState=CursorLockMode.Confined;	//画面内にロック
		ShowMouse (false);
		EndUI.SetActive (false);
	}
	public void ShowMouse(bool flg){
//		Screen.lockCursor = flg;
//		Cursor.lockState = flg;
		Cursor.visible=flg;
	}
}
