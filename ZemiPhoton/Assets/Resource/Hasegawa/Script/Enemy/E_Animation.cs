using System.Collections;
using UnityEngine;

public class E_Animation : MonoBehaviour
{

    /// アニメーター格納
    [SerializeField]
    protected Animator animator;
    /// AIソースの格納
    [SerializeField]
    protected E_AI ai;

    /// アニメーションの名前格納
    protected string[] anm_name = new string[] { "Idle", "Run", "Attack", "Hit", "Die" };
    /// 攻撃制限のフラグ
    protected bool attacked = false;
    /// 死亡制限のフラグ
    protected bool died = false;

    /// アニメーションの取得
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// E_AIのステータスによってアニメーションを制御する
    /// アニメーション以外の処理（攻撃の当たり判定・死亡時の処理）も
    /// この中でやっている
    void Update()
    {

        // アニメーション再生
        animator.SetBool(anm_name[(int)ai.State], true);

        // 攻撃処理
        if (ai.State == E_AI.ATTACK && !attacked)
        {
            attacked = true;
            // 攻撃判定する
            StartCoroutine(Attacked());
            //StartCoroutine (ReturnToNormal ());
        }

        // 死亡処理
        if (ai.State == E_AI.DIE && !died)
        {
            died = true;
            StartCoroutine(Died());
        }

    }

    /// 一定時間後に攻撃処理をする
    /// 攻撃アニメーションで腕を振り切ったタイミングで
    /// 攻撃判定が走るように調整する
    IEnumerator Attacked()
    {
        // アニメーションがAttackになるまで待つ
        while (!animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Attack")))
            yield return null;

        StartCoroutine(ReturnToNormal());
        StartCoroutine(OnAttack());
    }

    /// 攻撃のコールバック
    public virtual IEnumerator OnAttack()
    {
        float time = animator.GetCurrentAnimatorStateInfo(0).length / 2;
        yield return new WaitForSeconds(time);
        // ダメージ処理
        ai.AttackedTheTarget();
    }

    IEnumerator ReturnToNormal()
    {
        // アニメーションがAttackになるまで待つ
        //while (!animator.GetCurrentAnimatorStateInfo (0).shortNameHash.Equals (Animator.StringToHash("Attack")))
        //	yield return null;
        // アニメーションのステータスを取得
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // アニメーションが終了するまで１フレームずつ待つ
        while (!ChangeAnimation(stateInfo))
            yield return null;
        // アニメーションの攻撃を無効化
        animator.SetBool(anm_name[(int)E_AI.ATTACK], false);
        // 追尾状態にシフト
        ai.MakeThenRun();
        // 次の攻撃ができるようにする
        attacked = false;
    }

    /// 倒された際のアニメーションが終わったタイミングで自身を削除する
    IEnumerator Died()
    {
        // アニメーションがDieになるまで待つ
        while (!animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Die")))
            yield return null;
        // アニメーションのステータスを取得して、再生時間分遅延させる
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length - .5f);
        OnDied();
        // 削除
        Destroy(gameObject);
    }


    /// アニメーションが変わったかをハッシュ比較でする
    /// 戻り値　true:変わった　false:変わっていない
    bool ChangeAnimation(AnimatorStateInfo info)
    {
        return info.nameHash != animator.GetCurrentAnimatorStateInfo(0).nameHash;
    }

    /// 倒された際のコールバック
    public virtual void OnDied()
    {
    }

}
