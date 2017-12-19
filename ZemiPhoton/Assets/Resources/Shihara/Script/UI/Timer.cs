using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Image Gauge;
    public float Gauge_T;
    float Gauge_T_max;

    float Gauge_G = 0;

    public Text Time_T;
	[SerializeField]GameObject Panel;
	Image panelimage;
	// Use this for initialization
	void Start () {
        Gauge = GameObject.Find("Timer").GetComponent<Image>();
        Gauge_T_max = Gauge_T;
        StartCoroutine(TimeCount());

		panelimage = Panel.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
			if (PlayerInfo.onTimer)
				EnGauge_Time ();
        }
    }


    //ゲームタイマー(コルーチン.var)
    void EnGauge_Time()
    {
        Gauge_G += 0.1f;
        Gauge_T -= 0.1f;
        Gauge.fillAmount = Gauge_G/Gauge_T_max;

        if (Gauge_T <= 0) {
//            Gauge_T = Gauge_T_max;
            Gauge_T = 0;
            Gauge_G = 0;
			GameEnd ();
        }

        Time_T.text = "" + (Gauge_T * 10).ToString("f0");

        Gauge.color = new Color(Gauge_G/Gauge_T_max, Gauge_T/Gauge_T_max, 0.0f, 1.0f);

        Time_T.color = new Color(Gauge_G / Gauge_T_max, Gauge_T / Gauge_T_max, 0.0f, 1.0f);

    }
	//ゲーム終了処理
	//敵の生成を止め、出ている敵を消す
	void GameEnd(){
		PlayerInfo.Spawn = false;
		int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
		if (enemies != 0) {
			for (int i = 0; i < enemies; i++) {
				PhotonNetwork.Destroy (GameObject.FindGameObjectsWithTag("Enemy") [i]);
//			GameObject.FindGameObjectsWithTag("Enemy") [i].GetComponent<A_normal_enemy_move1>().A_state=0;
			}
		}
		Panel.SetActive (true);
		panelimage.color += new Color (0, 0, 0, 0.1f);
		if (panelimage.color.a >= 1) {
			Cursor.lockState=CursorLockMode.None;	//ロックなし
			Cursor.visible=true;
			PhotonNetwork.Disconnect ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("title");
		}
	}
}
