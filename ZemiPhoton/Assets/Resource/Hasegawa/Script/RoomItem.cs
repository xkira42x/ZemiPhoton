using UnityEngine;
using UnityEngine.UI;

public class RoomItem : Photon.MonoBehaviour {

	[SerializeField]Text RoomName;			// ルーム名表示テキスト
	[SerializeField]Text NumberofPeople;	// ルーム内人数表示テキスト

	string _roomName;						// ルーム名
	int _numofPeople;						// ルーム内人数
	byte _maxnumofPeople;					// ルーム内最大人数

    /// ルーム情報の設定
    public void SetRoomInfo(RoomInfo room) {
        _roomName = room.Name;			    // ルーム名の格納
        string[] ss = _roomName.Split("_"[0]);
        _numofPeople = room.PlayerCount;	// ルーム内人数の格納
        _maxnumofPeople = room.MaxPlayers;  // ルーム内最大人数の格納

        // ルーム名の設定・ルーム内人数の設定・ルーム内最大人数の設定
        RoomName.text = "ルーム名 " + ss[0];//_roomName;
		NumberofPeople.text = "人数 " + (_numofPeople - 1).ToString () + " / " + (_maxnumofPeople - 1).ToString ();
	}

	/// ルーム参加ボタン
	public void OnClickJoinButton(){
		// ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		// ルームを見えるようにする
		ro.IsVisible = true;
		// 部屋の入室最大人数
		ro.MaxPlayers = _maxnumofPeople;
        // ルーム作成、もしくは参加
		PhotonNetwork.JoinOrCreateRoom (_roomName, ro, TypedLobby.Default);
        PlayerInfo.roomID = _roomName;

		PlayerInfo.role = PlayerInfo.Client;
	}
}