using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	[SerializeField]InputField inputField;
	[SerializeField]string MainSceneName = "main";

	void Start () {
		GameObject.Find ("LoginButton").GetComponent<Button> ().onClick.AddListener (() => OnLoginButton ());
		GameObject.Find ("ExitButton").GetComponent<Button> ().onClick.AddListener (() => OnExitButton ());
	}

	public void OnLoginButton(){

		if (string.IsNullOrEmpty (inputField.text)) {
			inputField.image.color = Color.red;
		}else {
		Debug.Log ("Login");
		PlayerInfo.playerName = inputField.text;
		SceneManager.LoadScene (MainSceneName);
		}
	}

	public void OnValueChanged(){
		inputField.image.color = Color.white;
	}

	public void OnExitButton(){
		Debug.Log ("Exit");
		Application.Quit ();
	}
}
