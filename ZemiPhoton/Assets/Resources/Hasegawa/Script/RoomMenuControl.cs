using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMenuControl : Photon.MonoBehaviour {

	// ルーム作成のセッティングメニュー
	[SerializeField]GameObject RoomSettings;
	[SerializeField]InputField RoomNane;
	[SerializeField]Dropdown RoleDropdown;

	// ルーム選択画面
	[SerializeField]GameObject RoomSelectinView;
	[SerializeField]Transform Content;
	[SerializeField]int RoomNum = 0;

	// ルーム一覧のひな型
	[SerializeField]GameObject RoomListTemplate;

	[SerializeField]GameObject CreateRoomButton;
	[SerializeField]GameObject CreateRoomCancelButton;

	/// <summary>
	/// <para>作成されたルームの表示</para>
	/// True ルーム有り	False ルームなし
	/// </summary>
	public bool DisplayRoomList(){
		// ルームアイテムをすべて削除
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("RoomItem"))
			Destroy (obj);
		
		// ルームが一つでも作られていたら
		// ルームリストとして表示をする
		RoomNum = PhotonNetwork.GetRoomList().Length;
		if (RoomNum == 0) {
			Debug.Log ("部屋なし");
			return false;
		} else {
			// ルーム情報を取得して、表示用のオブジェクトに入れる
			foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
				GameObject obj = Instantiate (RoomListTemplate, new Vector3 (0, 0, 0), Quaternion.identity);
				obj.GetComponent<RoomItem> ().SetRoomInfo (room.Name, room.PlayerCount, room.MaxPlayers);
				obj.transform.parent = Content;
			}
			return true;
		}
	}

	/// <summary>
	/// ルーム作成ボタン
	/// </summary>
	public void OnClickCreateRoomButton(){
		RoomSelectinView.SetActive (false);
		RoomSettings.SetActive (true);
		CreateRoomButton.SetActive (false);
		CreateRoomCancelButton.SetActive (true);
	}
	/// <summary>
	/// ルーム作成キャンセルボタン
	/// </summary>
	public void OnClickCreateRoomCancelButton(){
		RoomSettings.SetActive (false);
		RoomSelectinView.SetActive (true);
		CreateRoomButton.SetActive (true);
		CreateRoomCancelButton.SetActive (false);
	}

	/// <summary>
	/// ルーム作成の完了ボタン
	/// </summary>
	public void OnClickRoomSettingCompleteButton(){
		
		PlayerInfo.role = (RoleDropdown.value == 0) ? PlayerInfo.Server : PlayerInfo.Client;

		//　ルームオプションを設定
		RoomOptions ro = new RoomOptions ();
		//　ルームを見えるようにする
		ro.IsVisible = true;
		//　部屋の入室最大人数
		ro.MaxPlayers = 5;
		string roomname = RoomNane.text;
		// ルーム名の空白判定
		if (string.IsNullOrEmpty (roomname)) {
			roomname = "Room" + (RoomNum + 1).ToString ();
			RoomNane.text=roomname;
		}
		// ルーム作成、もしくは参加
		PhotonNetwork.JoinOrCreateRoom (RoomNane.text, ro, TypedLobby.Default);

		RoomSettings.SetActive (false);
		RoomSelectinView.SetActive (false);
		CreateRoomButton.SetActive (false);
		CreateRoomCancelButton.SetActive (false);
	}

	public void JoinRoom(){
		RoomSettings.SetActive (false);
		RoomSelectinView.SetActive (false);
		CreateRoomButton.SetActive (false);
		CreateRoomCancelButton.SetActive (false);
	}

}