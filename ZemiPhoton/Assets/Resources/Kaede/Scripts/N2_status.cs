using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_status : Photon.MonoBehaviour {

	private int userid;
	private short hp = 100;
	public short Hp{ get { return hp; } set { hp = value; } }
	[PunRPC]
	public void Damage(short d){hp -= d;HpSlider.value = hp;}

	bool find = false;
	[SerializeField]
	Text myText;
	[SerializeField]
	Slider HpSlider;


	int no;
	public int No {	get { return no; } set { no = value; } }

	void Start(){
		myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
		HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();
		if (photonView.isMine) {
			no = PhotonNetwork.player.ID;
		} else {
			no = PhotonNetwork.player.ID;
		}
	}

	void Update(){
		// プレイヤーステータスの表示対象が見つからなかったら
		if (!find) {
			if (myText == null)
				myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
			if (HpSlider == null)
				HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();

			gameObject.name = "Player" + no.ToString ();

			if (photonView.isMine) {
				myText.text = "自分";
			} else {
				myText.text = "仲間";
			}

			if (myText != null && HpSlider != null)
				find = true;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (no);
			stream.SendNext (hp);
		} else {
			no = (int)stream.ReceiveNext ();
			hp = (short)stream.ReceiveNext ();
			HpSlider.value = hp;
		}
	}

}
