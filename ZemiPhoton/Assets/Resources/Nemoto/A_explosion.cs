using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_explosion : Photon.MonoBehaviour{

    public GameObject explosion;
    void Start()
    {
        Destroy(explosion, 0.8f);
    }
	// Use this for initialization
	void Updata () {
        if(gameObject == null)
            PhotonNetwork.Destroy(explosion);
    }
}
