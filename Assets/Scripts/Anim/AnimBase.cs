using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimType
{
    LOOP,
    TRIGGER
}

public class AnimBase : MonoBehaviour
{
    protected virtual Transform Frames
    {
        get;
        private set;
    }

    public bool sort = false;

    public AnimType animType;

    private int currentIndex = 0;
    private float count;
    public float frameLength = 0.02f;
    private int direction = 1;

    public int hitFrame = -1;
    public float hitDistance = 3f;
    public Action<AnimBase> onHitFrame;
    public Action onEndCallback;

    protected bool active = false;
    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            this.gameObject.SetActive(value);
            Frames.GetChild(currentIndex).gameObject.SetActive(false);
            currentIndex = 0;
            Frames.GetChild(currentIndex).gameObject.SetActive(true);
        }
    }

    void Awake ()
    {
        count = frameLength;

        if(sort)
        {
            var ts = new List<Transform>();

            foreach(Transform t in Frames)
                ts.Add(t);

            ts.Sort((x, y) => x.name.CompareTo(y.name));

            foreach (Transform t in ts)
                t.SetAsLastSibling();
        }
    }

    public void Trigger(Action callback)
    {
        onEndCallback = callback;
        Active = true;
    }

    private void Finish()
    {
        if (onEndCallback != null)
            onEndCallback();

        Active = false;
    }

    void Update ()
    {
        if (Frames == null || !Active)
            return;

        count -= Time.deltaTime;

        if (count > 0f)
            return;

        count = frameLength;

        Frames.GetChild(currentIndex).gameObject.SetActive(false);

        if (direction > 0 && currentIndex + 1 >= Frames.childCount)
        {
            if(animType == AnimType.LOOP)
                direction = -1;
            else
            {
                Finish();
                return;
            }
        }
        else if (direction < 0 && currentIndex <= 0)
        {
            direction = 1;
        }

        currentIndex += direction;

        Frames.GetChild(currentIndex).gameObject.SetActive(true);

        if(hitFrame != -1 && currentIndex == hitFrame)
        {
            if(onHitFrame != null)
                onHitFrame(this);
        }
    }
}
