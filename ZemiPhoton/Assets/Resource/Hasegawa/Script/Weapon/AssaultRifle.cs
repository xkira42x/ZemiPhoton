
public class AssaultRifle : GunBase {

	/// 処理は基底クラス内のモノでまかなえるのですが、
	/// 基底クラスと差別化するために作っている

	/// 初期化
	public override void Awake (){
		base.Awake ();
	}

	/// 弾を撃つ(ベースの処理で収束している)
	public override void Action (){
		base.Action ();
	}
}
