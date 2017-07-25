using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewData : MonoBehaviour {

	public Text UserMessage;
	public Text UserName;
	public Text Button_Message;
	public int Button_flg=0;

	public string ServerAddress = "10.22.1.79/3zemi/DB_test_unity_input.php";

	public void NewData_Button_Push(){
		if (Button_flg == 0) {
			UserMessage.GetComponent<Text> ().text = "Prese New UserName&PassWord";
			UserName.GetComponent<Text> ().text = "User Name";
			Button_Message.GetComponent<Text> ().text = "Enter!";
			Button_flg = 1;
		} else {
			UserMessage.GetComponent<Text> ().text = "Prease\nID&PassWord";
			UserName.GetComponent<Text>().text="ID";
			Button_Message.GetComponent<Text> ().text = "New Data";
			Button_flg = 0;

			StartCoroutine ("DataAccess");

		}
	}
		
	private IEnumerator DataAccess(){
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		dic.Add ("name", UserName.GetComponent<Text> ().text);
		dic.Add ("pass", Catch.InputPass, GetComponent<Text> ().text);

		StartCoroutine(DataPost(ServerAddress,dic));
		yield return 0;
	}

	private IEnumerator DataPost(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string>post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);
		}
	}

}