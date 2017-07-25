using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームを強制終了させるキーです
public class N3_EndKey : MonoBehaviour {

	public GameObject EndUI;

	GameObject gObj;
	// Update is called once per frame
	void Update () {
		//Escapeキーでゲーム終了ボタンを表示
		if (Input.GetKeyDown (KeyCode.Escape)) {
			gObj = Instantiate (EndUI);
			gObj.transform.name = EndUI.name;
			gObj.transform.parent=GameObject.Find ("Canvas").transform;
			ShowMouse (false);
		}
	}
	public void EndButton(){
		Application.Quit ();
	}
	public void ContnButton(){
		Destroy (GameObject.Find(EndUI.name));
		ShowMouse (true);
	}
	public void ShowMouse(bool flg){
		Screen.lockCursor = flg;
	}
}
