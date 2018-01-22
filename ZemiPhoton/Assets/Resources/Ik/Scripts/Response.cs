using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : MonoBehaviour {
	N15_SizeOf SO;
	PhotonView phview;
	void Start(){
		SO=GameObject.Find("PhotonManager").GetComponent<N15_SizeOf>();
		phview = GameObject.Find ("PhotonManager").GetPhotonView ();
	}
	/// <summary>
	/// 同期のレスポンスを行う
	/// </summary>
	/// <param name="line">タスク表の行番号.</param>
	/// <param name="column">タスク表の列番号.</param>
	[PunRPC]
	void Receive(byte line,byte column){
		string messeage = "Receive." + line+":" + column;

		SO.AddSize (10);
		SO.AddSize (3);
		Debug.Log(messeage);
	}
}
