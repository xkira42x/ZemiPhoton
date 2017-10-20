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
        //ボタン入力に変更////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Space)) OK();

        if (Input.GetKeyDown(KeyCode.C))
        photonView.RPC("Check", PhotonTargets.All, ok);
        //////////////////////////////////////////////////

    }

    [PunRPC]
    public void Check(bool Check_ok)
    {
<<<<<<< HEAD
        Debug.Log(PhotonNetwork.countOfPlayers);
        for(int i = 0; i <= PhotonNetwork.countOfPlayers; i++)
=======
		//IK
		int playercount=PhotonNetwork.playerList.Length;
//		Debug.Log(PhotonNetwork.countOfPlayers);
//        for(int i = 0; i < PhotonNetwork.countOfPlayers; i++)
		Debug.Log(playercount);
		for(int i = 0; i < playercount; i++)
>>>>>>> df29816e4d20e17206aa079e3e5ef64c5dc16ab4
        {
            if (Check_ok == true) check++;
            //if (check >= PhotonNetwork.countOfPlayers) check = PhotonNetwork.countOfPlayers;
        }

        //二人以上でスタート
//        if (2 <= check && check == PhotonNetwork.countOfPlayers)
        if (2 <= check && check == playercount)
        {
            Debug.Log("start");
            start = true;
        }
        check = 0;
    }

    public void OK()
    {
        ok = true;
    }
}
