using UnityEngine;

[CreateAssetMenu(fileName = "CoalPreset", menuName = "ScriptableObjects/CoalPreset", order = 1)]
public class CoalPreset : ScriptableObject
{
    public float animationTime;
    public AnimationCurve brightnessCurve;
}
