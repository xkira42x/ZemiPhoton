using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_test : MonoBehaviour {

    public Transform Target;
    Vector3 vec;

    void Start()
    {
        StartCoroutine(Delete());
        vec = Target.position - transform.position;
    }

	// Update is called once per frame
	void Update () {
        //Vector3 vec = Target.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), 2f);
        transform.Translate(Vector3.forward * 0.05f);
	}

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
