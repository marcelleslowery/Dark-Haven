using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {

    Vector3 upPosition;
    public float buttonDepth = 0.5f;
    public List<actionable> actionableList;
    private void Start()
    {
        upPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = upPosition + new Vector3(0, -buttonDepth, 0);
        foreach(actionable a in actionableList)
        {
            a.PositiveAction();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.position = upPosition;
        foreach (actionable a in actionableList)
        {
            a.NegativeAction();
        }
    }
}
