using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_status: Photon.MonoBehaviour {

	private int userid;
	private short hp = 100;
	public short Hp{ get { return hp; } set { hp = value; } }
	int no;
	public int No {	get { return no; } set { no = value; } }
	public void Damage(short d){hp -= d;HitPoint.value = hp;
		if (photonView.isMine)
			HpSlider.value = hp;
	}

	bool find = false;
	private GameObject namePlate;	//　名前を表示しているプレート
	public Text nameText;	//　名前を表示するテキスト
	private GameObject HitPointPlate;	//　HPを表示しているプレート
	public Slider HitPoint;

	//	[SerializeField]
	Text myText;
	//	[SerializeField]
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
			HpSlider = GameObject.Find ("MyHP").GetComponent<Slider> ();
		} 
		//プレイヤーの名前とHPバーを取得
		//		myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
		//HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();
//		transform.FindChild("HP").name="HitPoint"+no.ToString();
		HitPoint = this.GetComponentInChildren<Slider> ();
		myText = GameObject.Find ("MyPlayerName").GetComponent<Text> ();

		Debug.Log ("No:"+no);
		gameObject.name = "Player" + no.ToString ();
		if (no == 0)
			Debug.Log ("番号が割りふられていません" + gameObject.name);
		namePlate = nameText.transform.parent.gameObject;
//		HitPointPlate = HitPoint.transform.parent.gameObject;
		if(PhotonNetwork.player.IsMasterClient)
		StartCoroutine("SyncHp");
	}
	IEnumerator SyncHp(){
		while (true) {
//			photonView.RPC ("SetHP", PhotonTargets.OthersBuffered, Hp);
			yield return new WaitForSeconds(0.5f);
		}
	}

	void Update(){
		// プレイヤーステータスの表示対象が見つからなかったら
		if (!find) {
			if (myText == null)
				//				myText = GameObject.Find ("Status" + no.ToString ()).GetComponent<Text> ();
				myText = GameObject.Find ("MyPlayerName").GetComponent<Text> ();
			if (HpSlider == null)
				//				HpSlider = GameObject.Find ("HpSlider" + no.ToString ()).GetComponent<Slider> ();
				HpSlider = GameObject.Find ("MyHP").GetComponent<Slider> ();


			if (photonView.isMine) {
				myText.text = nameText.text;
				//				myText.text = "自分";
				//			} else {
				//				myText.text = "仲間"+no;
			}

			if (myText != null && HpSlider != null)
				find = true;
		}

		namePlate.transform.rotation = Camera.main.transform.rotation;
//		HitPoint.transform.rotation = Camera.main.transform.rotation;

	}

	[PunRPC]
	void SetName(string name) {
		nameText.text = name;
	}
	[PunRPC]
	void SetHP(short hp){
		Hp = hp;
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
