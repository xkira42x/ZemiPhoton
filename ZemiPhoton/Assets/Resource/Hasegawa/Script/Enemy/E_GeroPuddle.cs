using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GeroPuddle : MonoBehaviour
{

    /// Transformのキャッシュ
    Transform trns;
    /// 縮小率
    Vector3 reductionRatio = new Vector3(0, .005f, .005f);
    /// ダメージ量
    [SerializeField]
    float dmg = 10;

    /// 初期化
    void Start()
    {
        trns = transform;
    }

    /// メインループ
    void Update()
    {
        // だんだんと縮小していき、最終的には消えてなくなる
        trns.localScale -= reductionRatio;
        if (trns.localScale.y <= 0.05f) Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
            other.gameObject.GetComponent<S2_Status>().Damage(dmg);
    }

}
