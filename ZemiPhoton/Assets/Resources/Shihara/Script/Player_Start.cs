using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Start : Photon.MonoBehaviour {
    [SerializeField]
    public int check = 0;
    public bool ok = false;
    public static bool start = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //ボタンに変更
        if (Input.GetKeyDown(KeyCode.Space)) OK();

        if (Input.GetKeyDown(KeyCode.C))
        photonView.RPC("Check", PhotonTargets.All, ok);

    }

    [PunRPC]
    public void Check(bool Check_ok)
    {
        Debug.Log(PhotonNetwork.countOfPlayers);
        for(int i = 0; i <= PhotonNetwork.countOfPlayers; i++)
        {
            if (Check_ok == true) check++;
            if (check >= PhotonNetwork.countOfPlayers) check = PhotonNetwork.countOfPlayers;
            check = 0;
        }

        //二人以上でスタート
        if (1 <= check && check == PhotonNetwork.countOfPlayers)
        {
            Debug.Log("start");
            start = true;
        }
    }

    public void OK()
    {
        ok = true;
    }
}
