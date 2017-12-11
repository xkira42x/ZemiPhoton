using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Spot : MonoBehaviour {

    float y;
    float speed = 5.0f;

    public static float tama  = 50;
    public static float tama_max = tama;

    public GameObject Rec;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
            y -= Time.deltaTime * 100;

        if (Input.GetKey(KeyCode.D))
            y += Time.deltaTime * 100;

        transform.rotation = Quaternion.Euler(0, y, 0);

        if (Input.GetMouseButton(0) && tama > 0) tama -= 1;
        if (Input.GetKeyDown(KeyCode.R)) tama = tama_max;

    }

}
