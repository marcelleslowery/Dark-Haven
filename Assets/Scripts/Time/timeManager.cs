using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timeManager : MonoBehaviour
{

    List<timeTracker> trackedObjects;
    List<aiTimeTracker> trackedAI;
    playerTimeTracker playerTracker;
    cameraTimeTracker cameraTracker;
    MagicTarget target;
    public float accelRate = 0.1f;

    public float speed = 0.0f;
    public float maxSpeed = 100.0f;

    private timeTracker.State state;

    void Start()
    {
        state = timeTracker.State.PLAY;
        trackedObjects = new List<timeTracker>();
        trackedAI = new List<aiTimeTracker>();
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
        foreach(aiTimeTracker t in FindObjectsOfType<aiTimeTracker>())
        {
            trackedAI.Add(t);
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

    public enum MagicTarget
    {
        PLAYER,
        ENVIRONMENT
    }


}
