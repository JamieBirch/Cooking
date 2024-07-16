using UnityEngine;

public class CauldronContent : MonoBehaviour
{
    public CauldronIngredient[] cauldronIngredients;
    private int ingredientCount = 0;

    public void AddIngredient(Ingredient ingredient)
    {
        // Debug.Log("add " + ingredient.type);
        cauldronIngredients[ingredientCount].UpdateSprite(ingredient.sprite);
        ingredientCount++;
    }

    public void EmptyContent()
    {
        ingredientCount = 0;
        foreach (CauldronIngredient cauldronIngredient in cauldronIngredients)
        {
            cauldronIngredient.SetToDefault();
        }
    }
}
