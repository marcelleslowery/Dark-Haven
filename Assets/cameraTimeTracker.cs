using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(ColorCorrectionCurves))]
public class cameraTimeTracker : MonoBehaviour, ITimeTracker
{
    ColorCorrectionCurves ccc;
    void Start () {
        ccc = GetComponent<ColorCorrectionCurves>();
        Play();
    }

    public void Play()
    {
        setColorCorrection(0f, 1f, 0f, 1f, 0f, 1f, 1f);
    }

    public void Pause()
    {
        setColorCorrection(0f, 1f, 0f, 1f, 0f, 1f, 0.5f);
    }

    public void Rewind()
    {
        setColorCorrection(0f, .8f, 0f, .8f, .2f, 1f, 0.5f);
    }

    public void FastForward()
    {
        setColorCorrection(.2f, 1f, 0f, .8f, 0f, .8f, 1.5f);
    }

    private void setColorCorrection(float rBegin, float rEnd, float gBegin, float gEnd, float bBegin, float bEnd, float saturation)
    {
        ccc.redChannel.MoveKey(0, new Keyframe(0, rBegin));
        ccc.redChannel.MoveKey(1, new Keyframe(1, rEnd));

        ccc.redChannel.SmoothTangents(0, 0);
        ccc.redChannel.SmoothTangents(1, 0);


        ccc.greenChannel.MoveKey(0, new Keyframe(0, gBegin));
        ccc.greenChannel.MoveKey(1, new Keyframe(1, gEnd));

        ccc.greenChannel.SmoothTangents(0, 0);
        ccc.greenChannel.SmoothTangents(1, 0);

        ccc.blueChannel.MoveKey(0, new Keyframe(0, bBegin));
        ccc.blueChannel.MoveKey(1, new Keyframe(1, bEnd));

        ccc.blueChannel.SmoothTangents(0, 0);
        ccc.blueChannel.SmoothTangents(1, 0);

        ccc.saturation = saturation;

        ccc.UpdateParameters();
    }
    

}
