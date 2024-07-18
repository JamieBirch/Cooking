using UnityEngine;

public class CauldronContent : MonoBehaviour
{
    // Array of CauldronIngredient representing the visual components of the ingredients in the cauldron.
    public CauldronIngredient[] cauldronIngredients;

    // Counter to keep track of the number of ingredients added to the cauldron.
    private int _ingredientCount = 0;

    // Adds an ingredient to the cauldron and updates its visual representation.
    public void AddIngredient(Ingredient ingredient)
    {
        if (_ingredientCount < cauldronIngredients.Length)
        {
            cauldronIngredients[_ingredientCount].UpdateSprite(ingredient.sprite);
            _ingredientCount++;
        }
        else
        {
            Debug.LogWarning("Cauldron is full, cannot add more ingredients.");
        }
    }

    // Empties the cauldron content, resetting all visual components to their default state.
    public void EmptyContent()
    {
        _ingredientCount = 0;
        foreach (CauldronIngredient cauldronIngredient in cauldronIngredients)
        {
            cauldronIngredient.SetToDefault();
        }
    }
}