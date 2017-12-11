using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status_tes : MonoBehaviour {

    float HP = 1;

    Image HP_l4d2;

	// Use this for initialization
	void Start () {
        HP_l4d2 = GameObject.Find("HP").GetComponent<Image>();
        
	}

    // Update is called once per frame
    void Update() {
        HP -= 0.01f;
        if (HP < 0) HP = 1;

        HP_l4d2.fillAmount = HP;
        HP_l4d2.color = new Color(1.0f, HP, 1.0f, 1.0f);

    }
}
