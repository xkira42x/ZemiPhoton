using UnityEngine;

public class N2_NetworkSetting : Photon.MonoBehaviour {
	
	[SerializeField]GameObject myCamera;	// カメラ
	[SerializeField]GameObject myCanvas;	// UIキャンバス
	[SerializeField]GameObject body;		// キャラクターボディ

	/// 初期化
	void Awake () {

		// 操作するキャラにはカメラとUI表示を有効にして、それ以外は無効にする
		if (photonView.isMine) {
			myCamera.SetActive (true);
			myCanvas.SetActive (true);
			GetComponent<Rigidbody> ().useGravity = true;
			body.SetActive (false);
		} else {
			GetComponent<S1_Move> ().enabled = false;
			GetComponent<S2_Angle> ().enabled = false;
			Destroy (myCanvas);
		}

		Destroy (this);
	}
}