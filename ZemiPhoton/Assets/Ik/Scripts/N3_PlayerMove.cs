using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N3_PlayerMove : Photon.MonoBehaviour {

	private Vector3 N_hensu2=new Vector3(0,0,0);

	N3_CubeScript N_CS;
	// Use this for initialization
	void Start () {
		StartCoroutine ("move");
		N_CS = this.GetComponent<N3_CubeScript> ();
	}

	// Update is called once per frame
	void Update () {

		//キー入力は操作プレイヤーのみにしたいので、isMineで制御する
		if (photonView.isMine) {
			if (Input.GetKey (KeyCode.D)) {
				transform.position += new Vector3 (0.1f, 0, 0);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.position -= new Vector3 (0.1f, 0, 0);
			}
			if (Input.GetKey (KeyCode.W)) {
				transform.position += new Vector3 (0, 0, 0.1f);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.position -= new Vector3 (0, 0, 0.1f);
			}
		}
		//同期された値を拾う
		N_hensu2 = N_CS.N_hensu1;
	}
	IEnumerator move(){
		while(true){
			this.transform.position = new Vector3 (this.transform.position.x+N_hensu2.x/5f, this.transform.position.y, this.transform.position.z+N_hensu2.z/5f);

			//移動呼び出しは微調整 PCスペックにより左右される可能性有
			yield return new WaitForSeconds(0.01655f);
		}		
	}
}