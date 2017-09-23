using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : AnimBase
{
    protected override Transform Frames
    {
        get
        {
            return framesRoot;
        }
    }

    public Transform framesRoot;
}
