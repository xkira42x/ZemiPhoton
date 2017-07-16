using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_Status : Photon.MonoBehaviour {

	private int userid;
	private float hp = 1;
	public void Damage(float d){
		hp -= d;
		HpSlider.value = hp;
	}

	//private int userid2;
	//private int hp2;

	//private Text p1Text;
	//private Text p2Text;
	bool find = false;
	[SerializeField]
	Text myText;
	[SerializeField]
	Slider HpSlider;


	int no;
	public int No {	get { return no; } set { no = value; } }

	void Start(){
		//p1Text = GameObject.Find ("Status1").GetComponent<Text> ();
		//p2Text = GameObject.Find ("Status2").GetComponent<Text> ();
		myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
		HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();
		if (photonView.isMine) {
			userid = PhotonNetwork.player.ID;
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
			myText.text = "Player" + no.ToString ();

			if (myText != null && HpSlider != null)
				find = true;
		}

		//p1Text.text = "自分： " + hp;
		//p2Text.text = "仲間： " + hp2;
	}

	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (no);
			stream.SendNext (hp);
			//stream.SendNext (userid);
			//stream.SendNext (hp);
		} else {
			no = (int)stream.ReceiveNext ();
			hp = (float)stream.ReceiveNext ();
			//userid2 = (int)stream.ReceiveNext ();
			//hp2 = (int)stream.ReceiveNext ();
			//Debug.Log ("Receive: " + hp2);

		}
	}


}
