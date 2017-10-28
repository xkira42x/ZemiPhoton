using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]Text timeText;
	[SerializeField]Text GameMessage;
	[SerializeField]float timeLimit = 60;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator TimeCountDown(){
		while (true) {
			if (timeLimit <= 0) {
				GameMessage.text = "Time Over";
				break;
			}
			timeLimit -= .5f;
			timeText.text = "Time : " + ((int)timeLimit).ToString ();
			yield return new WaitForSeconds (1.0f);
		}
	}
}
