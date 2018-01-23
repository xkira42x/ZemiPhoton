using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵通常感染者
/// </summary>
public class A_normal_enemy_move1 : A_Enemy_Base {

    new void Start()
    {
        base.Start();
        //A_enemy = gameObject;
        A_hp = A_hp_init = 100;                        //>通常感染者のHP
        A_power = 10;                                  //>通常感染者の攻撃力
        A_target_magnitude = 2f;                       //>通常感染者の攻撃範囲
        A_spd = Random.Range(0.08f, 0.1f);             //>通常感染者の速度
        A_anim.SetBool("Any State", true);             //>生成時のアニメーション
        //アニメーション更新(元→A_Enemy_Base)
        A_animator_state = new[] { "Idle", "Run", "Attack", "Hit", "Die" };
        A_Del_timer = 2;
    }


    // Update is called once per frame
    void Update()
    {
        A_Enemy_State();//Stategy(アルゴリズムの集合体->)
    }


    //@override->独自の攻撃パターン
    protected override void A_Attack()
    {
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
            transform.LookAt(A_Player.transform.position);
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.54f)
        {
       
            //攻撃判定
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude;//>二点間の距離
            if (A_delay_flg == false && A_magnitude <= A_target_magnitude)
            {
                //プレイヤーダメージ処理
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する
                //if(PhotonNetwork.player.IsMasterClient)//IK追記　ダメージ判定はホスト側で行う
                A_P_info.Damage(A_power);
            }
            A_delay_flg = true;    //攻撃時にflgをtrue
        }

        if (A_delay_flg == true && A_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            //アニメーションが終わったらflgをfalse     
            A_delay_flg = false;//攻撃モーション終了時にflgをtrue
            A_anim.SetBool(A_animator_state[(int)A_state], false);//攻撃モーション終了
            //攻撃モーションが終わったのでstateを切り替える(待機へ)
            Debug.Log("遷移 " + name);
            A_state = A_enemy_state.A_oov;
            return;
        }

    }





    /* /// <summary>
     /// プレイヤー参照（1P～4Pランダムで狙う）
     /// </summary>
     [PunRPC]
     protected virtual void A_Player_Select()
     {
         do
         {
             A_Player = GameObject.Find("Player" + Random.Range(1, PhotonNetwork.playerList.Length+1).ToString());//>1P～4Pをランダムで参照
         } while (A_Player == null);//>プレイヤーが居たら抜け出す
         A_P_info = A_Player.GetComponent<N2_status> ();

     }*/
    /*[PunRPC]
	protected void TargetSet(int ss){
		//数字に応じたプレイヤーをターゲットに代入
		A_Player = GameObject.Find ("Player" + ss);

		if (A_Player == null) {
			Debug.Log ("プレイヤーがみつかりません");
		}
		A_P_info = A_Player.GetComponent<N2_status> ();

		Debug.Log ("AP:"+A_Player);
	}
	public string TargetGet(){
		return A_Player.transform.name;
	}
	protected virtual void TargetSelect(){

			photonView.RPC ("TargetSet",PhotonTargets.AllBuffered, Random.Range (1, PhotonNetwork.playerList.Length+1));

	}*/
}