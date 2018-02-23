﻿using UnityEngine;
using UnityEngine.UI;

public class S2_Status : Photon.MonoBehaviour
{

    string userName = "Default";    // ユーザ名
    public string UserName { get { return userName; } set { userName = value; } }

    S1_Move move;                   // 移動
	[SerializeField]
    PlayerStatusUI statusUI;        // ステータス表示UI
    public PlayerStatusUI StatusUI { get { return statusUI; } set { statusUI = value; } }

    [SerializeField]
    float health = 100;             // ヒットポイント(0～100まで)
    public float Health { get { return health; } set { health = value; } }

    public Text Name;

    //[SerializeField]
    //bool damage = false;

    /// 体力を引数分減らし、HPのUIゲージ更新と同期を行う
    public void Damage(float dmg)
    {
        if (photonView.isMine && !PlayerInfo.isDied)
        {
            health -= dmg;
            statusUI.Health = health;
            photonView.RPC("SyncHP", PhotonTargets.Others, health);
            // 死亡処理
            if (health <= 0)
                move.Died();
        }
    }

    void Start()
    {
        // 操作しているキャラクタのステータスは必ず、右端のステータスに表示する。
        // それ以外は、右から順に設定する
        if (statusUI==null)
        {
            if (photonView.isMine)
            {
                statusUI = GameObject.Find("PlayerStatusUI0").GetComponent<PlayerStatusUI>();
                //photonView.RPC("SyncPlayerID", PhotonTargets.AllBuffered, PlayerInfo.playerNumber + 1);
            }
            else
            {
                statusUI = GameObject.Find("PlayerStatusUI" + (PlayerInfo.statusCount).ToString()).GetComponent<PlayerStatusUI>();
                PlayerInfo.statusCount++;
            }
            statusUI.UserName = userName;
        }
    }

    /// ステータス表示するオブジェクトを設定する
    void Awake()
    {
		// プレイヤーリストに追加する
        photonView.RPC("SyncPlayerList", PhotonTargets.AllBufferedViaServer);
        		
        move = GetComponent<S1_Move>();
    }

    //void Update() { if (damage) { Damage(1); damage = false; } }
   
    /// ユーザ名の同期(初期呼び出し)
    [PunRPC]
    void SetName(string name)
    {
        userName = name;
        Name.text = name;
    }

    /// ユーザIDからキャラクタの名前IDを設定する(初期呼び出し)
    /*[PunRPC]
    void SyncPlayerID(int id)
    {
        //		string name = statusUI.UserName;
        //		string name = "Player" + id.ToString ();
        //		userName = name;
        //gameObject.name = name;
    }*/

    /// ヒットポイントの同期して、HPのUIゲージも更新する
    [PunRPC]
    void SyncHP(float hp)
    {
        statusUI.Health = health = hp;
    }

    [PunRPC]
    void SyncPlayerList()
    {
        PlayerList.AddPlayerList(gameObject);
    }

    /// 情報の退避所を探す
    [PunRPC]
    void SyncFindEvacuationPlace()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("EvacuationPlace");
        for (int ii = 0; ii < objects.Length; ii++)
        {
            if (userName == objects[ii].name)
            {
				DisconObjSetting status = objects[ii].GetComponent<DisconObjSetting>();

				statusUI = status.statusUI;
                health = status.health;
                statusUI.UserName = userName;
            }
        }
    }
}
