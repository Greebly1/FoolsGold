using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BlendableRig : Rig
{
    float _inputWeight = 0;
    float inputWeight
    {
        get { return _inputWeight; }
        set { _inputWeight = Mathf.Clamp01(value); }
    }

    public float lerpingTime = 1;
    float smooothdampvelocity = 0;
    bool islerping = false;
    Coroutine WeightLerper;

    private void Awake()
    {
        inputWeight = weight;
    }
    public void SmoothdampLayerWeight(float targetWeight)
    {
        inputWeight = targetWeight;

        if (!islerping) { WeightLerper = StartCoroutine("WeightLerp"); }
    }

    IEnumerator WeightLerp()
    {
        islerping = true;

        while (weight != inputWeight)
        {
            weight = Mathf.SmoothDamp(weight, inputWeight, ref smooothdampvelocity, lerpingTime);

            yield return null;
        }

        islerping = false;
    }
}
