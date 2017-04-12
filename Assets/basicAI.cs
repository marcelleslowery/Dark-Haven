using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicAI : MonoBehaviour {
    private float v;                                // Vertical Axis.
    private bool sprint;
    //private bool jump;
    //private float distToGround;

    Animator animator;
    NavMeshAgent agent;
    public GameObject goal;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //distToGround = GetComponent<Collider>().bounds.extents.y;
        agent.SetDestination(goal.transform.position);
        agent.updateRotation = true;
        agent.updatePosition = false;
    }
	
	// Update is called once per frame
	void Update () {

        //jump = Input.GetButton("Jump");
        agent.SetDestination(goal.transform.position);
        v = Vector3.Distance(agent.nextPosition, transform.position);
        animator.SetFloat("H", 0);
        animator.SetFloat("V", v);
        animator.SetFloat("Speed", Mathf.Abs(v));
        //animator.SetBool("Jump", jump);
        //animator.SetBool("Grounded", Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f));
        if (v > agent.radius)
        {
            agent.nextPosition = transform.position + 0.9f * (agent.nextPosition - transform.position);
        }
            

    }
}
