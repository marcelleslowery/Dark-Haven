using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class rigidBodyTimeTracker : timeTracker
{

    protected Rigidbody rB;
    public bool permanent = true;
    public bool destroyAtZero = false;

    override protected void Start()
    {
        rB = GetComponent<Rigidbody>();
        base.state = State.PLAY;
        base.reel = new List<frame>();
        base.currFrameIndex = 0;
        base.tM = FindObjectOfType<timeManager>();
    }

    override protected void Update()
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
                if(!permanent)
                {
                    if(currFrameIndex <= 0 && destroyAtZero)
                    {
                        tM.unsubscribeFromTime(this);
                        Destroy(this.gameObject);
                    }

                    rB.isKinematic = !reel[currFrameIndex].exists;
                    this.GetComponent<Renderer>().enabled = reel[currFrameIndex].exists;
                    this.GetComponent<Collider>().enabled = reel[currFrameIndex].exists;
                }
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;

                break;
            case State.FASTFORWARD:
                if (currFrameIndex == reel.Count - 1)
                {
                    tM.Play();
                    break;
                }
                currFrameIndex = Mathf.Clamp(currFrameIndex + Mathf.CeilToInt(tM.speed), 0, reel.Count - 1);
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;
                break;
            default:
                break;
        }
    }

    override protected frame generateFrame()
    {
        frame f = new frame();
        f.position = transform.position;
        f.rotation = transform.rotation;
        f.velocity = rB.velocity;
        f.angularVelocity = rB.angularVelocity;
        f.gravity = rB.useGravity;
        try { f.exists = this.GetComponent<Renderer>().enabled; }
        catch { f.exists = true; };
        return f;
    }

    override public void Rewind()
    {
        state = State.REWIND;
        rB.isKinematic = true;
    }

    override public void Pause()
    {
        state = State.PAUSE;
        rB.isKinematic = true;
    }

    override public void Play()
    {
        state = State.PLAY;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        rB.isKinematic = false;
        rB.velocity = reel[currFrameIndex].velocity;
        rB.angularVelocity = reel[currFrameIndex].angularVelocity;
        rB.useGravity = reel[currFrameIndex].gravity;
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);
    }

    override public void FastForward()
    {
        state = State.FASTFORWARD;
        rB.isKinematic = true;
    }
}
