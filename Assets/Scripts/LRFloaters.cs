using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(timeTracker))]
public class LRFloaters : MonoBehaviour {

	Transform floater;
	Vector3 myPos;
	bool stopped;

    timeTracker tT;

	void Start () {
		
		floater = this.transform;
		myPos = new Vector3 (0.1f, 0, 0);
		stopped = false;
        tT = GetComponent<timeTracker>();
	}

	void LateUpdate () {

        if(tT.IsTimeMoving())
        {
            if (floater.position.x >= 11f)
            {
                myPos.x = -0.1f;
            }
            if (floater.position.x <= -11f)
            {
                myPos.x = 0.1f;
            }

            if (stopped)
            {
                //nuffin
            }
            else
            {
                floater.position += myPos;
            }
        }

		
	}
}
