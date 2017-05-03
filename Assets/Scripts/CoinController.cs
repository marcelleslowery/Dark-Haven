using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    timeTracker tT;
    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        tT = GetComponent<timeTracker>();
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        audio.Play();
    }

}
