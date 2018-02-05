using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LogText : MonoBehaviour
{

    /// ScrollView
    ScrollRect scroll_rect;
    /// ログの吐き出し先
    Text text_log;

    /// 表示するログデータの保存
    public static string logs = "";
    /// 過去のログデータ
    string old_logs = "";


    /// 初期化
    void Start()
    {
        scroll_rect = gameObject.GetComponent<ScrollRect>();
        text_log = scroll_rect.content.GetComponentInChildren<Text>();
    }

    /// メインループ
    void Update()
    {

        // ログデータが更新されたら、テキストに追加表示する
        // その後、自動スクロールし過去ログデータの更新をする
        if (scroll_rect != null && logs != old_logs)
        {
            text_log.text = logs;
            StartCoroutine(Delay(.5f, () =>
            {
                scroll_rect.verticalNormalizedPosition = 0;
            }));
            old_logs = logs;
        }
    }

    /// ログデータの追加更新をする
    public static void UpdateLog(string log_text)
    {
        logs += (log_text + "\n");
    }

    /// 遅延させた後に処理を実行する
    IEnumerator Delay(float interval, Action action)
    {
        yield return new WaitForSeconds(interval);
        action();
    }
}
