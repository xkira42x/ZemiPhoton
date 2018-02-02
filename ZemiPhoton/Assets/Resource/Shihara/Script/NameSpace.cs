using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSpace : MonoBehaviour {

    public Text Name;
    public S2_Status status;
	// Use this for initialization
	void Start () {
        Name.text = status.UserName;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Camera.main.transform.rotation;
	}
}
