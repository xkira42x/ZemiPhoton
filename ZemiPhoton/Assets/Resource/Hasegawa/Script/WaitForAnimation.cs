//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimation : CustomYieldInstruction {

	Animator _animator;
	int _lastStateHash = 0;
	int _layerNo = 0;

//	public WaitAnimation(Animator animator,int layerNo){
//		Init (animator, layerNo, animator.GetCurrentAnimatorStateInfo (layerNo).nameHash);
//	}

	void Init(Animator animator,int layerNo,int hash){
		_layerNo = layerNo;
		_animator = animator;
		_lastStateHash = hash;
	}

	public override bool keepWaiting {
		get {
			var currentAnimatorState = _animator.GetCurrentAnimatorStateInfo (_layerNo);
			return currentAnimatorState.fullPathHash == _lastStateHash && (currentAnimatorState.normalizedTime < 1);
		}
	}

}
