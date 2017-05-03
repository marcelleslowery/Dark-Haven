using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from footsteps tutorial: https://www.youtube.com/watch?v=ISoBKFxQLic

[RequireComponent (typeof (AudioSource))]

public class Footsteps : MonoBehaviour {

	AudioSource myAudio;
	Animator myAnim;
	public AudioClip floor;
	public GameObject leftFoot;
	public GameObject rightFoot;

	[Space(5.0f)]
	private float currentVolume;
	[Range(0.0f,1.0f)]
	public float volume = 0.15f;
	[Range(0.0f,0.2f)]
	public float volumeVariance = 0.095f;
	private float pitch;
	[Range(0.0f,0.2f)]
	public float pitchVariance = 0.08f;
	[Space(5.0f)]

	float currentFootstepLeft;
	float lastFootstepLeft;
	float currentFootstepRight;
	float lastFootstepRight;

	void Start () {

		myAudio = gameObject.GetComponent<AudioSource>();
		myAnim = GetComponent<Animator> ();
	}

	void OnCollisionStay(Collision obj) {
		//Left foot
		currentFootstepLeft = myAnim.GetFloat ("FootstepLeft");
		if (currentFootstepLeft > 0f && lastFootstepLeft < 0f) {

			CheckGround (obj.gameObject, leftFoot);
		}
		lastFootstepLeft = myAnim.GetFloat ("FootstepLeft");

		//Right foot
		currentFootstepRight = myAnim.GetFloat ("FootstepRight");
		if (currentFootstepRight < 0f && lastFootstepRight > 0f) {

			CheckGround (obj.gameObject, rightFoot);
		}
		lastFootstepRight = myAnim.GetFloat ("FootstepRight");
	}

	void CheckGround(GameObject floor, GameObject currentFoot){

		if (floor.tag == ("Floor")) {
			Invoke ("PlayConcrete", 0);
		}
	}

	void PlayConcrete(){
		currentVolume = (volume + UnityEngine.Random.Range (-volumeVariance, volumeVariance));
		pitch = (1.0f + Random.Range (-pitchVariance, pitchVariance));
		myAudio.pitch = pitch;
		myAudio.PlayOneShot (floor, currentVolume);
	}
}
