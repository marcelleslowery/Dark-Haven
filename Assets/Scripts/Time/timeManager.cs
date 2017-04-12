using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeManager : MonoBehaviour
{

    List<timeTracker> trackedObjects;
    cameraTimeTracker cameraTracker;
    void Start()
    {
        trackedObjects = new List<timeTracker>();
        foreach (timeTracker t in FindObjectsOfType<timeTracker>())
        {
            trackedObjects.Add(t);
        }
        cameraTracker = FindObjectOfType<cameraTimeTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Play") || Input.GetButtonUp("FastForward"))
        {
            Play();
        }

        if (Input.GetButtonDown("Pause") || Input.GetButtonUp("Rewind"))
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
        cameraTracker.Play();
    }

    public void Pause()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.Pause();
        }
        cameraTracker.Pause();
    }

    public void Rewind()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.Rewind();
        }
        cameraTracker.Rewind();
    }

    public void FastForward()
    {
        foreach (timeTracker t in trackedObjects)
        {
            t.FastForward();
        }
        cameraTracker.FastForward();
    }


}
