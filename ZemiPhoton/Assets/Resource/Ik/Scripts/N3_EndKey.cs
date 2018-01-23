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

	GameObject MyGameObject;
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
		LeaveRoom ();
		//ボタンの参照先がResourceのオブジェクトなので、シーン内の自分を探している
/*		PhotonView phview;
		//プレイヤーオブジェクト群から、自分を探す
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			phview=GameObject.FindGameObjectsWithTag ("Player")[i].GetComponent<PhotonView>();
			//このオブジェクトが自分のオブジェクトなら、その座標にオブジェクトを生成
			if (phview.isMine) {
				MyGameObject = GameObject.FindGameObjectsWithTag ("Player") [i];
				CubeInstant (MyGameObject.transform.position);
			}
		}

		Invoke ("MineDestroy", 0.5f);
*/	}
	void MineDestroy(){
		PhotonNetwork.Destroy (MyGameObject);
		Invoke ("LeaveRoom",0.5f);
	}
	void LeaveRoom(){
		PhotonNetwork.Disconnect ();
		SceneManager.LoadScene ("title");
	}
	//退出キューブを生成
	void CubeInstant(Vector3 pos){
		Debug.Log ("CubeInstant");
		Debug.Log(pos);
		PhotonNetwork.Instantiate ("DisconCube", pos,new Quaternion(0,0,0,0),0);
	}
	public void ContnButton(){
		EndUI = GameObject.Find ("EndUI");
		Cursor.lockState=CursorLockMode.Confined;	//画面内にロック
		ShowMouse (false);
		EndUI.SetActive (false);
	}
	public void ShowMouse(bool flg){
		Cursor.visible=flg;
	}
}
