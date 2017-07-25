using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += this.transform.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += this.transform.forward * -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += this.transform.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += this.transform.right * -1;
        }
    }
}