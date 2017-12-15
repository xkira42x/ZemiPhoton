using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵のデータのベースクラス(オフライン上の変数とムーブに関する関数とHIT判定を管理)
/// 初期化対象→「A_enemy」「A_hp_init」「A_hp; A_power」「A_target_magnitude」 「A_Del_timer」
/// </summary>
public class A_Enemy_Base : Photon.MonoBehaviour {
    //宣言
    protected float A_spd;                     //>敵のスピード(とりあえず現在は適当に数値をIN)>>少ないデータ型(求
    protected float A_rad;                     //>ラジアン(プレイヤー追尾にて使用)
    protected short A_hp_init;                 //>初期化必須->>>>>体力初期値 (16bit)->扱える数値(-32768~32767)             
    protected short A_hp;                      //>初期化必須->>>>>体力
    protected short A_power;                   //>初期化必須->>>>>攻撃力
    protected float A_magnitude;               //>二点間の距離(プレイヤーとの距離)
    protected float A_target_magnitude;        //>初期化必須->>>>>プレイヤーに対しての攻撃判定距離
    protected Animator A_anim;                 //>敵(ゾンビ)のアニメーター
    protected bool A_delay_flg;         //>ダメージ処理の抑制flg(多段HIT防止)
    protected GameObject A_Player;             //>プレイヤーオブジェクト
    protected S2_Status A_P_info;              //>プレイヤーの情報
    GameObject A_Bullet;                       //>弾オブジェクト
    protected Bullet A_B_info;                 //>弾の情報
    protected GameObject A_enemy;              //>継承先の敵オブジェクトをIN
    Rigidbody A_rd;
    //死亡制御Timer
    bool once = false;
    protected float A_Del_timer;

    /// <summary>
    /// 敵の状態遷移まとめ->変数名A_state
    /// </summary>
    protected enum A_enemy_state
    {//使わないstateは後に消す
     // A_Stand_by, //>Stand-by    ->マップ外待機(アクティブfalse)
        A_oov,      //>Out-of-view ->待機
        A_vsb,      //>Visibility  ->移動
        A_atk,      //>Attack      ->攻撃
        A_hit,      //>Hit         ->ヒット
        A_death,    //>Death       ->死亡
    }protected A_enemy_state A_state;


    //↑の列挙の状態のアニメーション情報
    //アニメーション割り振り->A_enemy_stateの番号に合ったアニメーションを順に入れる
    protected string[] A_animator_state;

    /// <summary>
    /// 継承先に『base.Start();』でA_Enemy_BaseのStart()を呼び出す。
    /// </summary>
    protected void Start()
    {
        A_rd = GetComponent<Rigidbody>();
        A_enemy = gameObject;
        A_Player_Select(1);//1P狙い(デバッグ用)
        //A_Player_Select();                           //>プレイヤーをランダムで参照（狙う）
        /*if (PhotonNetwork.player.IsMasterClient)
        TargetSelect();*/
        //IK追記
        A_delay_flg = false;
        once = false;
        A_anim = this.GetComponent<Animator>();
        A_P_info = A_Player.GetComponent<S2_Status>();
        //A_B_info = A_Bullet.GetComponent<Bullet>();
        A_state = A_enemy_state.A_vsb;
    }



    //関数

    /// <summary>
    /// 待機処理
    /// </summary>
    protected virtual void A_OutOfView()
    {
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.15f)
        {
            //プレイヤーが攻撃範囲内なら攻撃に切り替える
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude;//>二点間の距離
            if (A_magnitude <= A_target_magnitude)
            {
                A_anim.SetBool(A_animator_state[(int)A_state], false);
                A_state = A_enemy_state.A_atk;//攻撃範囲内にプレイヤーがいたら攻撃実行

            }
            //>プレイヤーが攻撃範囲外なら追いかける
            else
            {
                A_anim.SetBool(A_animator_state[(int)A_state], false);
                A_state = A_enemy_state.A_vsb; 
            }
        }
    }
    /// <summary>
    /// 追尾処理------->>>実装後にナビ追加-------->>>敵の通信クラスから座標同期の関数呼び出し
    /// </summary>
    protected virtual void A_Visibility()
    {
        //プレイヤーが攻撃範囲内なら攻撃に切り替える
        A_magnitude = (transform.position - A_Player.transform.position).magnitude;//二点間の距離
        if (A_magnitude <= A_target_magnitude)
        {
            A_anim.SetBool(A_animator_state[(int)A_state], false);
            A_state = A_enemy_state.A_atk;//プレイヤーと自分の距離が一定範囲以内なら攻撃へ
        }
        //プレイヤーが攻撃範囲外なら走り続ける
        else if(A_magnitude > A_target_magnitude)
        {
            transform.position = A_Unique_Move();
        }
        
    }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    protected virtual void A_Attack() { }
    /// <summary>
    /// ダメージ処理
    /// </summary>
    protected virtual void A_Hit()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (A_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") == false) A_state = A_enemy_state.A_vsb;
    }
    /// <summary>
    /// 死亡処理-------->>>敵の通信クラスからEnemy_PhotonのDestroy関数を呼び出す
    /// </summary>
    protected virtual void A_Death()
    {
        if (once) {
            //敵の通信クラスからPhotonのDestroy関数を呼び出す

            PhotonNetwork.Destroy(A_enemy);
        }
        else
        {
            A_Del_timer -= Time.deltaTime;
            if (A_Del_timer <= 0)
                once = true;
        }
    }


    /// <summary>
    /// 状態遷移(アニメーション付き)
    /// </summary>
    protected void A_Enemy_State()
    {
        A_anim.SetBool(A_animator_state[(int)A_state], true);//>状態毎のアニメーション

        switch (A_state)
        {
            case A_enemy_state.A_oov://待機状態
                A_OutOfView();
                break;

            case A_enemy_state.A_vsb://追いかけて来る状態(視認している)
                A_Visibility();
                break;

            case A_enemy_state.A_atk://攻撃
                
                A_Attack();
                break;

            case A_enemy_state.A_hit://敵がダメージを受けた状態(撃たれるなど)
                A_Hit();
                break;

            case A_enemy_state.A_death://死亡した状態(行動不能)		
                A_Death();
                break;
        }
    }


    /// <summary>
    /// プレイヤー参照（1P～4Pどれを狙うか選ぶ）
    /// </summary>
    protected virtual void A_Player_Select(sbyte player_num)
    {
        A_Player = GameObject.Find("FPSPlayer(Clone)");//仮組み→テスト
        //A_Player = GameObject.Find("Player" + player_num.ToString());   
    }

  
    /// <summary>
    /// 弾との当たり判定
    /// </summary>
    /// <param name="col"></param>
    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet" && A_state != A_enemy_state.A_death)
        {
            
            A_Bullet = col.gameObject;
            A_B_info = A_Bullet.GetComponent<Bullet>();
            A_anim.SetBool(A_animator_state[(int)A_state], false);
            // GameObject.Destroy(col.gameObject);
            


            //ダメージ計算
            A_hp -= A_B_info.Pow;


            if (A_hp <= 0)//体力が0(以下)なら状態を死亡に遷移
            {
                A_state = A_enemy_state.A_death;
            }
            else
            {
                //まだ体力が余っているならHITモーション
                A_state = A_enemy_state.A_hit;
                
            }

        }
    }
    
    /////////////////////ナビに変えるー＞テスト/////////////////////////////////////
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
}