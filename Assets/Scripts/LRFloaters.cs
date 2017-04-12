using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRFloaters : MonoBehaviour {

	Transform floater;
	Vector3 myPos;
	bool stopped;

	void Start () {
		
		floater = this.transform;
		myPos = new Vector3 (0.1f, 0, 0);
		stopped = false;
	}

	void LateUpdate () {

		if (floater.position.x >= 11f) {
			myPos.x = -0.1f;
		}
		if (floater.position.x <= -11f) {
			myPos.x = 0.1f;
		}

		if (stopped) {
			//nuffin
		} else {
			floater.position += myPos;
		}
	}
}
