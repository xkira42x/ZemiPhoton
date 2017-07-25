using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catch : MonoBehaviour {

	public Text ResultText_;	//結果格納用テキスト
	public Text InputID;		//IDを入力するインプットフィールド
	public Text InputPass;		//パスワードを入力するインプットフィールド

//	public Text NotID;

	//server接続のアドレス
	public string ServerAddress = "10.22.1.79/3zemi/DB_test_unity_select.php";

	public void SendSignal_Button_Push(){
		
		StartCoroutine ("Access");	//Accessコルーチンの開始

	}

	private IEnumerator Access(){
		
		Dictionary<string,string> dic = new Dictionary<string,string> ();

		//インプットフィールドからIDの所得
		dic.Add("id",InputID.GetComponent<Text>().text);
		dic.Add ("pass", InputPass.GetComponent<Text> ().text);
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
			//Debug.Log("HttpPost NG: " + www.error);
			//そもそも接続ができていないとき

		} else if (www.isDone) {
			//PHPから送られるnullの量が7byteだったためそれで比較
			//null以外でPHPから送られる情報量が同じ7byteだった場合、
			//期待通りの結果にならない可能性があるため、以下の処理は危険性大
			if (www.bytesDownloaded != 7) {	
				//送られてきたデータをテキストに反映
				ResultText_.GetComponent<Text> ().text = www.text;
				//デバッグ用(PHPから送られるデータ量の確認)
				//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();
			} else {
				ResultText_.GetComponent<Text>().text="NotID";
				//ResultText_.GetComponent<Text> ().text = www.bytesDownloaded.ToString ();
			}
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