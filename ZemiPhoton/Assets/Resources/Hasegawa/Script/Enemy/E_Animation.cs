using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Animation : MonoBehaviour {

	/// アニメーター格納
	[SerializeField]Animator animator;
	/// AIソースの格納
	[SerializeField]E_AI ai;

	/// アニメーションの名前格納
	string[] anm_name = new string[] { "Idle", "Run", "Attack", "Hit", "Die" };
	/// 攻撃制限のフラグ
	bool attacked = false;
	/// 死亡制限のフラグ
	bool died = false;

	/// アニメーションの取得
	void Start () {
		animator = GetComponent<Animator> ();
	}

	/// E_AIのステータスによってアニメーションを制御する
	/// アニメーション以外の処理（攻撃の当たり判定・死亡時の処理）も
	/// この中でやっている
	void Update () {

		// 攻撃処理
		if (ai.State == E_AI.ATTACK && !attacked) {
			// アニメーションの再生時間を取得
			float time = animator.GetCurrentAnimatorStateInfo (0).length;
			attacked = true;
			// アニメーションの切り替る
			StartCoroutine (ReturnToNormal (time));
			// 攻撃判定する
			StartCoroutine (Attacked (time));
		}

		// 死亡処理
		if (ai.State == E_AI.DIE && !died) {
			// アニメーションの再生時間を取得
			float time = animator.GetCurrentAnimatorStateInfo (0).length;
			died = true;
			// 死亡後に削除する
			StartCoroutine (Died (time));
		}

		// アニメーション再生
		animator.SetBool (anm_name [(int)ai.State], true);
	}

	/// 通常行動のステータスに戻る（走る）
	IEnumerator ReturnToNormal(float interval){
		yield return new WaitForSeconds (interval);
		ai.MakeThenRun ();
		attacked = false;
	}

	/// 一定時間後に攻撃処理をする
	/// 攻撃アニメーションで腕を振り切ったタイミングで
	/// 攻撃判定が走るように調整する
	IEnumerator Attacked(float interval){
		yield return new WaitForSeconds (interval * .8f);
		if (ai.DistanceToTarger () <= 3f)
			ai.AttackedTheTarget ();
	}

	/// 倒された際のアニメーションが終わったタイミングで自身を削除する
	IEnumerator Died(float interval){
		yield return new WaitForSeconds (interval);
		Destroy (gameObject);
	}

}
