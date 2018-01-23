using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCamera : MonoBehaviour {

    Transform myTransform;
    Transform otherTransform;
    int index;

	void Start () {
        index = PlayerInfo.playerNumber;
        otherTransform = PlayerList.GetPlayerList(index).transform;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index++;
            if (PlayerList.Check(index)) { }
            else { index = 0; }
            otherTransform = PlayerList.GetPlayerList(index).transform;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index--;
            if (PlayerList.Check(index)) { }
            else { index = PlayerList.length - 1; }
            otherTransform = PlayerList.GetPlayerList(index).transform;
        }
        myTransform.position = otherTransform.position + new Vector3(0, 4, 0);
    }
}
