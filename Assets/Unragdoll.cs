using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unragdoll : MonoBehaviour {

    private Rigidbody playerRigidbody;
    private Animator playerAnimator;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        SetKinematic(true);
    }


    public void SetKinematic(bool newValue)
    {

        //Get an array of components that are of type Rigidbody
        Component[] components = GetComponentsInChildren(typeof(Rigidbody));

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic and detectCollisions property
        foreach (Component c in components)
        {
            (c as Rigidbody).isKinematic = newValue;
            (c as Rigidbody).detectCollisions = !newValue;
        }

        //Sets PLAYER rigid body as opposite
        playerRigidbody.isKinematic = !newValue;
        playerRigidbody.detectCollisions = newValue;
        playerAnimator.enabled = newValue;

    }
}
