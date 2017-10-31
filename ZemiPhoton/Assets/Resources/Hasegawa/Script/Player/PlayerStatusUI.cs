using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour {

	[SerializeField]Text userNameUI;
	[SerializeField]Slider hpSlider;

	public string UserName { set { userNameUI.text = value; } }

	public short Health { set { hpSlider.value = value; } }

//	void Awake () {
//		userNameUI = GetComponentInChildren<Text> ();
//		hpSlider = GetComponentInChildren<Slider> ();
//	}
}