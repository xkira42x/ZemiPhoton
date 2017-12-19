using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	public static string playerName = "default";	// ユーザー名
	public static int playerNumber = 1;				// ユーザー番号
	public static int statusCount = 1;				// UIテキストの参照番号

	public static bool Spawn = false;				// プレイヤー生成

	public const byte Server = 0,Client = 1;		// サーバークライアントの振り分け
	public static byte role;						// サバクラの振り分けを保存
	public static bool isClient(){return role == Client;} // クライアント判定

	public static bool onTimer = false;				// タイマの起動判定
}
