using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionableFalling : actionable {

    override public void PositiveAction()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
    override public void NegativeAction()
    {
        //GetComponent<Rigidbody>().useGravity = false;
    }
}
