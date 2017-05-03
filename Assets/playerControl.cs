using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    private float h;                                // Horizontal Axis.
    private float v;                                // Vertical Axis.
    private bool sprint;
    private bool jump;
    private float distToGround;
    public float turnSmoothing = 3.0f;
    public float JumpSpeed = 5.0f;
    public float JumpThreshold = 1.0f;

    private float jumpTimer;

    public Transform playerCamera;
    private ThirdPersonOrbitCam camScript;

    private Vector3 lastDirection;

    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        camScript = playerCamera.GetComponent<ThirdPersonOrbitCam>();
        jumpTimer = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward);
        forward.y = 0.0f;
        forward = forward.normalized;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        sprint = Input.GetButton("Sprint");
        jump = Input.GetButton("Jump");
        Rotating(h, v);
        animator.SetFloat("H", h * (sprint ? 0f : 1f));
        animator.SetFloat("V", v * (sprint ? 1f : .5f));
        animator.SetFloat("Speed", (Mathf.Abs(v) + Mathf.Abs(h)) * (sprint ? 1f: .5f));
        animator.SetBool("Jump", jump);
        animator.SetBool("Grounded", Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f));

        jumpTimer += Time.deltaTime;
    }

    Vector3 Rotating(float horizontal, float vertical)
    {
        // Get camera forward direction, without vertical component.
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward);

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

        // Lerp current direction to calculated target direction.
        if ((horizontal != 0 || vertical != 0) && targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            transform.rotation = newRotation;
            lastDirection = targetDirection;
        }
        // If idle, Ignore current camera facing and consider last moving direction.
        if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
        {
            Repositioning();
        }

        return targetDirection;
    }

    void Repositioning()
    {
        if (lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            transform.rotation = newRotation;
        }
    }

    void Jumping()
    {
        bool grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        if (grounded && jumpTimer >= JumpThreshold)
        {
            Debug.Log("JUMP");
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3(0, JumpSpeed, 0);
            jumpTimer = 0f;
        }
    }

}
