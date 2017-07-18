using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 敵行動スクリプト(機能→状態遷移、動き、攻撃、当たり判定)
/// </summary>
public class A_normal_enemy_move : MonoBehaviour {
 
    protected float A_spd = 0.01f;         //>敵のスピード(とりあえず現在は適当に数値をIN)>>少ないデータ型(求
    protected float A_rad;                 //>ラジアン(プレイヤー追尾にて使用)
    protected short A_hp_init = 100;       //>体力初期値 (16bitの変数)             
    protected short A_hp;                  //>体力(16bitの変数)
    public Transform Player;               //>プレイヤーの座標(inspectorにプレイヤーを突っ込む)
    protected float A_distance;            //>二点間の距離(プレイヤーとの距離)
    protected float A_target_distance = 2; //>プレイヤーに対しての攻撃判定距離

    /// <summary>
    /// 0 = 待機(アクティブfalse)、1 = 待機、2 = 視認、3 = 攻撃、4 = hit、5 = 見失う、6 = 死亡
    /// </summary>
    protected enum A_enemy_state
    {/*****使わないstateは後に消す******/
        A_Stand_by, //Stand-by    -> 0 = 待機(アクティブfalse)
        A_oov,      //Out-of-view -> 1 = 待機
        A_vsb,      //Visibility  -> 2 = 視認(移動)
        A_atk,      //attack      -> 3 = 攻撃
        A_hit,      //Hit         -> 4 = 攻撃が当たった
        A_lost,     //Lost        -> 5 = 見失う
        A_death     //Death       -> 6 = 死亡
    } A_enemy_state A_state;

    void Awake()
    {
        A_hp = A_hp_init;                //>体力初期化
        A_state =  A_enemy_state.A_vsb;  //>デバッグ用
        
    }
    // Update is called once per frame
    void Update()
    {
        A_Enemy_Move(A_spd);//敵が動く
    }


    /// <summary>
    /// 敵の移動関数(引数は速度(A_spd))→状態遷移も含む
    /// </summary>
    /// <param name="A_spd"></param>
    protected void A_Enemy_Move(float A_spd)
    {
        /*ここのタイミングで同期良さそう*/
        switch (A_state)
        {
            case A_enemy_state.A_Stand_by://待機状態
                A_hp = A_hp_init;
                break;
            case A_enemy_state.A_oov://stay状態
                /*待機モーション*/
                Debug.Log("AAAAAAAAAASSSSSSSSSSSS");
                /***************/


                /*攻撃に切り替える*/
                A_distance = (transform.position - Player.transform.position).magnitude;//>二点間の距離

                if (A_distance <= A_target_distance) A_state = A_enemy_state.A_atk;
                else {
                    A_state = A_enemy_state.A_vsb;
                }
                /****************/
                break;

            case A_enemy_state.A_atk://攻撃
                A_Attack();

                /*stateを切り替える(待機へ)*/
                A_state = A_enemy_state.A_oov;
                /***********************/
                break;

            case A_enemy_state.A_vsb://move状態(視認している)

                /*移動のモーション*/

                /****************/

                /*追尾*/
                transform.position = A_Unique_Move();
                /*****/


                /*条件で攻撃(stateを攻撃に切り替え)*/

                /********************************/


                /*条件でstateを切り替える(攻撃以外)*/

                /***********************/
                break;
            case A_enemy_state.A_hit://敵がダメージを受けた状態(撃たれるなど)
                /*ヒットモーション*/

                /****************/

                /*ダメージ計算*/

                /*************/
                break;
            case A_enemy_state.A_lost://プレイヤーを見失う状態(一定範囲)
                /*見失ったときにする処理(案は今のところ無し)*/

                /****************************************/
                break;
            case A_enemy_state.A_death://死亡した状態(行動不能)
                /*死亡モーション*/

                /***************/

                /*敵死亡時のプレイヤーへの恩恵(キル数など)*/

                /************************************/

                /*初期ポジションに戻る*/

                /********************/
                break;
        }
    }

    /// <summary>
    /// 敵(ゾンビ)の行動⇒継承後にオーバーライド
    /// </summary>
    /// <returns></returns>
    protected Vector3 A_Unique_Move()
    {

        /*if(目の前に壁はあるか否か){
        壁を避ける(navi mesh使う説)
        }
        */
        transform.LookAt(Player); //>常にプレイヤーを見ている
      　/***********************追尾処理**************************/
            A_rad = Mathf.Atan2(
            Player.transform.position.z - transform.position.z,
            Player.transform.position.x - transform.position.x);
        Vector3 Position = transform.position;

        Position.x += A_spd * Mathf.Cos(A_rad);
        Position.z += A_spd * Mathf.Sin(A_rad);
        /*******************************************************/


        A_distance = (transform.position - Player.transform.position).magnitude;//>二点間の距離
        //↓プレイヤーと自分の距離が一定範囲以内なら攻撃へ
        if (A_distance <= A_target_distance) A_state = A_enemy_state.A_atk;
        

        return Position;//移動値を返す
    }


    /// <summary>
    /// 敵の攻撃(継承でオーバーライドする)
    /// </summary>
    protected void A_Attack()
    {
        
        /*攻撃*/
    }

    
}