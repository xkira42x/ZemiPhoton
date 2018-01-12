using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Photonサーバーから切断した際に呼び出されます
public class DisconnectedFromPhoton : MonoBehaviour {
	void OnDisconnectedFromPhoton(){
		Debug.Log (this.gameObject.name+"の通信が切断された");
	}
}
