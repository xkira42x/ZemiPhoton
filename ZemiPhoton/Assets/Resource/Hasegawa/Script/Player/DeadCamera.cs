using UnityEngine;

public class DeadCamera : MonoBehaviour
{
	/// transformのキャッシュ
    Transform myTransform;
    [SerializeField]
    Transform otherTransform;
	/// プレイヤー番号
    int index;

	/// 初期化
    void Start()
    {
        index = PlayerInfo.playerNumber;
        myTransform = transform;
        otherTransform = PlayerList.GetPlayerList(PlayerInfo.playerNumber).transform;
    }

	/// メインループ
    void Update()
    {
		// キー入力でカメラフォーカスを当てる対象を変える
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index++;
            if (PlayerList.Check(index)) { }
            else { index = 0; }
            otherTransform = PlayerList.GetPlayerList(index).transform;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index--;
            if (PlayerList.Check(index)) { }
            else { index = PlayerList.length - 1; }
            otherTransform = PlayerList.GetPlayerList(index).transform;
        }
		// 座標の更新
        myTransform.position = otherTransform.position + new Vector3(0, 4, 0);
    }
}
