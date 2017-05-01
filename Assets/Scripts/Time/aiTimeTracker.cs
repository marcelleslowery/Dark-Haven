using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(patrolAndChaseAI))]
public class aiTimeTracker : MonoBehaviour, ITimeTracker
{
    public State state;

    protected List<frame> reel;
    protected int currFrameIndex;
    protected Rigidbody rB;

    private patrolAndChaseAI ai;

    private timeManager tM;

    virtual protected void Start()
    {
        state = State.PLAY;
        reel = new List<frame>();
        currFrameIndex = 0;
        ai = GetComponent<patrolAndChaseAI>();
        rB = GetComponent<Rigidbody>();
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
                ai.fromSave(reel[currFrameIndex].aiSave);
                break;
            case State.FASTFORWARD:
                currFrameIndex = Mathf.Clamp(currFrameIndex + Mathf.CeilToInt(tM.speed), 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                if (currFrameIndex == reel.Count - 1)
                {
                    Play();
                }
                ai.fromSave(reel[currFrameIndex].aiSave);
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
        f.velocity = rB.velocity;
        f.angularVelocity = rB.angularVelocity;
        f.aiSave = ai.getSave();
        return f;
    }

    virtual public void Rewind()
    {
        state = State.REWIND;
        rB.isKinematic = true;
        ai.freeze();
    }

    virtual public void Pause()
    {
        state = State.PAUSE;
        rB.isKinematic = true;
        ai.freeze();
    }

    virtual public void Play()
    {
        state = State.PLAY;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        rB.isKinematic = false;
        rB.velocity = reel[currFrameIndex].velocity;
        rB.angularVelocity = reel[currFrameIndex].angularVelocity;
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);
        ai.unfreeze();
        ai.fromSave(reel[currFrameIndex].aiSave);
        
    }

    virtual public void FastForward()
    {
        state = State.FASTFORWARD;
        rB.isKinematic = true;
        ai.freeze();
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
        public patrolAndChaseAI.save aiSave;
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
