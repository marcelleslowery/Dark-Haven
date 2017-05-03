using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeManager : MonoBehaviour
{

    List<timeTracker> trackedObjects;
    List<aiTimeTracker> trackedAI;
    playerTimeTracker playerTracker;
    cameraTimeTracker cameraTracker;
    public MagicTarget target;
    public float accelRate = 1.4f;

    public float speed = 0.0f;
    public float maxSpeed = 100.0f;

    public timeTracker.State state;
    private ResourcesManager rmanager;

    void Start()
    {
        state = timeTracker.State.PLAY;
        trackedObjects = new List<timeTracker>();
        trackedAI = new List<aiTimeTracker>();
        foreach (timeTracker t in FindObjectsOfType<timeTracker>())
        {
            trackedObjects.Add(t); 
        }
        playerTracker = FindObjectOfType<playerTimeTracker>();
        foreach(aiTimeTracker t in FindObjectsOfType<aiTimeTracker>())
        {
            trackedAI.Add(t);
        }
        cameraTracker = FindObjectOfType<cameraTimeTracker>();
        target = MagicTarget.ENVIRONMENT;
        rmanager = FindObjectOfType<ResourcesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Play") || Input.GetButtonUp("FastForward"))
        {
            Play();
        }

        if ((Input.GetButtonDown("Pause") || Input.GetButtonUp("Rewind")) && rmanager.Pauses > 0)
        {
            if (state != timeTracker.State.PAUSE)
            {
                rmanager.Pauses -= 1;
                rmanager.UpdateHUD();
            }
            Pause();
        }

        if (Input.GetButtonDown("Rewind") && rmanager.Rewinds > 0)
        {
            if (state != timeTracker.State.REWIND)
            {
                rmanager.Rewinds -= 1;
                rmanager.UpdateHUD();
            }
            Rewind();
        }

        if (Input.GetButtonDown("FastForward") && rmanager.FastForwards > 0)
        {
            if (state != timeTracker.State.FASTFORWARD)
            {
                rmanager.FastForwards -= 1;
                rmanager.UpdateHUD();
            }
            FastForward();
        }

        if (Input.GetButtonDown("SwitchMagicTarget"))
        {
            target = (MagicTarget) (((int) target + 1) % System.Enum.GetNames(typeof(MagicTarget)).Length);
        }

        if(state == timeTracker.State.FASTFORWARD || state == timeTracker.State.REWIND)
        {
            speed = Mathf.Clamp(speed + accelRate * Time.deltaTime, 0.0f, maxSpeed);
        }
    }

    public void Play()
    {
        state = timeTracker.State.PLAY;
        speed = 0.0f;
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Play();
            }
            foreach (aiTimeTracker t in trackedAI)
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
        state = timeTracker.State.PAUSE;
        speed = 0.0f;
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Pause();
            }
            foreach (aiTimeTracker t in trackedAI)
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
        state = timeTracker.State.REWIND;
        speed = 0.0f;
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.Rewind();
            }
            foreach (aiTimeTracker t in trackedAI)
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
        state = timeTracker.State.FASTFORWARD;
        speed = 0.0f;
        if (target == MagicTarget.ENVIRONMENT)
        {
            foreach (timeTracker t in trackedObjects)
            {
                t.FastForward();
            }
            foreach (aiTimeTracker t in trackedAI)
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
    
    public void setMagicTarget(MagicTarget t)
    {
        Play();
        target = t;
    }

    public enum MagicTarget
    {
        PLAYER,
        ENVIRONMENT
    }

    public void subscribeToTime(MonoBehaviour t)
    {
        if(t is rigidBodyTimeTracker)
        {
            trackedObjects.Add((rigidBodyTimeTracker) t);
        }
        else if (t is aiTimeTracker)
        {
            trackedAI.Add((aiTimeTracker) t);
        }
    }

    public void unsubscribeFromTime(MonoBehaviour t)
    {
        if (t is rigidBodyTimeTracker)
        {
            trackedObjects.Remove((rigidBodyTimeTracker)t);
        }
        else if (t is aiTimeTracker)
        {
            trackedAI.Remove((aiTimeTracker)t);
        }
    }


}
