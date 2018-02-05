﻿using UnityEngine;
using System.Collections;

    /// <summary>
    /// 外部からリザルトデータをもらうクラス
    /// </summary>
    public class Result_Date : MonoBehaviour
    {
        int player_Num;//プレイヤーのナンバー
        public struct Result_Stock
        {
            public string name_stock;
            public bool sd_stock;
            public int kill_sock;
        }
        public Result_Stock[] result_Stock = new Result_Stock[5];

        GameObject child_obj;

        void Start()
        {
            player_Num = 1;
        }

        /// <summary>
        /// リザルト引数(名前,生死,キル数)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sd"></param>
        /// <param name="kill"></param>
        public void Result_Column(string name, bool sd, int kill)
        {
            if (player_Num >= 5) return;

            result_Stock[player_Num].name_stock = name;
            result_Stock[player_Num].sd_stock = sd;
            result_Stock[player_Num].kill_sock = kill;
            player_Num++;
        }
        /// <summary>
        /// リザルト開始の関数
        /// </summary>
        public void Result_Start()
        {
            foreach (Transform child in transform)
            {
                //>子をアクティブに
                child_obj = child.gameObject;
                child_obj.SetActive(true);
            }
        }
        /// <summary>
        /// リザルト終了の関数
        /// </summary>
        public void Result_End()
        {
            StartCoroutine("ResultTimer");
        }
        /// <summary>
        /// Result_End()用のコルーチン
        /// </summary>
        /// <returns></returns>
        IEnumerator ResultTimer()
        {
            yield return new WaitForSeconds(5.0f);
            foreach (Transform child in transform)
            {
                //>子を非アクティブに
                child_obj = child.gameObject;
                child_obj.SetActive(false);
            }
            for (int i = 0;i < 5;i++)
            {
                result_Stock[i].name_stock = "";
                result_Stock[i].sd_stock = false;
                result_Stock[i].kill_sock = 0;
            }
            player_Num = 1;
        }
    }
