using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵行動スクリプト(機能→状態遷移、動き、攻撃、当たり判定)
/// </summary>
public class A_normal_enemy_move : Photon.MonoBehaviour {

    protected float A_spd;                     //>敵のスピード(とりあえず現在は適当に数値をIN)>>少ないデータ型(求
    protected float A_rad;                     //>ラジアン(プレイヤー追尾にて使用)
    protected short A_hp_init = 100;           //>体力初期値 (16bit)->扱える数値(-32768~32767)             
    protected short A_hp;                      //>体力
    protected short A_power = 10;              //>攻撃力
    protected float A_magnitude;               //>二点間の距離(プレイヤーとの距離)
    protected float A_target_magnitude = 2f;   //>プレイヤーに対しての攻撃判定距離
    protected Animator A_anim;                 //>敵(ゾンビ)のアニメーター
    protected bool A_delay_flg = true;         //>ダメージ処理の抑制flg(多段HIT防止)
    protected GameObject A_Player;             //>プレイヤーオブジェクト
    protected N2_status A_P_info;              //>プレイヤーの情報
    public GameObject A_Bullet;                //>弾オブジェクト
    protected Bullet A_B_info;                 //>弾の情報
    public GameObject A_enemy;
    /// <summary>
    /// マップ外待機(アクティブfalse)、待機、視認、攻撃、hit、死亡
    /// </summary>
    protected enum  A_enemy_state
    {/*****使わないstateは後に消す******/
       // A_Stand_by, //>Stand-by    ->マップ外待機(アクティブfalse)
        A_oov,      //>Out-of-view ->待機
        A_vsb,      //>Visibility  ->視認(移動)
        A_atk,      //>attack      ->攻撃
        A_hit,      //>Hit         ->攻撃が当たった
        A_death,    //>Death       ->死亡
    }protected A_enemy_state A_state;
    

    //↑の列挙の状態のアニメーション情報
     protected string[] A_animator_state = { "play" , "run", "attack", "play", "dide" };//play2つの理由→ヒットモーションなし

	protected void Start()
    {
//        A_Player_Select();                             //>プレイヤーをランダムで参照（狙う）
		if (PhotonNetwork.player.IsMasterClient)
		TargetSelect();
			//IK追記
        A_anim = GetComponent<Animator>();
		A_P_info = A_Player.GetComponent<N2_status>();
        A_B_info = A_Bullet.GetComponent<Bullet>();
        A_hp = A_hp_init;                              //>体力初期化
        A_spd = Random.Range(0.04f, 0.1f);
        A_state = A_enemy_state.A_vsb;
        A_anim.SetBool("play", true);
    }
    // Update is called once per frame
    void Update()
    {
        A_Enemy_Move();//敵の動作(引数は移動の速さ)
    }


    /// <summary>
    /// 敵の動作関数→状態遷移も含む
    /// </summary>
    protected void A_Enemy_Move()
    {


        A_anim.SetBool(A_animator_state[(int)A_state], true);//>状態毎のアニメーション



        switch (A_state) {
		/*case A_enemy_state.A_Stand_by://マップ外の待機状態(リセット)
			A_hp = A_hp_init;
			break;
            */
		case A_enemy_state.A_oov://待機状態
             
			if (A_anim.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.15f) {

				/*攻撃に切り替える*/
				A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude;//>二点間の距離
				if (A_magnitude <= A_target_magnitude) {
                        A_anim.SetBool(A_animator_state[(int)A_state], false);
                        A_state = A_enemy_state.A_atk;//攻撃範囲内にプレイヤーがいたら攻撃実行
				} else {
                        A_anim.SetBool(A_animator_state[(int)A_state], false);
                        A_state = A_enemy_state.A_vsb; //>プレイヤーが攻撃範囲外なら追いかける
					
				}
				/****************/
			}
			break;
      
		case A_enemy_state.A_vsb://追いかけて来る状態(視認している)

                /*条件で攻撃(stateを攻撃に切り替え)*/
			A_magnitude = (transform.position - A_Player.transform.position).magnitude;//二点間の距離
			if (A_magnitude <= A_target_magnitude) {
                    A_anim.SetBool(A_animator_state[(int)A_state], false);
                    A_state = A_enemy_state.A_atk;//プレイヤーと自分の距離が一定範囲以内なら攻撃へ
			}
            else
                {
                    /******追尾*******/
                    transform.position = A_Unique_Move();
                    /*****************/
                }
                /********************************/
                break;

		case A_enemy_state.A_atk://攻撃
                /*攻撃判定処理*/
			A_Attack ();
                /*************/
			break;

		case A_enemy_state.A_hit://敵がダメージを受けた状態(撃たれるなど)
                
                /*ダメージ計算*/
			A_hp -= A_B_info.Pow;
                /************/
			if (A_hp <= 0) {//体力が0(以下)なら状態を死亡に遷移
                    A_anim.SetBool(A_animator_state[(int)A_state], false);
                    A_state = A_enemy_state.A_death;
                    
			} else {
                    /*ヒットモーション*/

                    /****************/
                    /*まだ体力が余っているプレイヤーを追いかける*/
                    A_anim.SetBool(A_animator_state[(int)A_state], false);
                    A_state = A_enemy_state.A_vsb;
			}
                /************/
			break;
            
		case A_enemy_state.A_death://死亡した状態(行動不能)		
                                   /* if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                                { //0～1(始発から終点)
                                       
                                        PhotonNetwork.Destroy(this.gameObject);//確認用
                                }
                             */
                Destroy(A_enemy, 2f);
                if (A_enemy == null)//オブジェクトがnullなら
                {
                    PhotonNetwork.Destroy(A_enemy);
                }
                break;
		}
        
    }
		

    /// <summary>
    /// 敵(ゾンビ)の行動
    /// </summary>
    /// <returns></returns>
    protected virtual Vector3 A_Unique_Move()
    {

        /***********************追尾処理**************************/
        A_rad = Mathf.Atan2(
               A_Player.transform.position.z - transform.position.z,
               A_Player.transform.position.x - transform.position.x);
        Vector3 A_Position = transform.position;

        A_Position.x += A_spd * Mathf.Cos(A_rad);
        A_Position.z += A_spd * Mathf.Sin(A_rad);
        /********************************************************/
        transform.rotation = new Quaternion(0, 0, A_Position.z, 0);
        transform.LookAt(A_Position); //移動方向を見る
        return A_Position;//移動値を返す
    }


    /// <summary>
    /// 敵の攻撃
    /// </summary>
     protected virtual void  A_Attack()
    {
        
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0) transform.LookAt(A_Player.transform.position);
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.54f)
        {
            
            /*攻撃判定*/
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude;//>二点間の距離
            if (A_delay_flg == true && A_magnitude <= A_target_magnitude)
            {
                /*プレイヤーダメージ処理*/
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する
		if(PhotonNetwork.player.IsMasterClient)//IK追記　ダメージ判定はホスト側で行う
                A_P_info.Damage(A_power);

                A_delay_flg = false;    //ダメージが入った時にflgをfalse
                /**********************/
            }
        }
        //アニメーションが終わったらflgをtrue
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            A_delay_flg = true;
            A_anim.SetBool(A_animator_state[(int)A_state], false);
            /*攻撃モーションが終わったのでstateを切り替える(待機へ)*/
            A_state = A_enemy_state.A_oov;
            /**************************************************/
        }

        
    }
    

    /// <summary>
    /// 弾との当たり判定
    /// </summary>
    /// <param name="col"></param>
    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("Bullet") && A_state != A_enemy_state.A_death)
        {
            A_anim.SetBool(A_animator_state[(int)A_state], false);
            
            GameObject.Destroy(col.gameObject);
            A_state = A_enemy_state.A_hit;
        }
    }
    

    /// <summary>
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

    }

/*
    /// <summary>
    /// プレイヤー参照（1P～4Pどれを狙うか選ぶ）
    /// </summary>
    protected virtual void A_Player_Select(sbyte player_num)
    {
        A_Player = GameObject.Find("Player" + player_num.ToString());//>1P～4Pをランダムで参照   
    }*/
	/// <summary>
	/// instanceがtrueになった時に呼ばれる
	/// </summary>

	[PunRPC]
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

	}
}