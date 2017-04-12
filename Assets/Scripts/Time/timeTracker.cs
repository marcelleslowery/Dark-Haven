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

[RequireComponent(typeof(Rigidbody))]
public class timeTracker : MonoBehaviour, ITimeTracker
{

    Rigidbody rB;
    public State state;

    List<frame> reel;
    int currFrameIndex;

    int ffSpeed = 4;


    void Start()
    {
        rB = GetComponent<Rigidbody>();
        state = State.PLAY;
        reel = new List<frame>();
        currFrameIndex = 0;

    }

    void Update()
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
                currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                break;
            case State.FASTFORWARD:
                currFrameIndex = Mathf.Clamp(currFrameIndex + ffSpeed, 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                if (currFrameIndex == reel.Count - 1)
                {
                    Pause();
                }
                break;
            default:
                break;
        }
    }

    frame generateFrame()
    {
        frame f = new frame();
        f.position = transform.position;
        f.rotation = transform.rotation;
        f.velocity = rB.velocity;
        f.angularVelocity = rB.angularVelocity;
        return f;
    }

    public void Rewind()
    {
        state = State.REWIND;
        rB.isKinematic = true;
    }

    public void Pause()
    {
        state = State.PAUSE;
        rB.isKinematic = true;
    }

    public void Play()
    {
        state = State.PLAY;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        rB.isKinematic = false;
        rB.velocity = reel[currFrameIndex].velocity;
        rB.angularVelocity = reel[currFrameIndex].angularVelocity;
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);
    }

    public void FastForward()
    {
        state = State.FASTFORWARD;
        rB.isKinematic = true;
    }

    public enum State
    {
        PLAY,
        PAUSE,
        REWIND,
        FASTFORWARD
    }

    struct frame
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
