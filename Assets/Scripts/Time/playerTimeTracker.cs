using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BasicBehaviour))]
[RequireComponent(typeof(MoveBehaviour))]
public class playerTimeTracker : MonoBehaviour, ITimeTracker
{
    public State state;

    protected List<frame> reel;
    protected int currFrameIndex;
    protected Rigidbody rB;

    private timeManager tM;
    private Animator animator;

    virtual public void Pause()
    {
        GetComponent<BasicBehaviour>().enabled = false;
        GetComponent<MoveBehaviour>().enabled = false;
        state = State.PAUSE;
        rB.isKinematic = true;
        animator.enabled = false;
    }

    virtual public void Play()
    {
        GetComponent<BasicBehaviour>().enabled = true;
        GetComponent<MoveBehaviour>().enabled = true;
        state = State.PLAY;
        currFrameIndex = Mathf.Clamp(currFrameIndex - 1, 0, reel.Count - 1);
        rB.isKinematic = false;
        rB.velocity = reel[currFrameIndex].velocity;
        rB.angularVelocity = reel[currFrameIndex].angularVelocity;
        reel.RemoveRange(currFrameIndex + 1, reel.Count - currFrameIndex - 1);

        animator.enabled = true;
        animator.Play(reel[currFrameIndex].stateHash, reel[currFrameIndex].layer, reel[currFrameIndex].normTime);
        animator.Update(Time.deltaTime);
    }


    virtual protected void Start()
    {
        state = State.PLAY;
        reel = new List<frame>();
        currFrameIndex = 0;
        rB = GetComponent<Rigidbody>();
        tM = FindObjectOfType<timeManager>();
        animator = GetComponent<Animator>();
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
                Debug.Log(Mathf.CeilToInt(tM.speed));
                transform.position = reel[currFrameIndex].position;
                transform.rotation = reel[currFrameIndex].rotation;

                animator.enabled = true;
                animator.Play(reel[currFrameIndex].stateHash, reel[currFrameIndex].layer, reel[currFrameIndex].normTime);
                animator.Update(Time.deltaTime);
                animator.enabled = false;
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

                animator.enabled = true;
                animator.Play(reel[currFrameIndex].stateHash, reel[currFrameIndex].layer, reel[currFrameIndex].normTime);
                animator.Update(Time.deltaTime);
                animator.enabled = false;
                break;
            default:
                break;
        }
    }

    virtual protected frame generateFrame()
    {
        frame f = new frame();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        f.position = transform.position;
        f.rotation = transform.rotation;
        f.velocity = rB.velocity;
        f.angularVelocity = rB.angularVelocity;
        f.normTime = stateInfo.normalizedTime;
        f.layer = 0;
        f.stateHash = stateInfo.shortNameHash;
        return f;
    }

    virtual public void Rewind()
    {
        state = State.REWIND;
        rB.isKinematic = true;
        animator.enabled = false;
    }



    virtual public void FastForward()
    {
        state = State.FASTFORWARD;
        rB.isKinematic = true;
        animator.enabled = false;
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
        public float normTime;
        public int layer;
        public int stateHash;
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