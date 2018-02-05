using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class E_NavControl : MonoBehaviour {

	/// AIに当たる部分のソースを取得
	[SerializeField]E_AI ai;
	/// ナビメッシュコンポーネントを取得
	[SerializeField]NavMeshAgent agent;

	/// 時間経過でナビゲーションを起動させる
	/// 敵の生成が完了される前にナビゲーションを起動させると
	/// エラーで動かなくなるのでその対策
	void Start () {
		StartCoroutine (SetNaviControl ());
	}

	/// 時間経過でナビゲーションを起動させる
	/// その後、このソースは不要なので削除する
	IEnumerator SetNaviControl(){
		yield return new WaitForSeconds (1);
		agent.enabled = true;
		ai.enabled = true;

		Destroy (this);
	}
}
