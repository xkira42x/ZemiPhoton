using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_status_Test : Photon.MonoBehaviour {

	private GameObject namePlate;	//　名前を表示しているプレート
	public Text nameText;	//　名前を表示するテキスト

	private GameObject HitPointPlate;	//　HPを表示しているプレート
	public Slider HitPoint;
	private short HP = 100;

	bool find = false;
	[SerializeField]
	Text myText;
	[SerializeField]
	Slider HpSlider;

	int no;

	void Start(){

		myText = GameObject.Find ("MyPlayerName").GetComponent<Text> ();
		HpSlider = GameObject.Find ("HP").GetComponent<Slider> ();

		if (photonView.isMine) {
			no = PhotonNetwork.player.ID;
		}

		namePlate = nameText.transform.parent.gameObject;
		HitPointPlate = HitPoint.transform.parent.gameObject;
	}

	void Update(){
		// プレイヤーステータスの表示対象が見つからなかったら
		if (!find) {

			gameObject.name = "Player" + no.ToString ();

			if (photonView.isMine) {
				myText.text = nameText.ToString();
			}

			if (myText != null )
				find = true;
		}

		namePlate.transform.rotation = Camera.main.transform.rotation;
		HitPoint.transform.rotation = Camera.main.transform.rotation;

	}

	[PunRPC]
	void SetName(string name) {
		nameText.text = name;
	}

	[PunRPC]
	void SetHP(short hp){
		HitPoint.value = hp;
	}

	[PunRPC]
	void Damage( short d){
		HP -= d;
		HitPoint.value = HP;
		HpSlider.value = HP;
	}
}
