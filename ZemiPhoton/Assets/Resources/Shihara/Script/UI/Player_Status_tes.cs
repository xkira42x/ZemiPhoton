using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status_tes : MonoBehaviour {

    float HP = 1;

    Image HP_1;
    Image HP_2;
    // Use this for initialization
    void Start () {
        HP_1 = GameObject.Find("HP_1").GetComponent<Image>();
        HP_2 = GameObject.Find("HP_2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        HP -= 0.01f;
        if (HP < 0) HP = 1;

        HP_1.fillAmount = HP;
        HP_1.color = new Color(1.0f, HP, 1.0f, 1.0f);

        HP_2.fillAmount = HP;
        HP_2.color = new Color(1.0f, HP, 1.0f, 1.0f);
    }
}
