using UnityEngine;

public class CauldronIngredient : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite defaultSprite;

    public void UpdateSprite(Sprite sprite)
    {
        renderer.sprite = sprite;
    }
    
    public void SetToDefault()
    {
        renderer.sprite = defaultSprite;
    }
}
