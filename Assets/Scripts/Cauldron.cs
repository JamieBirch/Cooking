using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public CauldronContent cauldronContent;
    
    private List<IngredientType> _ingredients = new();

    private void Update()
    {
        if (_ingredients.Count == RecipeUtils.ingredientsInRecipe)
        {
            Recipe newDish = new Recipe(_ingredients);
            GameManager.GameStatistic.RegisterNewDish(newDish);
            EmptyCauldron();
        }
    }

    public void EmptyCauldron()
    {
        _ingredients = new();
        cauldronContent.EmptyContent();
    }

    public bool isEmpty()
    {
        return _ingredients.Count == 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO highlight
        // Debug.Log("Touches Pot");
        if (other.TryGetComponent(out Ingredient ingredient))
        {
            if (!ingredient.IsHeld())
            {
                Put(ingredient);
                Destroy(ingredient.gameObject);
            }
        }
    }

    private void Put(Ingredient ingredient)
    {
        _ingredients.Add(ingredient.type);
        cauldronContent.AddIngredient(ingredient);
    }
}
