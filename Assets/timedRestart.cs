using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timedRestart : MonoBehaviour {
    public float timeUntilRestart = 20.0f;
	// Use this for initialization
	void Start () {
        Invoke("goToMain", timeUntilRestart);
	}

    public void goToMain()
    {
        SceneManager.LoadScene(0);
    }
}
