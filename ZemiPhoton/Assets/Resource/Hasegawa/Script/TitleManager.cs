using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	[SerializeField]InputField inputField;			// 名前入力フォーム
	[SerializeField]string MainSceneName = "main";	// 遷移するシーン名

	/// 初期化
	void Start () {
		GameObject.Find ("LoginButton").GetComponent<Button> ().onClick.AddListener (() => OnLoginButton ());
		GameObject.Find ("ExitButton").GetComponent<Button> ().onClick.AddListener (() => OnExitButton ());
	}

	/// ログインボタン
	public void OnLoginButton(){
		// 名前が入力されている場合メインシーンに遷移する
		if (string.IsNullOrEmpty (inputField.text)) {
			inputField.image.color = Color.red;
		}else {
		PlayerInfo.playerName = inputField.text;
		SceneManager.LoadScene (MainSceneName);
		}
	}

	/// 名前入力フォームを白色に戻す
	public void OnValueChanged(){
		inputField.image.color = Color.white;
	}

	/// ゲームを終了
	public void OnExitButton(){
		Application.Quit ();
	}
}
