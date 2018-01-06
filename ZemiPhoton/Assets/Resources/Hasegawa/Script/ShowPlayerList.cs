using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerList : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		StartCoroutine (Show ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Show(){
		while (true) {
			text.text = "";
			for (int i = 0; i < PlayerList.length; i++) {
				string ss = PlayerList.GetPlayerList (i).GetComponent<S2_Status> ().UserName;
				text.text += i.ToString () + " : " + ss + "\n";
			}
			yield return new WaitForSeconds (1);
		}
	}
}
