using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Boomer(ボマー->爆発)
/// </summary>
public class A_Boomer_move : A_Enemy_Base{

    public GameObject A_explosion;

    // Use this for initialization
    new void Start() {
		base.Start ();                                 //>Enemy_BaseのStart関数を呼び出す
        //A_enemy = gameObject;
        A_hp = A_hp_init = 50;                         //>ブマーのHP
        A_power = 30;                                  //>ブマーの攻撃力
        A_target_magnitude = 4f;                       //>ブマーの攻撃範囲
		A_spd = Random.Range(0.02f, 0.04f);            //>ブマーの速度
		A_anim.SetBool("Any State", true);             //>生成時のアニメーション
        A_Del_timer = 3;
        //アニメーションの変数変わっているため、調整

        A_animator_state = new[] { "Idle", "Run", "Attack", "Hit", "Die" };
        /////////////////////////////////////
    }
    // Update is called once per frame
    void Update()
    {
        A_Enemy_State();
        
    }
    /// <summary>
    /// 敵の攻撃(継承させる)
    /// </summary>
    protected override void A_Attack()
    {
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0) transform.LookAt(A_Player.transform.position);
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.39f)
        {
            Instantiate(A_explosion, A_enemy.transform.position, A_enemy.transform.rotation);
            Destroy(gameObject);//自爆後ブーマーはいなくなる
           
            //攻撃判定
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude - A_target_magnitude;//>二点間の距離
            if (A_delay_flg == false && A_magnitude <= A_target_magnitude+2)
            {
                //プレイヤーダメージ処理
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する
		        //if(PhotonNetwork.player.IsMasterClient)//IK追記　ダメージ判定はホスト側で行う
	        	A_P_info.Damage(A_power);
               
            }
        }

    }
}