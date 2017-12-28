using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Animation : MonoBehaviour {

	[SerializeField]Animator animator;
	[SerializeField]E_AI ai;

	string[] anm_name = new string[] { "Idle", "Run", "Attack", "Hit", "Die" };
	bool attacked = false;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void Update () {

		if (ai.State == E_AI.ATTACK && !attacked) {
//			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);
			float time = animator.GetCurrentAnimatorStateInfo (0).length;
			attacked = true;
			StartCoroutine (ReturnToNormal (time));
			StartCoroutine (Attacked (time));
		}

		animator.SetBool (anm_name [(int)ai.State], true);
	}

	IEnumerator ReturnToNormal(float interval){
		yield return new WaitForSeconds (interval);
		ai.MakeThenRun ();
		attacked = false;
	}

	IEnumerator Attacked(float interval){
		yield return new WaitForSeconds (interval * .8f);
		if (ai.DistanceToTarger () <= 3f)
			ai.AttackedTheTarget ();
	}

}
