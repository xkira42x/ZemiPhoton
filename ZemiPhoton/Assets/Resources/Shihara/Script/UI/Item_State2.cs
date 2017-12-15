using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Item_State2 : MonoBehaviour
{
    public Text Bullet;
    public Image Gun_bar;

    public Image H_gun;
    public Image M_gun;

    public Image Bomb;
    public Image Recovery;


    public static byte Gun_flg = 1;
    public static float Bom_color = 1.0f;
    public static float Rec_color = 1.0f;

    float Gun_sel = 1.0f;
    float Bom_sel = 0.3f;
    float Rec_sel = 0.3f;

    void Start()
    {
        Gun_Select();
        Bomb_Select();
        Recovery_Select();

    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Gun_sel = 1.0f;
            Gun_Select();
            Bom_sel = 0.3f;
            Bomb_Select();
            Rec_sel = 0.3f;
            Recovery_Select();
        }
        if (Input.GetKeyDown("2"))
        {
            Gun_sel = 0.3f;
            Gun_Select();
            Bom_sel = 1.0f;
            Bomb_Select();
            Rec_sel = 0.3f;
            Recovery_Select();
        }
        if (Input.GetKeyDown("3"))
        {
            Gun_sel = 0.3f;
            Gun_Select();
            Bom_sel = 0.3f;
            Bomb_Select();
            Rec_sel = 1.0f;
            Recovery_Select();
        }

        //弾の円ゲージ（＝　残りの弾数　/　弾の最大所持数）
        //Gun_bar.fillAmount = Camera_Spot.tama / Camera_Spot.tama_max;

        //残り弾数表示テキスト
        //Bullet.text = "" + Camera_Spot.tama.ToString();
    }


    public void Gun_Select()
    {
        switch (Gun_flg)
        {
            case 1:
                H_gun.color = new Color(1.0f, 1.0f, 1.0f, Gun_sel);
                M_gun.color = new Color(0f, 0f, 0f, 0.0f);
                Gun_bar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Bullet.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;

            case 2:
                H_gun.color = new Color(0f, 0f, 0f, 0.0f);
                M_gun.color = new Color(1.0f, 1.0f, 1.0f, Gun_sel);
                Gun_bar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Bullet.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;

            default:
                H_gun.color = new Color(0f, 0f, 0f, 0.0f);
                M_gun.color = new Color(0f, 0f, 0f, 0.0f);
                Gun_bar.color = new Color(0f, 0f, 0f, 0.0f);
                Bullet.color = new Color(0f, 0f, 0f, 0.0f);
                break;
        }
    }

    public void Bomb_Select()
    {
        Bomb.color = new Color(Bom_color, 1.0f, Bom_color, Bom_sel);
    }
    public void Recovery_Select()
    {
        Recovery.color = new Color(Rec_color, 1.0f, Rec_color, Rec_sel);
    }
}


