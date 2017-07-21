using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_Status : Photon.MonoBehaviour {

	private int userid;
	private short hp = 100;
	public short Hp{ get { return hp; } set { hp = value; } }
	public void Damage(short d){hp -= d;HpSlider.value = hp;}

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
			no = PhotonNetwork.player.ID;
		} else {
			no = 3-PhotonNetwork.player.ID;
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

/*		GameObject.Find ("Status1").GetComponent<Text> ().text = "自分";
		GameObject.Find ("HpSlider1").GetComponent<Slider> ().value = hp;
		GameObject.Find ("Status2").GetComponent<Text> ().text = "仲間";
		GameObject.Find ("HpSlider2").GetComponent<Slider> ().value = hp2;*/

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
			hp = (short)stream.ReceiveNext ();
			//userid2 = (int)stream.ReceiveNext ();
			//hp2 = (int)stream.ReceiveNext ();
			//Debug.Log ("Receive: " + hp2);
			HpSlider.value = hp;
		}
	}


}
