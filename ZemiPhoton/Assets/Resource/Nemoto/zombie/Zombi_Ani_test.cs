using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Ani_test: MonoBehaviour
{
    protected Animator Anim;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //歩き
        if (Input.GetKeyDown(KeyCode.W))
        {
            Anim.SetBool("Walking", true);
            Anim.SetBool("Standing", false);
        }

        //爆死前のアニメーション
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Anim.SetBool("Being Strangled", true);
            Anim.SetBool("Standing", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Anim.SetBool("Shove Reaction", true);
        }
        else
        {
            Anim.SetBool("Shove Reaction", false);
            Anim.SetBool("Standing", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
            Anim.SetBool("Dying", true);
    }
}

