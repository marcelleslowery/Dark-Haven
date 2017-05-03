using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPlayerHud : MonoBehaviour {
    public Sprite playSprite;
    public Sprite ffSprite;
    public Sprite rwSprite;
    public Sprite pauseSprite;
    public Sprite onPlayerSprite;
    public Sprite onEnvironmentSprite;

    SpriteRenderer sR;
    TextMesh resourceCountText;
    SpriteRenderer magicTargetIndicator;
    timeManager tM;

    // Use this for initialization
    void Start () {
        sR = GetComponentsInChildren<SpriteRenderer>()[0];
        tM = FindObjectOfType<timeManager>();
        resourceCountText = GetComponentInChildren<TextMesh>();
        magicTargetIndicator = GetComponentsInChildren<SpriteRenderer>()[1];

    }
	
	// Update is called once per frame
	void Update () {
		switch(tM.state)
        {
            case timeTracker.State.PLAY:
                sR.sprite = playSprite;
                break;
            case timeTracker.State.PAUSE:
                sR.sprite = pauseSprite;
                break;
            case timeTracker.State.FASTFORWARD:
                sR.sprite = ffSprite;
                break;
            case timeTracker.State.REWIND:
                sR.sprite = rwSprite;
                break;
            default:
                break;
        }

        switch(tM.target)
        {
            case timeManager.MagicTarget.ENVIRONMENT:
                magicTargetIndicator.sprite = onEnvironmentSprite;
                break;

            case timeManager.MagicTarget.PLAYER:
                magicTargetIndicator.sprite = onPlayerSprite;
                break;
        }

        resourceCountText.text = 12.ToString();
	}
}
