using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestActionController : MonoBehaviour {

    timeTracker tT;
    Animator anim;
	// Use this for initialization
	void Start () {
        tT = GetComponent<timeTracker>();
        anim = GetComponent<Animator>();
	}
	
	void OnTriggerEnter(Collider col)
    {
        anim.SetBool("Close", false);
        anim.SetBool("Open", true);
    }

    void OnTriggerExit(Collider col)
    {
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
    }
}
