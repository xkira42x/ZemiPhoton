using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Item_State : MonoBehaviour {

    public Image H_gun;
    public Image M_gun;

    public Image Bomb;
    public Image Recovery;


    public static bool Gun_flg = true;
    public static float Bom_color = 1.0f;
    public static float Rec_color = 1.0f;


    enum ItemState
    {
        S_Gun,
        S_Bom,
        S_Rec
    };
    ItemState IS;
    
    void Start()
    {
        Bomb_Select(0.3f);
        Recovery_Select(0.3f);
        Gun_Select(1.0f);
    }

    void Update ()
    {

        switch (IS)
        {
            case ItemState.S_Gun:
                if (Input.GetKeyDown("2")){
                    Gun_Select(0.3f);
                    Bomb_Select(1.0f);
                    Recovery_Select(0.3f);
                }
                if (Input.GetKeyDown("3")) {
                    Gun_Select(0.3f);
                    Bomb_Select(0.3f);
                    Recovery_Select(1.0f);
                }
                break;

            case ItemState.S_Bom:
                if (Input.GetKeyDown("1")) {
                    Gun_Select(1.0f);
                    Bomb_Select(0.3f);
                    Recovery_Select(0.3f);
                }
                if (Input.GetKeyDown("3")) {
                    Gun_Select(0.3f);
                    Bomb_Select(0.3f);
                    Recovery_Select(1.0f);
                }
                break;

            case ItemState.S_Rec:
                if (Input.GetKeyDown("1")){
                    Gun_Select(1.0f);
                    Bomb_Select(0.3f);
                    Recovery_Select(0.3f);
                }
                if (Input.GetKeyDown("2")){
                    Gun_Select(0.3f);
                    Bomb_Select(1.0f);
                    Recovery_Select(0.3f);
                }
                break;

            default:
                break;
        }

    }


    public void Gun_Select(float Trans)
    {
        IS = ItemState.S_Gun;
            
        switch (Gun_flg)
        {
            case true:
                H_gun.color = new Color(1.0f, 1.0f, 1.0f, Trans);
                M_gun.color = new Color(0f, 0f, 0f, 0.0f);
                break;

            case false:
                H_gun.color = new Color(0f, 0f, 0f, 0.0f);
                M_gun.color = new Color(1.0f, 1.0f, 1.0f, Trans);
                break;

            default:
                break;
        }

    }

    public void Bomb_Select(float Trans)
    {
        IS = ItemState.S_Bom;

        Bomb.color = new Color(Bom_color, 1.0f, Bom_color, Trans);
    }

    public void Recovery_Select(float Trans)
    {
        IS = ItemState.S_Rec;

        Recovery.color = new Color(Rec_color, 1.0f, Rec_color, Trans);
    }
}

