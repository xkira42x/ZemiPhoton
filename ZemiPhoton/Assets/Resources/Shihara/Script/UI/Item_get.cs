using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item_get : MonoBehaviour {

    public GameObject Canvas;
    Item_State2 IS2;

	// Use this for initialization
	void Start() {
        IS2 = Canvas.GetComponent<Item_State2>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Item_State2.Bom_color = 0.3f;
            IS2.Bomb_Select();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Item_State2.Rec_color = 0.3f;
            IS2.Recovery_Select();
        }

        if (Input.GetKeyDown("4"))
        {
            Item_State2.Gun_flg = 2;
            IS2.Gun_Select();
        }
        if (Input.GetKeyDown("1")) {
            Item_State2.Gun_flg = 1;
            IS2.Gun_Select();
        }
    }
}
