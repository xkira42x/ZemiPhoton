using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : MonoBehaviour {
	/// <summary>
	/// 同期のレスポンスを行う
	/// </summary>
	/// <param name="line">タスク表の行番号.</param>
	/// <param name="column">タスク表の列番号.</param>
	[PunRPC]
	void Receive(byte line,byte column){
		string messeage = "Receive." + line+":" + column;
		Debug.Log(messeage);
	}
}
