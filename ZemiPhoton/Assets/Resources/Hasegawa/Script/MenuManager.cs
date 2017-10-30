using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]Text InputText;
	[SerializeField]string MainSceneName = "main";

	void Start () {
		GameObject.Find ("LoginButton").GetComponent<Button> ().onClick.AddListener (() => OnLoginButton ());
		GameObject.Find ("ExitButton").GetComponent<Button> ().onClick.AddListener (() => OnExitButton ());
	}

	public void OnLoginButton(){
		Debug.Log ("Login");
		PlayerInfo.playerName = InputText.text;
		SceneManager.LoadScene (MainSceneName);
	}

	public void OnExitButton(){
		Debug.Log ("Exit");
		Application.Quit ();
	}
}
