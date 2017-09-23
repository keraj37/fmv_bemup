using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public float maxLife = 100f;
    public Image image;

    public float Life
    {
        set
        {
            image.fillAmount = value / maxLife;
        }
    }
}
