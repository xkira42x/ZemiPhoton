using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵ボマースクリプト
/// </summary>
public class A_Boomer_move : A_normal_enemy_move{

    public GameObject A_explosion;

    // Use this for initialization
    void Start() {
		base.Start ();
        A_hp_init = 50;                                //>ブマーのHP
        A_power = 30;                                  //>ブマーの攻撃力
        A_target_magnitude = 4f;                       //>ブマーの攻撃範囲
//        A_Player_Select();                             //>プレイヤーターゲットロックオン     
		/*if (PhotonNetwork.player.IsMasterClient)
			photonView.RPC ("TargetSet",PhotonTargets.AllBuffered,Random.Range (1, PhotonNetwork.playerList.Length+1));
		//IK追記
       /* A_anim = GetComponent<Animator>();
        A_P_info = A_Player.GetComponent<N2_status>();
        A_B_info = A_Bullet.GetComponent<Bullet>();
        A_hp = A_hp_init;                               //>体力初期化
        A_state = A_enemy_state.A_vsb;
        */
        ////////////////////////////////
		A_spd = Random.Range(0.02f, 0.04f);             //>ブマーの速度
		A_anim.SetBool("Walking", true);

        //アニメーションの変数変わっているため、調整

        // string[] A_animator_state = { "Walking", "Walking", "Being Strangled", "Shove Reaction", "Dying" };//play2つの理由→ヒットモーションなし
        A_animator_state = new[]{ "Walking", "Walking", "Being Strangled", "Shove Reaction", "Dying" };//play2つの理由→ヒットモーションなし





        /////////////////////////////////////
	}
    // Update is called once per frame
    void Update()
    {
        A_Enemy_Move();
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
            if (A_delay_flg == true && A_magnitude <= A_target_magnitude)
            {
                //プレイヤーダメージ処理
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する
		if(PhotonNetwork.player.IsMasterClient)//IK追記　ダメージ判定はホスト側で行う
		A_P_info.Damage(A_power);
                A_delay_flg = false;    //ダメージが入った時にflgをfalse
                //----------------------//
            }
        }

    }
}