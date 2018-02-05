using System.Collections;
using UnityEngine;

public class E_Spitter : E_Animation
{

    /// ゲロを吐き出すためのソースを格納
    [SerializeField]
    E_SpitOut spitOut;

    /// 攻撃時のコールバック
    /// ベースクラスの攻撃処理はいらないので上書きして消している
    public override IEnumerator OnAttack()
    {
        // ちょうど顔を前に突き出して、吐くようにポーズを取った際に吐き出しのエフェクトを再生する
        yield return new WaitForSeconds(1.03f);
        spitOut.ShotGero(ai.targetPos);
    }
}
