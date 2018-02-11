using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSpace : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if(!PlayerInfo.isDied)
        transform.rotation = Camera.main.transform.rotation;
	}
}
