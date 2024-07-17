using UnityEngine;

public class CauldronIngredient : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;

    public void UpdateSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    
    public void SetToDefault()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}
