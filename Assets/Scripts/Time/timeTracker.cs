using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeTracker
{
    void Rewind();
    void Pause();
    void Play();
    void FastForward();
}

public class timeTracker : MonoBehaviour, ITimeTracker
{
    
    public State state;

    protected List<frame> reel;
    protected int currFrameIndex;

    protected timeManager tM;


    virtual protected void Start()
    {
        state = State.PLAY;
        reel = new List<frame>();
        currFrameIndex = 0;
        tM = FindObjectOfType<timeManager>();
    }

    virtual protected void Update()
    {
        switch (state)
        {
            case State.PLAY:
                reel.Add(generateFrame());
                currFrameIndex++;
                break;
            case State.PAUSE:
                break;
            case State.REWIND:
                currFrameIndex = Mathf.Clamp(currFrameIndex - Mathf.CeilToInt(tM.speed), 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                break;
            case State.FASTFORWARD:
                currFrameIndex = Mathf.Clamp(currFrameIndex + Mathf.CeilToInt(tM.speed), 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                if (currFrameIndex == reel.Count - 1)
                {
                    Play();
                }
                break;
            default:
                break;
        }
    }

    virtual protected frame generateFrame()
    {
        frame f = new frame();
        f.position = transform.position;
        f.rotation = transform.rotation;
        return f;
    }

    virtual public void Rewind()
    {
        state = State.REWIND;
    }

    virtual public void Pause()
    {
        state = State.PAUSE;
    }

    virtual public void Play()
    {
        state = State.PLAY;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);
    }

    virtual public void FastForward()
    {
        state = State.FASTFORWARD;
    }

    public enum State
    {
        PLAY,
        PAUSE,
        REWIND,
        FASTFORWARD
    }

    protected struct frame
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            int i = 0;
            frame lastF = reel[0];
            foreach (frame f in reel)
            {
                Gizmos.color = Color.HSVToRGB((float)i / reel.Count, 1, 1);
                Gizmos.DrawLine(f.position, (lastF.position - f.position) * 0.3f + f.position);
                lastF = f;
                i++;
            }
        }
    }

    public bool IsTimeMoving()
    {
        return state == State.PLAY;
    }
}
