using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

	Transform myCam;
	Vector3 camPos;

	// Use this for initialization
	void Start () {
		myCam = this.transform;
		camPos = new Vector3 (0f, 0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		myCam.position += camPos;
	}
}
