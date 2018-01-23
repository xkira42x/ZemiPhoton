using UnityEngine;

/// <summary>
/// 自身の破棄時に、自動的にMaterialを破棄する（メモリリーク対策）
/// </summary>
public class AutoDestroyMaterials : MonoBehaviour {

	#region Parameters
	// 重複登録防止のためのルートフラグ
	[System.NonSerialized] public bool IsRoot = true;
	#endregion

	/// 初期化
	void Start(){
		if(IsRoot){
			// 配下のレンダラすべてに追加
			foreach(var r in this.GetComponentsInChildren<Renderer>()){
				r.gameObject.AddComponent<AutoDestroyMaterials>().IsRoot = false;
			}
			// 配下のパーティクルシステムすべてに追加
			foreach(var p in this.GetComponentsInChildren<ParticleSystem>()){
				p.gameObject.AddComponent<AutoDestroyMaterials>().IsRoot = false;
			}
		}
	}

	/// 削除された時のコールバック
	void OnDestroy(){
		// レンダラのマテリアルを破棄(パーティクルシステムのレンダラも含まれる)
		var thisRenderer = this.GetComponent<Renderer>();
		if(thisRenderer != null && thisRenderer.materials != null){
			foreach(var m in thisRenderer.materials){
				DestroyImmediate(m);
			}
		}

		// 自身のオブジェクトをnull化してメモリ開放になる
		var obj = gameObject.GetComponent<GameObject> ();
		obj = null;
	}

}