using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spitter : E_Animation {

    [SerializeField]
    E_SpitOut spitOut;

    public override IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(1.03f);
        spitOut.ShotGero(ai.targetPos);
        Debug.Log("Call");
    }
}
