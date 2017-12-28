using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AI : Photon.MonoBehaviour {

	public const byte IDOL=0,RUN=1,ATTACK=2,HIT=3,DIE=4;
	[SerializeField]byte state = RUN;
	public byte State{ get { return state; } set { state = value; } }
	public void MakeThenRun(){
		state = RUN;
		if (targetTransform != null)
			agent.SetDestination (targetTransform.position);
	}

	bool attacked = false;
	float pow = 10;

	[SerializeField]NavMeshAgent agent;
	[SerializeField]Transform targetTransform;
	Vector3 targetPos;
	int targetIndex;

	void Start () {

		agent = GetComponent<NavMeshAgent> ();

		SetTarget ();
//		if (targetTransform == null && PlayerList.length > 0) {
//			int ii = Random.Range (0, PlayerList.length);
//			photonView.RPC ("SyncTarget", PhotonTargets.AllBuffered, ii);
//		}

		StartCoroutine (SetDesti ());
	}
	
	void Update () {

		SetTarget ();
//		if (targetTransform == null && PlayerList.length > 0) {
//			int ii = Random.Range (0, PlayerList.length);
//			photonView.RPC ("SyncTarget", PhotonTargets.AllBuffered, ii);
//		}

		AIState ();

		//Debug.Log (state);
	}

	void SetTarget(){
		if (targetTransform == null && PlayerList.length > 0) {
			targetIndex = Random.Range (0, PlayerList.length);
			photonView.RPC ("SyncTarget", PhotonTargets.AllBuffered, targetIndex);
		}
	}

	public float DistanceToTarger(){
		return agent.remainingDistance;
	}

	void AIState(){
		if (state != ATTACK) {
			if (DistanceToTarger() > 2.2f)
				state = RUN;
			else
				state = ATTACK;
		}
	}

	IEnumerator SetDesti(){
		while (true) {
			if (targetTransform != null && state == RUN) {
				//if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
				agent.SetDestination (targetTransform.position);
			}
			yield return new WaitForSeconds (1);
		}
	}


	[PunRPC]
	void SyncTarget(int index){
		targetTransform = PlayerList.GetPlayerList (index).transform;
	}

	public void AttackedTheTarget(){
		Debug.Log ("Call damage");
		PlayerList.GetPlayerList (targetIndex).GetComponent<S2_Status> ().Damage (pow);
	}
}
