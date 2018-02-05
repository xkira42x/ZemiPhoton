using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class S3_Shot : Photon.MonoBehaviour
{

    [SerializeField]
    Transform GunSpot;                  // 持っている銃が置かれる位置
    [SerializeField]
    GunBase MyGun;                      // 銃情報インターフェイス
    [SerializeField]
    public Transform CameraT;           // カメラの位置情報
    [SerializeField]
    Text UI;							// 弾切れなどを表示するUI
    [SerializeField]
    Text NumberofRemainingBullets;      // 残弾数の表示
    [SerializeField]
    Image NumberofRemainingBulletsImage;// 残弾数のゲージ表示
    int lastAmmoNum = 0;
    bool ShowUI = false;                // UIの表示判定
    [SerializeField]
    S3_Shot shot;                       // S3_Shotクラスのキャッシュ

    RaycastHit hitInfo;                 // レイキャストで衝突した情報を格納する

    Item_State2 iState;                 // 所持アイテムの表示先

    /// 初期化
    void Start()
    {
        // 銃が初期設定されていたら、撃てる状態にする
        if (MyGun != null)
            MyGun.ShotSetting(shot);
        // クラスのキャッシュ取得
        shot = GetComponent<S3_Shot>();
        // 所持アイテムの表示先設定
        iState = GameObject.Find("UI").GetComponent<Item_State2>();
    }

    /// メインループ
    void Update()
    {

        // プレイヤーコントロール設定
        if (photonView.isMine && !PlayerInfo.isDied)
        {

            // アタック(メッセージを送る)
            if (Input.GetMouseButton(0))
            {
                gameObject.SendMessage("ToAttackMSG", SendMessageOptions.DontRequireReceiver);
            }

            // リロード
            if (Input.GetKeyDown(KeyCode.R))
            {
                MyGun.ReloadRequest();
                UI.text = "";
            }

            // アイテムを取得できるか、レイキャストで判定する
            if (Physics.Raycast(CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer("Item")))
            {

                // Eキーを入力してもらう事を促すテキストを表示する
                WriteUIText("武器を拾う(E)");

                // アイテム取得(メッセージを送る)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.SendMessage("PickUpItemMSG", hitInfo.collider.gameObject, SendMessageOptions.DontRequireReceiver);
                }
            }
            ShowBulletUI();
        }
    }

    /// アイテムの取得
    public void PickUpItem()
    {
        if (Physics.Raycast(CameraT.position, CameraT.forward, out hitInfo, 5, 1 << LayerMask.NameToLayer("Item")))
        {
            PickUpItemMSG(hitInfo.collider.gameObject);
        }
    }

    /// アタックメッセージ
    /// 持っているアイテムを使用する。アイテムを持っていなければ持っていない事をUI表示する
    public void ToAttackMSG()
    {
        if (MyGun != null)
            MyGun.Action();
        else
            WriteUIText("武器を持っていません");
    }

    /// アイテムの取得・その前にアイテムを持っていたら捨てる
    public void PickUpItemMSG(GameObject obj)
    {
        // アイテムを捨てる
        if (MyGun != null)
            MyGun.ThrowAway();
        // アイテムを取得する
        obj.transform.parent = GunSpot;
        MyGun = obj.GetComponent<GunBase>();
        MyGun.ShotSetting(shot);
    }

    /// リロードの催促
    void OutOfAmmoMSG()
    {
        WriteUIText("リロードして下さい(R)");
    }

    /// UIテキストの書き込み処理
    /// 引数の文字を表示して、一定時間で消す
    void WriteUIText(string UIText)
    {
        if (UI != null)
        {
            UI.text = UIText;
            StartCoroutine("ClearUIText");
            ShowUI = true;
        }
    }
    /// UIテキストをクリアする
    IEnumerator ClearUIText()
    {
        yield return new WaitForSeconds(1);
        if (ShowUI)
            ShowUI = false;
        else
            UI.text = "";
    }

    /// 銃を持っている間、残弾数を表示する
    void ShowBulletUI()
    {
        if (MyGun != null)
        {
            if (lastAmmoNum != MyGun.GetMagazine())
            {
                iState.Number_of_remaining_bullets = MyGun.GetMagazine();
                lastAmmoNum = MyGun.GetMagazine();
                iState.Number_of_remainig_bullets_bar = MyGun.GetMagazineRatio();
            }
        }
    }

}