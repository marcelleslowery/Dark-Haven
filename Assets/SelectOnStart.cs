using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOnStart : MonoBehaviour {
    public Selectable StartingSelection;
	// Use this for initialization
	void Start () {
        StartingSelection.Select();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
