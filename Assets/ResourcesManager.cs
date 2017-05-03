using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour {

    public int Pauses;
    public int Rewinds;
    public int FastForwards;
    public string LevelObjective;

    private HUDManager hudmanager;
	// Use this for initialization
	void Start () {
        hudmanager = GetComponent<HUDManager>();
        hudmanager.UpdateHUDValues(Pauses, Rewinds, FastForwards);
        if (hudmanager.ObjectiveText != null)
        {
            hudmanager.ObjectiveText.text = LevelObjective;
        }
	}
	
	// Update is called once per frame
	public void UpdateHUD () {
        hudmanager.UpdateHUDValues(Pauses, Rewinds, FastForwards);
	}
}
