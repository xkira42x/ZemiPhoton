using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour {

	[SerializeField]Text userNameUI;
//	[SerializeField]Slider hpSlider;
	[SerializeField]Image hpImage;
	float hp = 1;

	public string UserName { set { userNameUI.text = value; } }

//	public short Health { set { hpSlider.value = value; } }
	public float Health {
		set {
			hp = (value / 100);
			hpImage.fillAmount = hp;
			hpImage.color = new Color (1f, hp, 1f, 1f);
		}
	}

}