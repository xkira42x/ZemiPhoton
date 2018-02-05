using UnityEngine;
using UnityEngine.UI;
public class Blur : MonoBehaviour {
    Image BG_image;
    Color alpha = new Color(0, 0, 0, 0.01f);

    // Use this for initialization
    void Start () {
        BG_image = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
        //>alpha値が一定になるまで濃くする
        if(BG_image.color.a <= 0.6f)
        {
            BG_image.color += alpha;
        }
        //>一定超えたら子を生成
        else
        {
            foreach (Transform child in transform)
            {
                //>子をアクティブに
                GameObject child_next = child.gameObject;
                child_next.SetActive(true);
            }
        }
    }

    void OnDisable()
    {
        BG_image.color -= new Color(0,0,0,BG_image.color.a);
    }
}
