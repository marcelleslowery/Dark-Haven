﻿using UnityEngine;
using System.Collections;
using System;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
    public float fallingThreshold = 0.3f;
    public float fallDeathThreshold = 0.5f;

	private float speed;                            // Moving speed.
	private bool run;                               // Boolean to determine whether or not the player activated the run mode.

	// Start is always called after any Awake functions.
	void Start() 
	{
		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour (this);
		behaviourManager.RegisterDefaultBehaviour (this.behaviourCode);
    }

    // Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		if(behaviourManager.IsMoving())
		{
			if(behaviourManager.isSprinting())
			{
				speed = sprintSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
		}
		else
		{
			speed = 0f;
		}
		anim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		float finalTurnSmoothing;
		targetDirection = forward * vertical + right * horizontal;
		finalTurnSmoothing = behaviourManager.turnSmoothing;

		// Lerp current direction to calculated target direction.
		if((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);

			Quaternion newRotation = Quaternion.Slerp(rbody.rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			rbody.MoveRotation (newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if(!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

    public override void LocalFixedUpdate()
    {
        MovementManagement(behaviourManager.GetH, behaviourManager.GetV);
    }
}
