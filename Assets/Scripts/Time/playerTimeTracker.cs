using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LazyMovement))]
public class playerTimeTracker : rigidBodyTimeTracker
{
    public override void Pause()
    {
        state = State.PAUSE;
        base.rB.isKinematic = false;
    }
}
