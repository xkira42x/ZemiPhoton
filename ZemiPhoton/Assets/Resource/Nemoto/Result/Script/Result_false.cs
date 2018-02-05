using UnityEngine;
public class Result_false : MonoBehaviour {

    GameObject child_obj;
    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            //>子を非アクティブに
            child_obj = child.gameObject;
            child_obj.SetActive(false);
        }
    }
}
