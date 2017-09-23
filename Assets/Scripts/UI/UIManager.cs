using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public LifeBar bar1;
    public LifeBar bar2;

    void Awake()
    {
        instance = this;
    }


}
