using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour {
    public int sceneIndex = -1;
    private void OnTriggerEnter(Collider other)
    {
        if (sceneIndex == -1)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }
        
        SceneManager.LoadScene(sceneIndex);
    }
}
