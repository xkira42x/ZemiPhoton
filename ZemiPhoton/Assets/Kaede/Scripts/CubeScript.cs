using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : Photon.MonoBehaviour {

	private PhotonView photonView;
	private PhotonTransformView photonTransformView;

	public int hensu1 = 0;
	public float hensu2 = 0f;

	// Use this for initialization
	void Start () {
		photonTransformView = GetComponent<PhotonTransformView>();
		photonView = PhotonView.Get(this);
	}

	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			//現在の移動速度
			Vector3 velocity = gameObject.GetComponent<Rigidbody> ().velocity;
			//移動速度を指定
			photonTransformView.SetSynchronizedValues (velocity, 0);
		}

		if (Input.GetKey (KeyCode.W)) {
			transform.position += new Vector3 (0, 0, 0.1f);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.position += new Vector3 (-0.1f, 0, 0);
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.position += new Vector3 (0, 0, -0.1f);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (0.1f, 0, 0);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
			stream.SendNext (hensu1);
			stream.SendNext (hensu2);
		} else {
			this.hensu1 = (int)stream.ReceiveNext ();
			this.hensu2 = (float)stream.ReceiveNext ();
		}

	}
		
}