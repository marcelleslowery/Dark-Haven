using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTracker : MonoBehaviour {

    timeManager tM;

	void Start () {
        tM = FindObjectOfType<timeManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathTrigger")
        {
            tM.setMagicTarget(timeManager.MagicTarget.PLAYER);
            tM.Pause();
        }
        
    }
}
