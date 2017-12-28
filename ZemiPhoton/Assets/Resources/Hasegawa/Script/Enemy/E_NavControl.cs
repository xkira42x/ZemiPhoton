using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_NavControl : MonoBehaviour {

	[SerializeField]E_AI ai;
	[SerializeField]NavMeshAgent agent;

	/// 時間経過でナビゲーションを起動させる
	/// 敵の生成が完了される前にナビゲーションを起動させると
	/// エラーで動かなくなるのでその対策
	void Start () {
		StartCoroutine (SetNaviControl ());
	}

	IEnumerator SetNaviControl(){
		yield return new WaitForSeconds (1);
		agent.enabled = true;
		ai.enabled = true;

		Destroy (this);
	}
}
