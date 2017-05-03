/*
Team Lionheart

Sara Jacks

Marcelles Lowery

Mathew Deeb
  
Alex Poole

John Blum
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnPath : MonoBehaviour {

    public List<Transform> path;
    public float speed;
    private int currentPt;
	void Start () {
        currentPt = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (transform.position + speed * Time.deltaTime * (path[(currentPt + 1) % path.Count].position - transform.position).normalized);
        if(Vector3.Distance(transform.position, path[(currentPt + 1) % path.Count].position) < speed * Time.deltaTime)
        {
            currentPt++;
        }
	}
}
