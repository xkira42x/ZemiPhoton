using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : Photon.MonoBehaviour
{

    [SerializeField]
    GameObject logView;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Result()
    {
        logView.SetActive(true);
        logView.GetComponentInChildren<Text>().color = Color.red;
        photonView.RPC("SyncPlayScore", PhotonTargets.AllBufferedViaServer, PlayerInfo.playerName, PlayerInfo.killCount, PlayerInfo.isDied);
    }

    [PunRPC]
    public void SyncPlayScore(string name, int score, bool died)
    {
        LogText.UpdateLog(name + "  " + "撃破数 " + score.ToString() + "  " + ((died) ? "死亡" : "生存"));
    }

}
