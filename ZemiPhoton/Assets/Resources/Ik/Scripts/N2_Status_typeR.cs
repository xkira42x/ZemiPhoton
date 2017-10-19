using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_Status_typeR : Photon.MonoBehaviour {

	private int userid;
	private short hp = 100;
	public short Hp{ get { return hp; } set { hp = value; } }
	int no;
	public int No {	get { return no; } set { no = value; } }
	public void Damage(short d){hp -= d;HpSlider.value = hp;
	}

	bool find = false;
	[SerializeField]
	Text myText;
	[SerializeField]
	Slider HpSlider;



	[PunRPC]
	void NoSet(int nn){
		no = nn;
	}
	void Start(){
		
		if (photonView.isMine) {
			//自分のIDを埋め込む
			no = PhotonNetwork.player.ID;
			//自分のIDを他のPCのクローンに代入させる
			photonView.RPC ("NoSet",PhotonTargets.OthersBuffered,no);
		} 
		//プレイヤーの名前とHPバーを取得
		myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
		HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();

		Debug.Log ("No:"+no);
		gameObject.name = "Player" + no.ToString ();
		if (no == 0)
			Debug.Log ("番号が割りふられていません" + gameObject.name);
	}

	void Update(){
		// プレイヤーステータスの表示対象が見つからなかったら
		if (!find) {
			if (myText == null)
				myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
			if (HpSlider == null)
				HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();


			if (photonView.isMine) {
				myText.text = "自分";
			} else {
				myText.text = "仲間"+no;
			}

			if (myText != null && HpSlider != null)
				find = true;
		}
	}

/*	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (no);
			stream.SendNext (hp);
		} else {
			no = (int)stream.ReceiveNext ();
			hp = (short)stream.ReceiveNext ();
			HpSlider.value = hp;
		}
	}
*/

}
