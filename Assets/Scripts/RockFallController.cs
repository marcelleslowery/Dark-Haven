using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallController : MonoBehaviour {

    //public GameObject[] rocks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "EndTrigger")
        {
            //print(rocks.Length);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.tag = "EndTrigger";
            //for (int i = 0; i < rocks.Length; i++)
            //{
            // rocks[i].GetComponent<Rigidbody>().isKinematic = true;
            //rocks[i].GetComponent<Rigidbody>().useGravity = false;
            //}
        }
    }
}
