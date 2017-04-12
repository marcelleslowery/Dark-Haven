using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(timeTracker))]
public class LRFloaters : MonoBehaviour {

	Transform floater;
	Vector3 myPos;
	bool stopped;

    public float width = 20.0f;
    public float speed = 1.0f;
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
            if (floater.position.x >= width / 2.0f)
            {
                myPos.x = -speed * Time.deltaTime;
            }
            if (floater.position.x <= -width / 2.0f)
            {
                myPos.x = speed * Time.deltaTime;
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
