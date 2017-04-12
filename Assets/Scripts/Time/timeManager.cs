using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeManager : MonoBehaviour
{

    List<timeTracker> trackedObjects;
    playerTimeTracker playerTracker;
    cameraTimeTracker cameraTracker;
    MagicTarget target;
    void Start()
    {
        trackedObjects = new List<timeTracker>();
        foreach (timeTracker t in FindObjectsOfType<timeTracker>())
        {
            if(t is playerTimeTracker)
            {
                playerTracker = (playerTimeTracker) t;
            }
            else
            {
                trackedObjects.Add(t);
            }
            
        }
        cameraTracker = FindObjectOfType<cameraTimeTracker>();
        target = MagicTarget.ENVIRONMENT;
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

        if(Input.GetButtonDown("SwitchMagicTarget"))
        {
            target = (MagicTarget) (((int) target + 1) % System.Enum.GetNames(typeof(MagicTarget)).Length);
        }
    }

    public void Play()
    {
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Play();
            }
        }
        else
        {
            playerTracker.Play();
        }
        cameraTracker.Play();
    }

    public void Pause()
    {
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Pause();
            }
        }
        else
        {
            playerTracker.Pause();
        }
        cameraTracker.Pause();
    }

    public void Rewind()
    {
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Rewind();
            }
        }
        else
        {
            playerTracker.Rewind();
        }
        cameraTracker.Rewind();
    }

    public void FastForward()
    {
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.FastForward();
            }
        }
        else
        {
            playerTracker.FastForward();
        }
        cameraTracker.FastForward();
    }

    public enum MagicTarget
    {
        PLAYER,
        ENVIRONMENT
    }


}
