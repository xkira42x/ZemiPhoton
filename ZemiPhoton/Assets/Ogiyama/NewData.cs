using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewData : MonoBehaviour {

	public Text UserMessage;
	public Text UserName;
	private int Button_flg=0;

	public string ServerAddress = "10.22.1.79/3zemi/DB_test_unity_input.php";

	public void CreateUser_Button_Push(){
		UserMessage.GetComponent<Text> ().text = "Prese New UserName&PassWord";
		UserName.GetComponent<Text> ().text = "User Name";

	}

}