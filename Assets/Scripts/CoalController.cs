using System.Collections;
using UnityEngine;

public class CoalController : MonoBehaviour
{
    // The preset used when the coals are idle.
    public CoalPreset idlePreset;
    
    // The preset used when the coals are hot.
    public CoalPreset hotPreset;

    // Reference to the cauldron to check for ingredients.
    public Cauldron cauldron;

    // The current preset being used.
    private CoalPreset _currentPreset;
    
    // Flag to determine if the cauldron has ingredients.
    private bool _hasIngredients;

    // Reference to the SpriteRenderer for the hot coals.
    public SpriteRenderer spriteRendererHot;

    // Initialize the coals with the idle preset.
    private void Start()
    {
        SetPreset(idlePreset);
    }

    // Check for ingredients in the cauldron and switch presets accordingly.
    private void Update()
    {
        _hasIngredients = CheckIfHasIngredients();
        
        if (_hasIngredients && _currentPreset != hotPreset)
        {
            StartCoroutine(SwitchPreset(hotPreset)); // switch to hot
        }
        else if (!_hasIngredients && _currentPreset != idlePreset)
        {
            StartCoroutine(SwitchPreset(idlePreset)); // switch to idle
        }
    }

    // Sets the current preset to the given preset.
    private void SetPreset(CoalPreset preset)
    {
        _currentPreset = preset;
    }

    // Coroutine to switch the coal preset with a transition effect.
    private IEnumerator SwitchPreset(CoalPreset newPreset)
    {
        float switchDuration = newPreset.animationTime;
        float switchTime = 0f;

        while (switchTime < switchDuration)
        {
            switchTime += Time.deltaTime;
            float t = switchTime / switchDuration;
            float alpha = newPreset.brightnessCurve.Evaluate(t);
            spriteRendererHot.color = new Color(1f, 1f, 1f, alpha); // update transparency of hot coals sprite

            yield return null;
        }

        SetPreset(newPreset);
    }

    // Checks if the cauldron has ingredients.
    private bool CheckIfHasIngredients()
    {
        return !cauldron.IsEmpty();
    }
}
