using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : Photon.MonoBehaviour {

	private PhotonView photonView;
	private PhotonTransformView photonTransformView;

	public float hensu1 = 0f;

	// Use this for initialization
	void Start () {
//		photonTransformView = GetComponent<PhotonTransformView>();
		photonView = PhotonView.Get(this);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (0.1f, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position -= new Vector3 (0.1f, 0, 0);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){

		if (stream.isWriting) {
			Debug.Log ("Serialize Sou"+Time.time);
			//データの送信
			stream.SendNext (this.transform.position.x);
		} else {
			Debug.Log ("Serialize Juu"+Time.time);
			//データの受信
			this.hensu1 = (float)stream.ReceiveNext ();
			this.transform.position = new Vector3 (hensu1, this.transform.position.y, this.transform.position.z);
		}
	}
}