using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour {

	[SerializeField]Text userNameUI;	// ユーザ名の表示UI
	[SerializeField]Image hpImage;		// ヒットポイントの表示UI 
	float hp = 1;						// ヒットポイント


	/// ユーザ名の設定
	public string UserName { set { userNameUI.text = value; } }

	/// ヒットポイントの設定
	/// 1～0までの間で保存をするため、代入の際に(/100)する
	/// 同時にHPゲージを更新する
	public float Health {
		set {
			hp = (value / 100);
			hpImage.fillAmount = hp;
			hpImage.color = new Color (1f, hp, 1f, 1f);
		}
	}

}