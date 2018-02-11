﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerList : Photon.MonoBehaviour {

	public static List<GameObject> Player = new List<GameObject>();		// プレイヤーリスト
    public static List<S1_Move> Status = new List<S1_Move>();           // Moveソースリスト
	static int _length = 0;											    // リストの長さ
	public static int length{ get { return _length; } set { _length = value; } }

	/// リストに追加する
	/// 受け取った名前で検索して、リストに追加する
	public static void AddPlayerList(string name){

		GameObject obj = null;

		// Playerタグの付いたオブジェクトを探索
		foreach (GameObject ooo in GameObject.FindGameObjectsWithTag("Player")) {
			if (ooo.GetComponent<S2_Status> ().UserName == name) {
				obj = ooo;
				break;
			}
		}

		// オブジェクトが見つかればリストに追加する
		if (obj != null)
        {
            Player.Add(obj);
        }

        _length = Player.Count;

		for (int i = 0; i < _length; i++)
			Debug.Log ("Show.. Add player id : " + i + " name : " + Player [i].name);
	}

    /// リストに追加する
    /// 受け取った名前で検索して、リストに追加する
    public static void AddPlayerList(GameObject obj){
		
		// オブジェクトをリストに追加する
		Player.Add (obj);
        Status.Add(obj.GetComponent<S1_Move>());

		_length = Player.Count;

		for (int i = 0; i < _length; i++)
        {
            Debug.Log("Show.. Add player id : " + i + " name : " + Player[i].name + " status : " + Status[i].Status);
        }
	}

    /// ランダムにリストからオブジェクトを取得
    public static GameObject GetPlayerList_Shuffle(){
		int index = Random.Range (0, _length);
		return Player [index];
	}

	/// リスト番号からオブジェクトを取得
	public static GameObject GetPlayerList(int index){
		if (0 > index || index > _length)
			return null;
		else
			return Player [index];
	}

	// 名前からオブジェクトを取得
	public static GameObject GetPlayerList(string name){
		foreach (GameObject obj in Player) {
			if (obj.GetComponent<S2_Status> ().UserName == name)
				return obj;
		}
		return null;
	}

	/// ランダムにリストから座標を取得
	public static Vector3 GetPlayerPosition_Shuffle(){
		int index = Random.Range (0, _length);
		return Player [index].transform.position;
	}

	/// リスト番号から座標を取得
	public static Vector3 GetPlayerPosition(int index){
        if (0 > index || index > _length)
            return Player [0].transform.position;
        else
            return Player[index].transform.position;
	}

    /// プレイヤの死亡判定
    public static bool isDied(int index)
    {
        if (0 > index || index > _length)
            return true;
        else return Status[index].Status == S1_Move.DIE;
    }

    /// プレイヤ全員の死亡判定
    public static bool isDiedAll()
    {
        //Debug.Log("PlayerList length =>" + _length);
        for (int i = 0; i < _length; i++)
        {
            //Debug.Log("Player Status => " + Status[i].Status);
            if (Status[i].Status != S1_Move.DIE) return false;
        }
        return true;
    }

    /// リストの情報を全開放
    public static void ReleaseAll()
    {
        Player.Clear();
        Status.Clear();
    }
	/// 指定したリストの情報を開放
	public static void ReleaseAt(int index){Player.RemoveAt (index);}

    public static bool Check(int index)
    {
        if (0 > index || index > _length)
            return false;
        else
            return true;
    }

}
