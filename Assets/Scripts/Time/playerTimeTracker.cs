using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LazyMovement))]
public class playerTimeTracker : rigidBodyTimeTracker
{
    public override void Pause()
    {
        state = State.PLAY;
        rB.isKinematic = false;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);
        GetComponent<LazyMovement>().enabled = false;
    }

    public override void Play()
    {
        GetComponent<LazyMovement>().enabled = true;
        base.Play();
    }
}
