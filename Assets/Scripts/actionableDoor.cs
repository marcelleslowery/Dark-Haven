using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionable
{
    void PositiveAction();
    void NegativeAction();
}

public class actionable : MonoBehaviour, IActionable
{
    public virtual void PositiveAction()
    {
    }
    public virtual void NegativeAction()
    {
    }
}
public class actionableDoor : actionable {

    override public void PositiveAction()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        foreach(MeshRenderer mR in GetComponentsInChildren<MeshRenderer>())
        {
            mR.enabled = false;
        }
    }
    override public void NegativeAction()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        foreach (MeshRenderer mR in GetComponentsInChildren<MeshRenderer>())
        {
            mR.enabled = true;
        }
    }
}
