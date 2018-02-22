using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// リザルトテキスト描画
/// </summary>
public class Text_Dis : MonoBehaviour {
    
    int player_num = 1;//プレイヤーのナンバー
    GameObject Player_result;
    GameObject child_obj;

    public Text survive;

    public Result_Date RD;
    string player_name;
    bool sd;
    int kill;

    void OnEnable()
    {
        foreach (Transform child in transform)
        {

            //>子をアクティブに
            child_obj = child.gameObject;
            child_obj.SetActive(true);
            //>名前
            player_name = RD.result_Stock[player_num].name_stock;
            //>生死
            sd = RD.result_Stock[player_num].sd_stock;
            //>倒した数
            kill = RD.result_Stock[player_num].kill_sock;
            Text retext = null;

            //>孫参照（テキスト書き換え）
            foreach (Transform child_next in child_obj.transform)
            {
                retext = child_next.gameObject.GetComponent<Text>();

                if (retext.name == "Player" + player_num + "_Name".ToString())
                {
                    retext.text = player_name;
                    print(PlayerInfo.playerName);
                }
                if (retext.name == "Player" + player_num + "_SD".ToString())
                {
                    if (sd) retext.text = "生存";
                    else retext.text = "死亡";

                    if (player_name == PlayerInfo.playerName)
                    {
                        if (sd) survive.text = "生存した！おめでとう！";
                        else survive.text = "あなたは死んでしまった！";
                    }
                }
                if (retext.name == "Player" + player_num + "_Kill".ToString())
                {
                    retext.text = kill.ToString();
                }               
            }
            if (child.name != "Base")
                player_num++;
        }
        RD.Result_End();
    }
    void OnDisable()
    {
        player_num = 1;

        foreach (Transform child in transform)
        {
            //>子を非アクティブに
            child_obj = child.gameObject;
            child_obj.SetActive(false);
        }
    }
}
