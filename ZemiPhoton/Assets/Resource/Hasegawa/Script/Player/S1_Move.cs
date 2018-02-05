using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class S1_Move : MonoBehaviour
{
    Transform myTransform;
    [SerializeField]
    GameObject DieCamera;   // 死亡時に表示するカメラ
    [SerializeField]
    GameObject Body;        // キャラクターオブジェクト

    public const byte IDLE = 0, WALK = 1, JUMP = 2, CROUCH = 3, CROUCHMOVE = 4, DIE = 5;// 行動ステート定数
    [SerializeField]
    byte status = IDLE;                                    // 行動ステート保存
    public byte Status { get { return status; } }          // 行動ステートのゲッタ

    [SerializeField]
    Transform myCollection; // カメラなどのまとめているオブジェクト

    [SerializeField]
    float speed = 0.1f;     // 移動速度
    float motion = 0;       // 移動判定（0:停止	1:移動）

    bool isGround;          // 床判定

    /// 下方向にレイを飛ばして着地判定する
    void IsGround() { isGround = Physics.Raycast(myTransform.position, Vector3.down, 0.3f); }

    bool isCrouch = false;  // しゃがみ判定

    Rigidbody myRigidbody;  // 物理処理の格納

    /// 初期化
    void Start()
    {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    /// メインループ
    void Update()
    {
        if (!PlayerInfo.isDied)
        {
            // キー移動
            S_KeyMove();

            // ジャンプ
            S_Jump();

            // しゃがみ
            Crouch();

            // 着地判定
            IsGround();

            // 行動ステートを設定する
            status = (!isGround) ? JUMP : (motion == 1) ? WALK : (isCrouch) ? CROUCH : IDLE;
        }
    }

    /// キー移動判定
    /// 十字（WASD）キー操作で移動する
    void S_KeyMove()
    {
        // キー入力した方向を移動量として設定する(horizontal:左右 vertical:前後)
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
        float vertical = CrossPlatformInputManager.GetAxis("Vertical") * speed;

        myTransform.Translate(horizontal, 0, vertical);

        // 移動しているか判定する(0:停止 1:移動)
        motion = (horizontal != 0 || vertical != 0) ? 1 : 0;
    }

    /// ジャンプ
    /// 物理処理で上方向にはね上げさせる
    void S_Jump()
    {
        // ジャンプスイッチ
        if (isGround && Input.GetKeyDown(KeyCode.Space))
            myRigidbody.velocity = Vector3.up * 5;
    }

    /// しゃがむ
    void Crouch()
    {
        float width = 0f;   // 初期の高さ

        // 左シフトキーが押された時、カメラの高さを低くしてしゃがんでいる
        if (Input.GetKey(KeyCode.LeftControl))
        {
            width = -1f;
            isCrouch = true;
        }
        else
            isCrouch = false;
        myCollection.localPosition = new Vector3(0, width, 0);
    }

    /// 死亡ステートに変更
    public void Died()
    {
        // 死亡時のカメラを生成
        Instantiate(DieCamera, myTransform.position + new Vector3(0, 4, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        // プレイヤカメラを非アクティブにする
        GetComponentInChildren<Camera>().gameObject.SetActive(false);
        // キャラを表示
        Body.SetActive(true);
        // 死亡ステートに変更
        status = DIE;
        PlayerInfo.isDied = true;
    }
}