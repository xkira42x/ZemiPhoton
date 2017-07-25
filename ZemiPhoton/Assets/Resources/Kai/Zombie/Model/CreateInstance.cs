using UnityEngine;
using System.Collections;

public class CreateInstance : MonoBehaviour
{
    // 複製するPrefabをobjにInspector格納しておく
    public GameObject obj;
    // Update is called once per frame
    void Update()
    {
        // マウスが左クリックされたら以下を実行
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instance = (GameObject)Instantiate(obj);
            // 複製したオブジェクトの位置をemmitterオブジェクトと同じ位置へ
            instance.transform.position = gameObject.transform.position;
            
        }
    }
}