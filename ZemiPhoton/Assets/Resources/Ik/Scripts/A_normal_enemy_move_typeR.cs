using UnityEngine;
using System.Collections;

/// <summary>
/// 敵行動スクリプト(機能→状態遷移、動き、攻撃、当たり判定)
/// </summary>
public class A_normal_enemy_move_typeR : Photon.MonoBehaviour {
 
    protected float A_spd = 0.05f;             //>敵のスピード(とりあえず現在は適当に数値をIN)>>少ないデータ型(求
    protected float A_rad;                     //>ラジアン(プレイヤー追尾にて使用)
    protected short A_hp_init = 100;           //>体力初期値 (16bit)->扱える数値(-32768~32767)             
    protected short A_hp;                      //>体力
    protected sbyte A_power = 20;              //>攻撃力(8bit)->扱える数値(0~255)
    protected float A_magnitude;               //>二点間の距離(プレイヤーとの距離)
    protected float A_target_magnitude = 2f;    //>プレイヤーに対しての攻撃判定距離
    protected Animator A_anim;                 //>敵(ゾンビ)のアニメーター
    public bool A_delay_flg = true;            //>ダメージ処理の抑制flg(多段HIT防止)

    public GameObject A_Player;                //>プレイヤーオブジェクト
    int A_player_target;                       //>どのプレイヤーをターゲットするか
　  N2_Status_typeR A_P_info;                    //>プレイヤーの情報
    public GameObject A_Bullet;
    Bullet A_B_info;                           //>弾の情報

	Target TG;//愛敬追記
//	string endStateName = null;


    /// <summary>
    /// マップ外待機(アクティブfalse)、待機、視認、攻撃、hit、死亡
    /// </summary>
    protected enum A_enemy_state
    {/*****使わないstateは後に消す******/
        A_Stand_by, //Stand-by    ->マップ外待機(アクティブfalse)
        A_oov,      //Out-of-view ->待機
        A_vsb,      //Visibility  ->視認(移動)
        A_atk,      //attack      ->攻撃
        A_hit,      //Hit         ->攻撃が当たった
        A_death     //Death       ->死亡
    } A_enemy_state A_state;

    void Awake()
	{
		if (PhotonNetwork.isMasterClient) {
			int InRoomsPlayer = PhotonNetwork.countOfPlayersInRooms+2;
			Debug.Log ("pls:" + InRoomsPlayer);
			//部屋内のプレイヤー数を最大に数字を抽出
			if(A_player_target!=InRoomsPlayer)
			A_player_target = Random.Range (1, InRoomsPlayer);
			Debug.Log ("tag:" + A_player_target);

			photonView.RPC ("TargetSet", PhotonTargets.AllBuffered, A_player_target);
		}

		A_anim = GetComponent<Animator> ();
		if (A_Player == null)
			Debug.Log ("プレイヤーが見つかっていません");
		A_P_info = A_Player.GetComponent<N2_Status_typeR> ();
		A_B_info = A_Bullet.GetComponent<Bullet> ();
		A_anim.SetBool ("play", true);
		A_hp = A_hp_init;                               //>体力初期化
		A_state = A_enemy_state.A_vsb;
		if (PhotonNetwork.isMasterClient)
		StartCoroutine ("Target");
	}
    // Update is called once per frame
    void Update()
    {
        A_Enemy_Move(A_spd);//敵の動作(引数は移動の速さ)
    }


    /// <summary>
    /// 敵の動作関数(引数は速度(A_spd))→状態遷移も含む
    /// </summary>
    /// <param name="A_spd"></param>
    protected void A_Enemy_Move(float A_spd)
    {

		switch (A_state) {
		case A_enemy_state.A_Stand_by://マップ外の待機状態(リセット)
			A_hp = A_hp_init;
			break;

		case A_enemy_state.A_oov://待機状態
                /*待機モーション*/
			A_anim.SetBool ("play", true);
                /***************/
			if (A_anim.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.2f) {

				/*攻撃に切り替える*/
				A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude - 2;//>二点間の距離
				if (A_magnitude <= A_target_magnitude) {
					A_anim.SetBool ("play", false);
					A_state = A_enemy_state.A_atk;//攻撃範囲内にプレイヤーがいたら攻撃実行
				} else {
					A_state = A_enemy_state.A_vsb; //>プレイヤーが攻撃範囲外なら追いかける
					A_anim.SetBool ("attack", false);
				}
				/****************/
			}
			break;
      
		case A_enemy_state.A_vsb://追いかけて来る状態(視認している)

                /*移動のモーション*/
			A_anim.SetBool ("run", true);
                /****************/

                /******追尾*******/
			transform.position = A_Unique_Move ();
                /*****************/

                /*条件で攻撃(stateを攻撃に切り替え)*/
			A_magnitude = (transform.position - A_Player.transform.position).magnitude;//二点間の距離
			if (A_magnitude <= A_target_magnitude) {
				A_anim.SetBool ("run", false);
				A_state = A_enemy_state.A_atk;//プレイヤーと自分の距離が一定範囲以内なら攻撃へ
			}
                /********************************/
			break;

		case A_enemy_state.A_atk://攻撃
			A_anim.SetBool ("run", false);
			A_anim.SetBool ("play", false);
                /*攻撃モーション*/
			A_anim.SetBool ("attack", true);
                /**************/

                /*攻撃判定処理*/
			A_Attack ();
                /*************/
			break;

		case A_enemy_state.A_hit://敵がダメージを受けた状態(撃たれるなど)
                
                /*ダメージ計算*/
			A_hp -= A_B_info.Pow;
                /************/
			if (A_hp <= 0) {//体力が0(以下)なら状態を死亡に遷移
				A_state = A_enemy_state.A_death;
			} else {
				/*ヒットモーション*/

				/****************/
				/*まだ体力が余っているプレイヤーを追いかける*/
				A_state = A_enemy_state.A_vsb;
			}
                /************/
			break;
            
		case A_enemy_state.A_death://死亡した状態(行動不能)		
			//A_anim.SetBool ("run", false);
			//A_anim.SetBool ("attack", false);
			//A_anim.SetBool ("play", false);
			/***************/
			if (A_anim.GetBool ("dide")) {
				if (A_anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1.0f) { //0～1(始発から終点)
					/*初期ポジションに戻る*/
					/********************/
				}else PhotonNetwork.Destroy (this.gameObject);//確認用
			}

                /*死亡モーション*/
			A_anim.SetBool ("dide", true);
			break;
		}
    }
		
    /// <summary>
    /// 敵(ゾンビ)の行動⇒継承後にオーバーライド
    /// </summary>
    /// <returns></returns>
    protected Vector3 A_Unique_Move()
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
    /// 敵の攻撃(継承させる)
    /// </summary>
    protected void A_Attack()
    {
        A_anim.SetBool("run", false);
        A_anim.SetBool("play", false);

        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0) transform.LookAt(A_Player.transform.position);
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.54f)
        {
            
            /*攻撃判定*/
            A_magnitude = (transform.position - A_Player.transform.position).sqrMagnitude - 2.0f;//>二点間の距離
            if (A_delay_flg == true && A_magnitude <= A_target_magnitude)
            {
                /*プレイヤーダメージ処理*/
                //A_P_info -= A_power; //プレイヤーのHPに自分の攻撃分減算する

				photonView.RPC("Dmg",PhotonTargets.All);
				Debug.Log ("HP:" + A_P_info.Hp+"_"+Time.time);
                A_delay_flg = false;    //ダメージが入った時にflgをfalse
                /**********************/
            }
        }
        //アニメーションが終わったらflgをtrue
        if (A_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            A_delay_flg = true;
            A_anim.SetBool("attack", false);
            /*攻撃モーションが終わったのでstateを切り替える(待機へ)*/
            A_state = A_enemy_state.A_oov;
            /**************************************************/
        }

        
    }
	[PunRPC]
	void Dmg(){
		A_P_info.Damage(A_power);
	}

    /// <summary>
    /// 弾との当たり判定
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("Bullet"))
        {
            GameObject.Destroy(col.gameObject);
            A_state = A_enemy_state.A_hit;
        }
    }

    /// <summary>
    /// instanceがtrueになった時に呼ばれる
    /// </summary>

	[PunRPC]
	void TargetSet(int ss){
		//数字に応じたプレイヤーをターゲットに代入
		A_Player = GameObject.Find ("Player" + ss);

		if (A_Player == null) {
			Debug.Log ("プレイヤーがみつかりません");
		}
		A_P_info = A_Player.GetComponent<N2_Status_typeR> ();

	}
	public string TargetGet(){
		return A_Player.transform.name;
	}
	IEnumerator Target(){
		while(true){
			PhotonNetwork.playerList.Length;
			int InRoomsPlayer = PhotonNetwork.countOfPlayersInRooms +2;
			Debug.Log ("pls:" + InRoomsPlayer);

		//部屋内のプレイヤー数を最大に数字を抽出
		A_player_target = Random.Range (1, InRoomsPlayer);

		photonView.RPC ("TargetSet",PhotonTargets.AllBuffered,A_player_target);
		Debug.Log ("Tag:" + A_player_target);


		yield return new WaitForSeconds (5f);
		}
	}
}