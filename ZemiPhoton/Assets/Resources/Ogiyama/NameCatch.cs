using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCatch : MonoBehaviour {

	public Text ResultText_;	//結果格納用テキスト
	public Text InputName;		//IDを入力するインプットフィールド
//	public Text InputPass;		//パスワードを入力するインプットフィールド

	//	public Text NotID;

	//server接続のアドレス
	//public string ServerAddress = "zemi8bit.php.xdomain.jp/DB_test_unity_select.php";
	//157.112.147.22
	//public string ServerAddress ="157.112.147.43";

	//public string ServerAddress = "localhost/3zemi/DB_test_unity_select.php";

	string ServerAddress = "10.22.1.156/3zemi/DB_test_unity_select_name.php";

	public void SendSignal_Button_Push(){

		StartCoroutine ("Access");	//Accessコルーチンの開始

	}

	private IEnumerator Access(){

		Dictionary<string,string> dic = new Dictionary<string,string> ();

		//インプットフィールドからIDの所得
		dic.Add("name",InputName.GetComponent<Text>().text);
		//dic.Add ("pass", InputPass.GetComponent<Text> ().text);
		//複数phpに送信したいデータがある場合はdic.Add("hoge",value)のように足す

		StartCoroutine (Post (ServerAddress, dic));//POST
		yield return 0;
	}

	private IEnumerator Post(string url,Dictionary<string,string>post){
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string> post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);
		}
		WWW www = new WWW (url, form);

		yield return StartCoroutine (CheckTimeOut (www, 3f));	//TimeOutSecond=3s

		if (www.error != null) {
			ResultText_.GetComponent<Text>().text="ConnectingError";
			Debug.Log("HttpPost NG: " + www.error);
			//そもそも接続ができていないとき

		} else if (www.isDone) {
			
				//送られてきたデータをテキストに反映
				ResultText_.GetComponent<Text> ().text = www.text;
				//デバッグ用(PHPから送られるデータ量の確認)
				//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();
				//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();
			}
		}

	private IEnumerator CheckTimeOut(WWW www, float timeout) {
		float requestTime = Time.time;

		while (!www.isDone) {
			if (Time.time - requestTime < timeout)
				yield return null;
			else {
				ResultText_.GetComponent<Text>().text="TimeOut";  //タイムアウト
				//タイムアウト処理
				//
				//
				break;
			}
		}
		yield return null;
	}
}