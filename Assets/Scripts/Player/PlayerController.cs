using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode right;
    public KeyCode left;

    public float speed = 1f;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(right))
        {
            this.transform.Translate(Vector3.right * speed);
        }
        else if (Input.GetKey(left))
        {
            this.transform.Translate(Vector3.left * speed);
        }
    }
}
