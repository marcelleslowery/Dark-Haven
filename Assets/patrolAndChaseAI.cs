using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrolAndChaseAI : MonoBehaviour {
    private float v;

    Animator animator;
    NavMeshAgent agent;
    public GameObject player;
    public List<GameObject> patrolPoints;
    public bool loopPatrol;

    private int patrolIndex;
    public float patrolPointTolerance = 1.0f;
    private int flipflop;

    public float maxPatrolSpeed = 5.0f;
    public float maxChaseSpeed = 10.0f;

    public float visionDistance = float.MaxValue;
    public float visionFrustrumTolerance = 0.8f;

    private bool frozen = false;

    State state;

    void Start () {
        flipflop = -1;
        state = State.PATROL;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        patrolIndex = 0;
        agent.SetDestination(patrolPoints[patrolIndex].transform.position);

        agent.updateRotation = true;
        agent.updatePosition = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if(frozen)
        {
            return;
        }
        switch(state)
        {
            case State.PATROL:

                checkIfPlayerSeen();

                if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) < patrolPointTolerance)
                {
                    if (loopPatrol)
                    {
                        patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
                    }
                    else
                    {
                        if(patrolIndex == 0 || patrolIndex == patrolPoints.Count - 1)
                        {
                            flipflop *= -1;
                        }

                        patrolIndex += flipflop;
                    }
                }

                agent.SetDestination(patrolPoints[patrolIndex].transform.position);
                v = Vector3.Distance(agent.nextPosition, transform.position);
                animator.SetFloat("H", 0);
                animator.SetFloat("V", v);
                animator.SetFloat("Speed", Mathf.Clamp(Mathf.Abs(v), 0, maxPatrolSpeed));

                if (v > agent.radius)
                {
                    agent.nextPosition = transform.position + 0.9f * (agent.nextPosition - transform.position);
                }
                break;

            case State.CHASE:
                agent.SetDestination(player.transform.position);
                v = Vector3.Distance(agent.nextPosition, transform.position);
                animator.SetFloat("H", 0);
                animator.SetFloat("V", v);
                animator.SetFloat("Speed", Mathf.Clamp(Mathf.Abs(v), 0, maxChaseSpeed));

                if (v > agent.radius)
                {
                    agent.nextPosition = transform.position + 0.9f * (agent.nextPosition - transform.position);
                }
                break;
        }
        
            

    }

    private void checkIfPlayerSeen()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < visionDistance && Vector3.Dot(transform.forward.normalized, (player.transform.position - transform.position).normalized) > visionFrustrumTolerance)
        {
            state = State.CHASE;
        }
    }

    private void OnDrawGizmos()
    {
        
        for(int i = 0; i < patrolPoints.Count; i++)
        {
            Gizmos.color = Color.blue;
            if(i == patrolIndex)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawCube(patrolPoints[i].transform.position, Vector3.one * 0.5f);
            if(i != patrolPoints.Count - 1 || loopPatrol)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(patrolPoints[i].transform.position, patrolPoints[(i + 1) % patrolPoints.Count].transform.position);
            }
            
        }

        if(state == State.CHASE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + Vector3.up * 2.0f, Vector3.one * 0.8f);
        }

    }

    public State getState()
    {
        return state;
    }

    public int getPatrolIndex()
    {
        return patrolIndex;
    }

    public void setState(State s)
    {
        state = s;
    }

    public void setPatrolIndex(int i)
    {
        patrolIndex = i;
    }

    public enum State
    {
        PATROL,
        CHASE
    }

    public struct save
    {
        public State s;
        public int pI;
        public int ff;
        public Vector3 aP;
        public Vector3 aV;
        public float t;
        public int l;
        public int sN;


        public save(State state, int patrolIndex, int flipflop, Vector3 agentPos, Vector3 agentVel, float time, int layer, int stateNameHash)
        {
            s = state;
            pI = patrolIndex;
            ff = flipflop;
            aP = agentPos;
            aV = agentVel;

            t = time;
            l = layer;
            sN = stateNameHash;
        }
    }

    public save getSave()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return new save(state, patrolIndex, flipflop, agent.nextPosition, agent.velocity, stateInfo.normalizedTime, 0, stateInfo.shortNameHash);
    }

    public void fromSave(save saveFile)
    {
        state = saveFile.s;
        patrolIndex = saveFile.pI;
        flipflop = saveFile.ff;
        agent.nextPosition = saveFile.aP;
        agent.velocity = saveFile.aV;
        animator.enabled = true;

        animator.Play(saveFile.sN, saveFile.l, saveFile.t);
        animator.Update(Time.deltaTime);
        animator.enabled = false;
    }

    public void freeze()
    {
        animator.enabled = false;
        agent.enabled = false;
        frozen = true;
    }

    public void unfreeze()
    {
        
        animator.enabled = true;
        agent.enabled = true;
        frozen = false;
    }
}
