using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	Transform gate;
	Vector3 myPos;
	bool stopped;

	void Start () {

		gate = this.transform;
		myPos = new Vector3 (0, 0.1f, 0);
		stopped = false;
	}

	void Update () {

		if (gate.position.y >= 11f) {
			myPos.y = -0.5f;
		}
		if (gate.position.y <= 4f) {
			myPos.y = 0.2f;
		}

		if (stopped) {
			//nuffin
		} else {
			gate.position += myPos;
		}
	}
}
