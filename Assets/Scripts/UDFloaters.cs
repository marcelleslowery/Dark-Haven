using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDFloaters : MonoBehaviour {

	Transform floater;
	Vector3 myPos;
	bool stopped;

	void Start () {

		floater = this.transform;
		myPos = new Vector3 (0, 0.1f, 0);
		stopped = false;
	}

	void Update () {

		if (floater.position.y >= 18f) {
			myPos.y = -0.1f;
		}
		if (floater.position.y <= 7f) {
			myPos.y = 0.1f;
		}

		if (stopped) {
			//nuffin
		} else {
			floater.position += myPos;
		}
	}
}
