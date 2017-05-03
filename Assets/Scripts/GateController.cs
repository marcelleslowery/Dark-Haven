using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(timeTracker))]
[RequireComponent(typeof(AudioSource))]
public class GateController : MonoBehaviour {

	Transform gate;
	Vector3 myPos;
	bool stopped;

	AudioSource myAudio;
	public AudioClip hit;
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

    timeTracker tT;

    void Start () {

		gate = this.transform;
		myPos = new Vector3 (0, 0.1f, 0);
		stopped = false;
		myAudio = gameObject.GetComponent<AudioSource>();
        tT = GetComponent<timeTracker>();
    }

	void Update () {
        if (!tT.IsTimeMoving())
        {
            return;
        }
		if (gate.position.y >= 11f) {
			myPos.y = -0.5f;
		}
		if (gate.position.y <= 4f) {
			Invoke ("PlaySmash", 0);
			myPos.y = 0.2f;
		}

		if (stopped) {
			//nuffin
		} else {
			gate.position += myPos;
		}
	}

	void PlaySmash(){
		currentVolume = (volume + UnityEngine.Random.Range (-volumeVariance, volumeVariance));
		pitch = (1.0f + Random.Range (-pitchVariance, pitchVariance));
		myAudio.pitch = pitch;
		myAudio.PlayOneShot (hit, currentVolume);
	}
}
