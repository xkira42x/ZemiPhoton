using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float speed;//移動速度
	public float lstspd;//最終決定された移動速度
	public string left;
	public string right;
	public string up;
	public string down;
//	public string dush;

	float dashspd;

//	Move_Ai Ai;
	bool AiFlg=false;

	//ステージ上にいるかを保持する変数
	public bool liveflg;

	float ReviveTime;//復帰する時間,10~20間で調整

//	GameController GC;

	bool Moveflg;

	float FloorPosY;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) {
			this.transform.position = new Vector3 (this.transform.position.x -lstspd, this.transform.position.y, this.transform.position.z);
		}
		//右
		if (Input.GetKey (right)) {
			this.transform.position = new Vector3 (this.transform.position.x + lstspd, this.transform.position.y, this.transform.position.z);
		}
		//上
		if (Input.GetKey (up)) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + lstspd);
		}
		//下
		if (Input.GetKey (down)) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - lstspd);
		}

	}

}
