using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : Photon.MonoBehaviour {
    [SerializeField]public int check = 0;
    public bool ok = false;
    public static bool start = false;
	PhotonManager PM;

	void Start () {
		PM = GameObject.Find ("PhotonManager").GetComponent<PhotonManager> ();
		photonView.RPC("OK", PhotonTargets.All, true);
	}

    void Update()
    {
        if (photonView.isMine)
            if (Input.GetKeyDown(KeyCode.C))
                Check(true);
    }

    public void Check(bool Check_ok)
    {
		int playercount=PhotonNetwork.playerList.Length;
		Debug.Log(playercount);
		for(int i = 0; i < playercount; i++)
        {
			if(GameObject.Find("Player"+(i+1)).GetComponent<Player_Start>().ok==true)check++;
        }

        if (2 <= check && check == playercount)
        {
            start = true;
			PM.GameStart ();
			photonView.RPC ("GAMESTART", PhotonTargets.All);
        }
        check = 0;
    }

	[PunRPC]
	public void OK(bool bb)
    {
        ok = bb;
    }

	[PunRPC]
	void GAMESTART(){
		StartCoroutine (PM.Tmanager.TimeCountDown ());
	}
}
