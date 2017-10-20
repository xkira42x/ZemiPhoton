﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵ボマースクリプト
/// </summary>
public class A_Boomer_move : A_normal_enemy_move{

	// Use this for initialization
	void Start () {
        A_hp_init = 50;                                //>ブマーのHP
        A_power = 30;                                  //>ブマーの攻撃力
        A_target_magnitude = 3f;                       //>ブマーの攻撃範囲
        A_Player_Select();                             //>プレイヤーターゲットロックオン     
        A_anim = GetComponent<Animator>();
        A_P_info = A_Player.GetComponent<N2_status>();
        A_B_info = A_Bullet.GetComponent<Bullet>();
        A_hp = A_hp_init;                               //>体力初期化
        A_spd = Random.Range(0.02f, 0.04f);             //>ブマーの速度
        A_state = A_enemy_state.A_vsb;
        A_anim.SetBool("play", true);
    }
	
	// Update is called once per frame
	void Update () {
        A_Enemy_Move();
    }
    /// <summary>
    /// 敵の攻撃(継承させる)
    /// </summary>
    protected override void A_Attack()
    {
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0) transform.LookAt(A_Player.transform.position);
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.54f)
        {
            Destroy(gameObject);//自爆後ブーマーはいなくなる
            //攻撃判定
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude - A_target_magnitude;//>二点間の距離
            if (A_delay_flg == true && A_magnitude <= A_target_magnitude)
            {
                //プレイヤーダメージ処理
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する
                //A_P_info.Damage(A_power);
                Debug.Log("HIT!!");
               
                A_delay_flg = false;    //ダメージが入った時にflgをfalse
                //----------------------//
            }
        }

    }

}