using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームを強制終了させるキーです
public class N3_EndKey : Photon.MonoBehaviour {

	GameObject EndUI;

	GameObject LoginUI;
	// Update is called once per frame
	void Start(){
		EndUI=GameObject.Find ("Canvas").transform.Find("EndUI").gameObject;
		LoginUI= GameObject.Find ("UI").transform.Find("loginUI").gameObject;
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
		LoginUI =GameObject.Find ("UI").transform.Find("loginUI").gameObject;
		LoginUI.SetActive(true);
		PhotonNetwork.LeaveRoom ();
	}
	public void ContnButton(){
		EndUI = GameObject.Find ("EndUI");
		EndUI.SetActive (false);
		Cursor.lockState=CursorLockMode.Confined;	//画面内にロック
		ShowMouse (false);
	}
	public void ShowMouse(bool flg){
//		Screen.lockCursor = flg;
//		Cursor.lockState = flg;
		Cursor.visible=flg;
	}
}
