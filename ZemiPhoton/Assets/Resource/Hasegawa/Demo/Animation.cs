using UnityEngine;

public class Animation : MonoBehaviour {

    public string anim_name;
    public Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        anim.Play(anim_name);
	}
}
