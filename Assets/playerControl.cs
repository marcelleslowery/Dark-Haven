using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    private float h;                                // Horizontal Axis.
    private float v;                                // Vertical Axis.
    private bool sprint;
    private bool jump;
    private float distToGround;

    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        sprint = Input.GetButton("Sprint");
        jump = Input.GetButton("Jump");

        animator.SetFloat("H", h);
        animator.SetFloat("V", v);
        animator.SetFloat("Speed", Mathf.Abs(v) + Mathf.Abs(h));
        animator.SetBool("Jump", jump);
        animator.SetBool("Grounded", Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f));
    }
}
