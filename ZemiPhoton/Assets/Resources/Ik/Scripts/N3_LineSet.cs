using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N3_LineSet : MonoBehaviour {

	//プレファブ化されたラインレンダラーオブジェクト
	public GameObject N_P_LR;

	LineRenderer N_LR;

	// 線の本数
	public Vector3 N_L_mass;

	// 線の開始座標
	private Vector3 N_L_start;

	// 線の終了座標
	private Vector3 N_L_end;

	// 線の間隔
	public Vector3 N_L_intrvl;

	//線の色変更に使用するマテリアルの配列
	public Material[] Colors;

	//カラーカウント
	private int ColCnt=0;

	// Use this for initialization
	void Start () {

		//終わりの座標 ＝ 線の本数 * 線の間隔
		N_L_end.x = N_L_mass.x * N_L_intrvl.x;
		N_L_end.z = N_L_mass.z * N_L_intrvl.z;

		//始まりの座標 ＝ 終わりの座標 * -1/2
		N_L_start.x = N_L_end.x *-1/2;
		N_L_start.z = N_L_end.z * -1/2;

		//ラインのセットfor文
		for (float i = N_L_mass.x*-1/2; i <= N_L_mass.x/2; i++) {

			Vector3 N_end = new Vector3 (0, 0,N_L_end.z);
			N_LR_Set(i,"X_",N_end);
			N_end = new Vector3 (N_L_end.x, 0, 0);
			N_LR_Set(i,"Z_",N_end);

		}
	}

	/// <summary>
	/// ラインレンダラーを設定する関数
	/// </summary>
	/// <param name="ii">for文の宣言値から取得</param>
	/// <param name="vctl">どの座標軸をセットするか</param>
	/// <param name="end">座標軸に対応したend値を設定する</param>
	void N_LR_Set(float ii,string vctl,Vector3 end){

		if (vctl == "X_") {
			//１番目の座標の位置へ移動し、ラインレンダラーを生成
			this.transform.position = new Vector3 (ii * N_L_intrvl.x, this.transform.position.y,
				N_L_start.z);
		} else if (vctl == "Z_") {
			this.transform.position = new Vector3 (N_L_start.x, this.transform.position.y,
				ii*N_L_intrvl.z);
		}
		//実生成
		GameObject I_LR=Instantiate (N_P_LR,this.transform.position,this.transform.rotation);

		//カラーを設定
		I_LR.GetComponent<LineRenderer>().material=Colors[ColCnt];

		//カウントアップ
		ColCnt++;

		//色が最後まで周ったら、最初に戻す
		if (ColCnt >= Colors.Length) {
			ColCnt = 0;
		}

		//コンポーネントの取得
		N_LR=I_LR.GetComponent<LineRenderer> ();

		//頂点の数をセット
//		N_LR.SetVertexCount (2);
		N_LR.positionCount=2;

		//１番目の頂点の座標をセット
		N_LR.SetPosition (0, I_LR.transform.position);

		//次の座標へ移動
		I_LR.transform.position = new Vector3 (I_LR.transform.position.x+end.x,
			I_LR.transform.position.y+end.y,
			I_LR.transform.position.z+end.z);

		//２番目の頂点の座標をセット
		N_LR.SetPosition (1, I_LR.transform.position);

		//名前を設定し、一つのオブジェクトにまとめる
		I_LR.name=vctl+ii+"LR";
		string child_name =vctl+"Lines";
		I_LR.transform.parent = this.gameObject.transform.FindChild (child_name);

	}
}