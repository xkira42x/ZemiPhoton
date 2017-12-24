using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_AI : Photon.MonoBehaviour {

	[SerializeField]NavMeshAgent agent;
	[SerializeField]Transform targetTransform;


	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	void Update () {
		if (targetTransform == null && PlayerList.length > 0) {
			int ii = Random.Range (0, PlayerList.length);
			photonView.RPC ("SyncTarget", PhotonTargets.AllBuffered, ii);
		}

		if (targetTransform != null) {
			//if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
			agent.SetDestination (targetTransform.position);
		}
	}


	[PunRPC]
	void SyncTarget(int index){
		targetTransform = PlayerList.GetPlayerList (index).transform;
	}
}
