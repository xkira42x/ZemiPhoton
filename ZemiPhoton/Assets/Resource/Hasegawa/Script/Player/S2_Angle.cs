using UnityEngine;

public class S2_Angle : MonoBehaviour
{
    Vector3 S_MouseAngle = Vector3.zero;    // マウス座標
    Quaternion S_MainAngle;                 // キャラクタの向き
    public Quaternion S_mainAngle { get { return S_MainAngle; } set { S_MainAngle = value; } }
    Quaternion S_CameraAngle;               // カメラの向き
    public Quaternion S_cameraAngle { get { return S_CameraAngle; } set { S_CameraAngle = value; } }

    [SerializeField]
    Transform S_Collection; // カメラのトランスフォーム格納

    /// 初期化
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // カーソルを固定
        Cursor.visible = true;                      // カーソルを隠す
		S_MouseAngle = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
    }

    /// メインループ
    void Update()
    {
        // 視線移動
        if (!PlayerInfo.isDied)
            S_Eye();
    }

    /// マウスの移動量を取得して、カメラ・キャラクタの回転量に変換して更新する
    void S_Eye()
    {
        float yy = -(Input.GetAxis("Mouse Y")), xx = (Input.GetAxis("Mouse X"));

        if (xx != 0 || yy != 0)
        {
            // マウス移動量を保存
            S_MouseAngle += new Vector3(yy, xx, 0);
            // カメラの移動制限
            if (S_MouseAngle.x <= -60) S_MouseAngle.x = -60;
            else if (S_MouseAngle.x >= 60) S_MouseAngle.x = 60;
            // 角度に変換
            S_MainAngle = Quaternion.Euler(transform.localEulerAngles.x, S_MouseAngle.y, 0);
            S_CameraAngle = Quaternion.Euler(S_MouseAngle.x, S_Collection.localEulerAngles.y, 0);
            // 角度の更新
            transform.localRotation = S_MainAngle;          // キャラクタの向きを更新
            S_Collection.localRotation = S_CameraAngle;     // カメラの向きを更新
        }
    }
}