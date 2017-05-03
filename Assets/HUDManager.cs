using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Button SwitchTargetButton;
    public Button PlayButton;
    public Button PauseButton;
    public Button RewindButton;
    public Button FastForwardButton;

    public Text PausesLeft;
    public Text RewindsLeft;
    public Text FastForwardsLeft;

    public Text ObjectiveText;

    // Use this for initialization
    void Start () {
        PlayButton.interactable = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Play") || Input.GetButtonUp("FastForward"))
        {
            PlayButton.interactable = true;
            PauseButton.interactable = false;
            RewindButton.interactable = false;
            FastForwardButton.interactable = false;
            PlayButton.Select();
        } 

        if (Input.GetButtonDown("Pause") || Input.GetButtonUp("Rewind"))
        {
            PlayButton.interactable = false;
            PauseButton.interactable = true;
            RewindButton.interactable = false;
            FastForwardButton.interactable = false;
            PauseButton.Select();
        }

        if (Input.GetButtonDown("Rewind"))
        {
            PlayButton.interactable = false;
            PauseButton.interactable = false;
            RewindButton.interactable = true;
            FastForwardButton.interactable = false;
            RewindButton.Select();
        }
        if (Input.GetButtonDown("FastForward"))
        {
            PlayButton.interactable = false;
            PauseButton.interactable = false;
            RewindButton.interactable = false;
            FastForwardButton.interactable = true;
            FastForwardButton.Select();
        }

        if (Input.GetButtonDown("SwitchMagicTarget"))
        {
            SwitchTargetButton.interactable = !SwitchTargetButton.interactable;
            SwitchTargetButton.Select();
        }

        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateHUDValues(int Pauses, int Rewinds, int FastForwards)
    {
        PausesLeft.text = "Uses Left: " + Pauses;
        RewindsLeft.text = "Uses Left: " + Rewinds;
        FastForwardsLeft.text = "Uses Left: " + FastForwards;
    }
}
