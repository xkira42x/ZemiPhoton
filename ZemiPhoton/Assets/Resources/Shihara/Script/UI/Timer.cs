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

	// Use this for initialization
	void Start () {
        Gauge = GameObject.Find("Timer").GetComponent<Image>();
        Gauge_T_max = Gauge_T;
        StartCoroutine(TimeCount());
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
			if (PlayerInfo.Spawn)
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
            Gauge_T = Gauge_T_max;
            Gauge_G = 0;
        }

        Time_T.text = "" + (Gauge_T * 10).ToString("f0");

        Gauge.color = new Color(Gauge_G/Gauge_T_max, Gauge_T/Gauge_T_max, 0.0f, 1.0f);

        Time_T.color = new Color(Gauge_G / Gauge_T_max, Gauge_T / Gauge_T_max, 0.0f, 1.0f);

    }
}
