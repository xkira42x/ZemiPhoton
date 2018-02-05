using UnityEngine;

public class E_Explosion : MonoBehaviour
{

    /// ダメージ
	[SerializeField]
    float dmg = 10;

    /// エフェクトの当たり判定
	void OnParticleCollision(GameObject obj)
    {
        // プレイヤーに当たったらダメージを与える
        if (obj.layer == 10)
        {
            obj.GetComponent<S2_Status>().Damage(dmg);
        }
    }
}
