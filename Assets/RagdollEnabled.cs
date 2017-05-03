using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RagdollEnabled : MonoBehaviour {

    Rigidbody[] bodyComponents;
	// Use this for initialization
	void Start () {
        bodyComponents = gameObject.GetComponentsInChildren<Rigidbody>();
	}
	
	// Update is called once per frame
	public void Kill () {
		foreach (Rigidbody comp in bodyComponents)
        {
            comp.isKinematic = false;
        }
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        Invoke("Reset", 5f);
	}

    public void Reset ()
    {
        SceneManager.LoadScene("BetterCharacter");
    }
}
