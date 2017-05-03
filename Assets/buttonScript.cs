using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {

    Vector3 upPosition;
    public float buttonDepth = 0.5f;
    public List<actionable> actionableList;
	AudioSource myAudio;
	public AudioClip hit;

    private void Start()
    {
		myAudio = gameObject.GetComponent<AudioSource>();
        upPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = upPosition + new Vector3(0, -buttonDepth, 0);
        foreach (actionable a in actionableList)
        {
            a.PositiveAction();
        }
		Invoke ("PlaySmash", 0);
    }

    private void OnTriggerStay(Collider other)
    {
        transform.position = upPosition + new Vector3(0, -buttonDepth, 0);
        foreach (actionable a in actionableList)
        {
            a.PositiveAction();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.position = upPosition;
        foreach (actionable a in actionableList)
        {
            a.NegativeAction();
        }
    }

	void PlaySmash(){
		myAudio.PlayOneShot (hit);
	}
}
