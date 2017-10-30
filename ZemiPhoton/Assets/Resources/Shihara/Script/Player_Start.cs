using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**IK追記
 * PhotonManagerを取得し
 * GameStart関数を呼び出す形に書き換えた
 * Ok状態を変更時に同期するよう追加
 * キー入力した人のみが準備状態のチェックを行うよう変更を加えた
 * チェックの参照を適切な形に変更
*/

public class Player_Start : Photon.MonoBehaviour {
    [SerializeField]
    public int check = 0;
    public bool ok = false;
    public static bool start = false;
	//IK追記
	PhotonManager PM;

	// Use this for initialization
	void Start () {
		//GameStart関数を呼ぶ為のコンポーネントの取得
		PM = GameObject.Find ("PhotonManager").GetComponent<PhotonManager> ();
		photonView.RPC("OK", PhotonTargets.All, true);
	}
	
	// Update is called once per frame
	void Update () {
		if(photonView.isMine)
        //ボタン入力に変更////////////////////////////////
	//        if (Input.GetKeyDown(KeyCode.Space)) OK();
    //    if (Input.GetKeyDown(KeyCode.Space)) 
	//		photonView.RPC("OK", PhotonTargets.All, true);

	if (Input.GetKeyDown (KeyCode.C))
			Check (true);
//        photonView.RPC("Check", PhotonTargets.All, ok);
        //////////////////////////////////////////////////

    }

//    [PunRPC]
    public void Check(bool Check_ok)
    {
		//IK
		int playercount=PhotonNetwork.playerList.Length;
//		Debug.Log(PhotonNetwork.countOfPlayers);
//        for(int i = 0; i < PhotonNetwork.countOfPlayers; i++)
		Debug.Log(playercount);
		for(int i = 0; i < playercount; i++)
        {
			if(GameObject.Find("Player"+(i+1)).GetComponent<Player_Start>().ok==true)check++;
		//if (Check_ok == true) check++;
            //if (check >= PhotonNetwork.countOfPlayers) check = PhotonNetwork.countOfPlayers;
        }

        //二人以上でスタート
//        if (2 <= check && check == PhotonNetwork.countOfPlayers)
        if (2 <= check && check == playercount)
        {
            Debug.Log("start");
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
