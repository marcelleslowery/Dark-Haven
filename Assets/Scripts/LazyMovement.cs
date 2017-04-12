using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyMovement : MonoBehaviour {
    public Transform playerCamera;                 // Reference to the camera that focus the player.
    public float turnSmoothing = 3.0f;             // Speed of turn when moving to match camera facing.
    public float WalkSpeed = 5.0f;
    public float SprintSpeed = 10.0f;
    public float JumpSpeed = 1.0f;
    public float JumpThreshold = 1.0f;

    private ThirdPersonOrbitCam camScript;         // Reference to the third person camera script
    private Vector3 lastDirection;                 // Last direction the player was moving.
    private Rigidbody rbody;                       // Reference to the player's rigidbody.
    private float h;                                // Horizontal Axis.
    private float v;                                // Vertical Axis.
    private bool sprint;
    private bool jump;
    private float distToGround;
    private float jumpTimer;


    // Use this for initialization
    void Start () {
		camScript = playerCamera.GetComponent<ThirdPersonOrbitCam> ();
        rbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        jumpTimer = 1.0f;
    }

    // Update is called once per frame
    void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        sprint = Input.GetButton("Sprint");
        jump = Input.GetButton("Jump");

        // Won't be here if we use root motion!!!
        Movement(h, v, sprint);
        if (jump)
        {
            Jumping();
        }
        Rotating(h, v);

        jumpTimer += Time.deltaTime;
    }

    void Movement(float horizontal, float vertical, bool sprintOn)
    {
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward);

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

        transform.position += targetDirection * Time.deltaTime * (sprintOn ? SprintSpeed : WalkSpeed);
    }

    void Jumping()
    {
        bool grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        if (grounded && jumpTimer >= JumpThreshold)
        {
            Debug.Log("JUMP");
            rbody.velocity = rbody.velocity + new Vector3(0, JumpSpeed, 0);
            jumpTimer = 0f;
        }
    }

    // Put the player on a standing up position based on last direction faced.
    void Repositioning()
    {
        if (lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(rbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            rbody.MoveRotation(newRotation);
        }
    }

    // Rotate the player to match correct orientation, according to camera and key pressed.
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
        float finalTurnSmoothing;
        targetDirection = forward * vertical + right * horizontal;
        finalTurnSmoothing = turnSmoothing;

        // Lerp current direction to calculated target direction.
        if ((horizontal != 0 || vertical != 0) && targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(rbody.rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
            rbody.MoveRotation(newRotation);
            lastDirection = targetDirection;
        }
        // If idle, Ignore current camera facing and consider last moving direction.
        if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
        {
            Repositioning();
        }

        return targetDirection;
    }
}
