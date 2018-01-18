using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Animation : MonoBehaviour {

	/// アニメーター格納
	[SerializeField]protected Animator animator;
	/// AIソースの格納
	[SerializeField]protected E_AI ai;

	/// アニメーションの名前格納
	protected string[] anm_name = new string[] { "Idle", "Run", "Attack", "Hit", "Die" };
	/// 攻撃制限のフラグ
	protected bool attacked = false;
	/// 死亡制限のフラグ
	protected bool died = false;

	/// アニメーションの取得
	void Start () {
		animator = GetComponent<Animator> ();
	}

	/// E_AIのステータスによってアニメーションを制御する
	/// アニメーション以外の処理（攻撃の当たり判定・死亡時の処理）も
	/// この中でやっている
	void Update () {
		
		// アニメーション再生
		animator.SetBool (anm_name [(int)ai.State], true);

		// 攻撃処理
		if (ai.State == E_AI.ATTACK && !attacked) {
			// アニメーションの再生時間を取得
			float time = animator.GetCurrentAnimatorStateInfo (0).length;
			attacked = true;
			// 攻撃判定する
			StartCoroutine (Attacked (time));
		}

		// 死亡処理
		if (ai.State == E_AI.DIE && !died) {
			//if (animator.GetCurrentAnimatorStateInfo (0).shortNameHash.Equals (Animator.StringToHash("Die"))) {
				// アニメーションの再生時間を取得
				float time = animator.GetCurrentAnimatorStateInfo (0).length;
				died = true;
				// 死亡後に削除する
				StartCoroutine (Died (time));
			//}
		}

	}

	/// 一定時間後に攻撃処理をする
	/// 攻撃アニメーションで腕を振り切ったタイミングで
	/// 攻撃判定が走るように調整する
	IEnumerator Attacked(float interval){

		// アニメーションのステータスを取得
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);

		// 残骸・・・見せしめに残しておく
/*		float tt = animator.GetCurrentAnimatorStateInfo (0).length / 2;//interval / 2;
		animator.SetBool (anm_name [(int)E_AI.ATTACK], false);
		yield return new WaitForSeconds (tt);
		ai.AttackedTheTarget ();
		yield return new WaitForSeconds (tt);
		ai.MakeThenRun ();
		ai.AttackedTheTarget ();
		attacked = false;*/

		// ダメージ処理
		ai.AttackedTheTarget ();
		// アニメーションが終了するまで１フレームずつ待つ
		while (!ChangeAnimation (stateInfo))
			yield return null;
		// アニメーションの攻撃を無効化
		animator.SetBool (anm_name [(int)E_AI.ATTACK], false);
		// 追尾状態にシフト
		ai.MakeThenRun ();
		// 次の攻撃ができるようにする
		attacked = false;

	}

	/// 倒された際のアニメーションが終わったタイミングで自身を削除する
	IEnumerator Died(float interval){

		while (!animator.GetCurrentAnimatorStateInfo (0).shortNameHash.Equals (Animator.StringToHash("Die")))
			yield return null;

		// アニメーションのステータスを取得
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		yield return new WaitForSeconds (stateInfo.length);
		//yield return new WaitForSeconds (interval);
		Destroy (gameObject);
	}


	/// アニメーションが変わったかをハッシュ比較でする
	/// 戻り値　true:変わった　false:変わっていない
	bool ChangeAnimation(AnimatorStateInfo info){
		return info.nameHash != animator.GetCurrentAnimatorStateInfo (0).nameHash;
	}

}
