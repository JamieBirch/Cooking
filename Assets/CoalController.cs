using System.Collections;
using UnityEngine;

public class CoalController : MonoBehaviour
{
    public CoalPreset idlePreset;
    public CoalPreset hotPreset;

    public Cauldron cauldron;

    private CoalPreset currentPreset;
    private bool hasIngredients;
    private float timeElapsed;

    public SpriteRenderer spriteRendererHot;

    private void Start()
    {
        SetPreset(idlePreset);
    }

    private void Update()
    {
        hasIngredients = CheckIfHasIngredients();
        
        if (hasIngredients && currentPreset != hotPreset)
        {
            StartCoroutine(SwitchPreset(hotPreset)); // switch to hot
        }
        else if (!hasIngredients && currentPreset != idlePreset)
        {
            StartCoroutine(SwitchPreset(idlePreset)); //switch to idle
        }
    }

    private void SetPreset(CoalPreset preset)
    {
        currentPreset = preset;
        timeElapsed = 0f;
    }

    private IEnumerator SwitchPreset(CoalPreset newPreset)
    {
        float switchDuration = newPreset.animationTime;
        float switchTime = 0f;

        while (switchTime < switchDuration)
        {
            switchTime += Time.deltaTime;
            float t = switchTime / switchDuration;
            float alpha = newPreset.brightnessCurve.Evaluate(t);
            spriteRendererHot.color = new Color(1f,1f,1f, alpha); //update transparency of hot coals sprite

            yield return null;
        }

        SetPreset(newPreset);
    }

    private bool CheckIfHasIngredients()
    {
        return !cauldron.isEmpty();
    }
}
