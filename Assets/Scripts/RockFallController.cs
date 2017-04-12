using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallController : MonoBehaviour {

    private Vector3 lastpos;
    private bool falling = false;
	// Use this for initialization
	void Start () {
        lastpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPos = transform.position;
        if (!falling && (lastpos.y > currentPos.y))
        {
            falling = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
        lastpos = currentPos;
	}

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "EndTrigger")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.tag = "EndTrigger";
        }
    }
}
