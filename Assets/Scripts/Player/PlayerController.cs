using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxX;
    public float minX;

    public KeyCode right;
    public KeyCode left;
    public KeyCode punch;

    public float speed = 1f;

    public AnimPlayer animIdle;
    public AnimPlayer animPunch;

    void Start()
    {
        animIdle.Active = true;
        animPunch.Active = false;
    }

    private void DisableAnims()
    {
        animPunch.Active = false;
        animIdle.Active = false;
    }

    void Update()
    {
        if(Input.GetKey(right))
        {
            this.transform.Translate(Vector3.right * speed);

            if(this.transform.position.x > maxX)
            {
                this.transform.position = new Vector3(maxX, this.transform.position.y, this.transform.position.z);
            }
        }
        else if (Input.GetKey(left))
        {
            this.transform.Translate(Vector3.left * speed);

            if (this.transform.position.x < minX)
            {
                this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
            }
        }

        if (Input.GetKeyDown(punch))
        {
            DisableAnims();
            animPunch.Trigger(() => animIdle.Active = true);
        }
    }
}
