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
        StartCoroutine(AnimateEmbers());
    }

    private void Update()
    {
        hasIngredients = CheckIfHasIngredients();
        
        if (hasIngredients && currentPreset != hotPreset)
        {
            StartCoroutine(SwitchPreset(hotPreset));
        }
        else if (!hasIngredients && currentPreset != idlePreset)
        {
            StartCoroutine(SwitchPreset(idlePreset));
        }
    }

    private void SetPreset(CoalPreset preset)
    {
        currentPreset = preset;
        timeElapsed = 0f;
    }

    private IEnumerator SwitchPreset(CoalPreset newPreset)
    {
        float switchDuration = 1.0f;
        float switchTime = 0f;
        
        while (switchTime < switchDuration)
        {
            switchTime += Time.deltaTime;
            float t = switchTime / switchDuration;
            
            spriteRendererHot.color = new Color(1f, 1f, 1f, t);

            yield return null;
        }

        SetPreset(newPreset);
    }

    private IEnumerator AnimateEmbers()
    {
        while (true)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / currentPreset.animationTime;

            if (normalizedTime >= 1f)
            {
                timeElapsed = 0f;
            }

            yield return null;
        }
    }

    private bool CheckIfHasIngredients()
    {
        return !cauldron.isEmpty();
    }
}
