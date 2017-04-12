using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeManager : MonoBehaviour
{

    List<timeTracker> trackedObjects;
    void Start()
    {
        trackedObjects = new List<timeTracker>();
        foreach (timeTracker t in FindObjectsOfType<timeTracker>())
        {
            trackedObjects.Add(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Play"))
        {
            Play();
        }

        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (Input.GetButtonDown("Rewind"))
        {
            Rewind();
        }

        if (Input.GetButtonDown("FastForward"))
        {
            FastForward();
        }
    }

    public void Play()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.Play();
        }
    }

    public void Pause()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.Pause();
        }
    }

    public void Rewind()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.Rewind();
        }
    }

    public void FastForward()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.FastForward();
        }
    }


}
