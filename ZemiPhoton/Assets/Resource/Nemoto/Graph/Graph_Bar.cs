using UnityEngine;
using UnityEngine.UI;

public class Graph_Bar : MonoBehaviour {
   
    public Text Graph_Bloc; //>グラフの代わりのテキスト
    int graph_Column;       //>グラフのカラム(列)

    float graph_timer = 1;  //>グラフの更新頻度/1秒

    public Traffic_Dis traf;

    void Start()
    {
        ////>初期化<////
        //>グラフの行初期化->0^20行
        graph_Column = 0; 
        
    }

    void Update()
    {
        graph_timer -= Time.deltaTime;
        if (graph_timer <= 0.0)
        {
            graph_Column = Bloc_Line(traf.traf_dis, graph_Column % 27);
            graph_timer = 1.0f;
        }
        
    }

    /// <summary>
    /// グラフ生成＜引数→通信料,列＞
    /// </summary>
    /// <param name="traffic"></param>
    int Bloc_Line(int traffic,int columu)
    {
        //>通信量をグラフに変換
        int nam = (int)(traffic / 100);
        //>自身(Root)の座標
        Vector3 this_transform = transform.position;
        
        for (int i = 0; i < nam + 1; i++)
        {
            if (i >= 25) break;
            //>生成
            Graph_Bloc = Instantiate(Graph_Bloc);//
            //>名前付け
            Graph_Bloc.name = "GraphBloc" + "[" + columu + "]" + "[" + i + "]".ToString();
            //>hierarchy上のTraffic_Bloc_rootの子に設定
            Graph_Bloc.transform.SetParent(transform, false);
            //設定でスケールを1に固定(これをしないとスケールバグがある)
            Graph_Bloc.transform.localScale = new Vector3(1, 1, 1);
            //>数値によって色付け：15以下→白。16~19→黄色。20~24→赤。25以上→紫
            if (15 >= i) Graph_Bloc.color = new Color(1, 1, 1);
            else if (19 >= i) Graph_Bloc.color = new Color(1, 1, 0);
            else if (24 > i) Graph_Bloc.color = new Color(1, 0, 0);
            else if (24 <= i) Graph_Bloc.color = new Color(1, 0, 1);
            //>生成位置
            Graph_Bloc.transform.position = new Vector3(this_transform.x + columu * 5, this_transform.y + i * 5, this_transform.z);
        }
        return columu + 1;
    }
}