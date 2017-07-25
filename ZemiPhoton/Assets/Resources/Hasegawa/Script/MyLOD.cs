using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLOD : MonoBehaviour {

	// 距離間隔
	[SerializeField]
	float[] lodLevels = new float[] { 100f, 50f, 10f };

	// 深度値
	[SerializeField]
	int[] shaderLOD = new int[] { 200, 100, 50 };

	// Renderer
	Renderer rend;
	// Camera Transform
	Transform cam;

	void Start (){
		// カメラの取得
		cam = GameObject.Find ("Camera").transform;
		// Renderer取得
		rend = GetComponent<Renderer> ();
		StartCoroutine ("UpdateLOD");
	}

	// 更新
	void SetLOD (int LOD){
		rend.material.shader.maximumLOD = shaderLOD[LOD];
	}

	// 
	IEnumerator UpdateLOD (){
		float[] levels = new float[lodLevels.Length];
		int i = 0;
		foreach (float level in lodLevels) {
			levels [i++] = level;
		}
		while (true) {
			// LOD対象者とプレイヤーカメラの距離
			float distance = (transform.position - cam.position).sqrMagnitude;
			// LOD深度
			int LOD = shaderLOD.Length - 1;
			// LOD判定
			foreach (float level in lodLevels) {
				if (distance > level) {
					// LOD更新
					SetLOD (LOD);
					break;
				}
				LOD--;
			}
			yield return new WaitForSeconds (0.1f);
		}
	}
}
