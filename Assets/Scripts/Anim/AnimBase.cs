using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBase : MonoBehaviour
{
    public const int FRAME_RATE = 24;

    protected virtual Transform Frames
    {
        get;
        private set;
    }

    private int currentIndex = 0;
    private float count;
    private float frameLength = 1f / FRAME_RATE;
    private bool directionUp = true;

    void Awake ()
    {
        count = frameLength;
    }
	
	void Update ()
    {
        if (Frames == null)
            return;

        count -= Time.deltaTime;

        if (count > 0f)
            return;

        count = frameLength;

        Frames.GetChild(currentIndex).gameObject.SetActive(false);

        currentIndex += directionUp ? 1 : -1;

        if (directionUp && Frames.childCount <= currentIndex)
        {
            currentIndex--;
            directionUp = false;
        }
        else if (!directionUp && 0 > currentIndex)
        {
            currentIndex = 0;
            directionUp = true;
        }

        Frames.GetChild(currentIndex).gameObject.SetActive(true);

    }
}
