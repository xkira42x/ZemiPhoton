using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSet : MonoBehaviour {

	//プレファブ化されたラインレンダラーオブジェクト
	public GameObject P_LR;

	LineRenderer LR;

	// 線の本数
	public Vector3 L_mass;

	// 線の開始座標
	private Vector3 L_start;

	// 線の終了座標
	private Vector3 L_end;

	// 線の間隔
	public Vector3 L_intrvl;

	// Use this for initialization
	void Start () {

		//終わりの座標 ＝ 線の本数 * 線の間隔
		L_end.x = L_mass.x * L_intrvl.x;
		L_end.z = L_mass.z * L_intrvl.z;

		//始まりの座標 ＝ 終わりの座標 * -1/2
		L_start.x = L_end.x *-1/2;
		L_start.z = L_end.z * -1/2;

		//ラインのセットfor文
		for (float i = L_mass.x*-1/2; i <= L_mass.x/2; i++) {

			Vector3 end = new Vector3 (0, 0, L_end.z);
			LR_Set(i,"X_",end);
			end = new Vector3 (L_end.x, 0, 0);
			LR_Set(i,"Z_",end);

		}
	}

	/// <summary>
	/// ラインレンダラーを設定する関数
	/// </summary>
	/// <param name="ii">for文の宣言値から取得</param>
	/// <param name="vctl">どの座標軸をセットするか</param>
	/// <param name="end">座標軸に対応したend値を設定する</param>
	void LR_Set(float ii,string vctl,Vector3 end){

		if (vctl == "X_") {
			//１番目の座標の位置へ移動し、ラインレンダラーを生成
			this.transform.position = new Vector3 (ii * L_intrvl.x, this.transform.position.y,
				L_start.z);
		} else if (vctl == "Z_") {
			this.transform.position = new Vector3 (L_start.x, this.transform.position.y,
				ii*L_intrvl.z);
		}
		GameObject I_LR=Instantiate (P_LR,this.transform.position,this.transform.rotation);

		//コンポーネントの取得
		LR=I_LR.GetComponent<LineRenderer> ();

		//頂点の数をセット
		LR.SetVertexCount (2);

		//１番目の頂点の座標をセット
		LR.SetPosition (0, I_LR.transform.position);

		//次の座標へ移動
		I_LR.transform.position = new Vector3 (I_LR.transform.position.x+end.x,
			I_LR.transform.position.y+end.y,
			I_LR.transform.position.z+end.z);

		//２番目の頂点の座標をセット
		LR.SetPosition (1, I_LR.transform.position);

		//名前を設定し、一つのオブジェクトにまとめる
		I_LR.name=vctl+ii+"LR";
		string child_name =vctl+"Lines";
		I_LR.transform.parent = this.gameObject.transform.FindChild (child_name);

	}
}