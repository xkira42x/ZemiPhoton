using UnityEngine;
using UnityEngine.UI;
public class Traffic_Dis : MonoBehaviour {

    public Text traf_text;

    float traf_timer = 1;  //>グラフの更新頻度/1秒
	public N15_SizeOf traf_nam;
    //>描画する数値
    public int traf_dis;

    // Update is called once per frame
    void Update () {
        traf_timer -= Time.deltaTime;
        if (traf_timer <= 0.0)
        {
		traf_dis = traf_nam.Syncmass;
            traf_text.color = new Color(1, 1, 1);
            traf_text.text = traf_dis + "/bps".ToString();
            traf_timer = 1.0f;
        }
    }
}
