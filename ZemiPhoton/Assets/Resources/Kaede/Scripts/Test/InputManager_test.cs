using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager_test : MonoBehaviour {

	InputField inputField;

	public string inputValue;


	// Startメソッド
	/// InputFieldコンポーネントの取得および初期化メソッドの実行

	void Start() {

		inputField = GetComponent<InputField>();

		InitInputField();
	}
		
	/// Log出力用メソッド
	/// 入力値を取得してLogに出力し、初期化

	public void InputLogger() {
	
		inputValue = inputField.text;

		GameObject.Find ("PhotonConnectB").GetComponent<Button> ().interactable = true;

		InitInputField();
	}
		
	// InputFieldの初期化用メソッド
	// 入力値をリセットして、フィールドにフォーカスする

	void InitInputField() {

		// 値をリセット
		inputField.text = "";

		// フォーカス
		inputField.ActivateInputField();
	}

}
