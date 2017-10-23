using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewData : MonoBehaviour {

	public Text UserMessage;
	public Text UserName;
	public Text NewPass;
	public Text Button_Message;
	//public int Button_flg=0;

	//string ServerAddress = "localhost/3zemi/DB_test_unity_input.php";

	string ServerAddress = "10.22.1.156/3zemi/DB_test_unity_input.php";

	public void NewData_Button_Push(){
		
			StartCoroutine ("DataAccess");
		/*if (Button_flg == 0) {
			UserMessage.GetComponent<Text> ().text = "Prese New UserName&PassWord";
			UserName.GetComponent<Text> ().text = "User Name";
			Button_Message.GetComponent<Text> ().text = "Enter!";
			Button_flg = 1;
		} else {
			UserMessage.GetComponent<Text> ().text = "Prease\nID&PassWord";
			UserName.GetComponent<Text>().text="ID";
			Button_Message.GetComponent<Text> ().text = "New Data";

			Button_flg = 0;
		}*/
	}
		
	private IEnumerator DataAccess(){
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		dic.Add ("name", UserName.GetComponent<Text> ().text);
		//dic.Add ("pass", NewPass.GetComponent<Text> ().text);
		StartCoroutine(DataPost(ServerAddress,dic));

		yield return 0;
	}

	private IEnumerator DataPost(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string>post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);

			WWW www = new WWW (url, form);

			yield return StartCoroutine (CheckTimeOut (www, 3f));	//TimeOutSecond=3s

			if (www.error != null) {
				UserMessage.GetComponent<Text> ().text = "ConnectingError";
				Debug.Log ("HttpPost NG: " + www.error);
				//そもそも接続ができていないとき

			} else if (www.isDone) {
				if (www.bytesDownloaded != 7) {
					UserMessage.GetComponent<Text> ().text = "Registration Complete";
				} else {
					UserMessage.GetComponent<Text> ().text = "Sorry\nPlease other name";
				}
			}
		}
	}

	private IEnumerator CheckTimeOut(WWW www, float timeout) {
		float requestTime = Time.time;
		while (!www.isDone) {
			if (Time.time - requestTime < timeout)
				yield return null;
			else {
				UserMessage.GetComponent<Text>().text="TimeOut";  //タイムアウト
				//タイムアウト処理
				//
				//
				break;
			}
		}
		yield return null;
	}

}