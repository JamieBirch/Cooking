using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    // The content of the cauldron, responsible for visual representation
    public CauldronContent cauldronContent;

    // The list of ingredients currently in the cauldron.
    private static List<IngredientType> _ingredients = new();

    // Checks if the cauldron has enough ingredients to create a new recipe on every frame update.
    private void Update()
    {
        if (_ingredients.Count == RecipeUtils.ingredientsInRecipe)
        {
            CreateNewDish();
        }
    }

    // Empties the cauldron, removing all ingredients and clearing its content.
    public void EmptyCauldron()
    {
        _ingredients.Clear();
        cauldronContent.EmptyContent();
    }

    // Checks if the cauldron is empty.
    public bool IsEmpty()
    {
        return _ingredients.Count == 0;
    }

    // Handles collision with other objects, adding ingredients to the cauldron if they are not being held.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ingredient ingredient) && !ingredient.IsHeld())
        {
            AddIngredient(ingredient);
            Destroy(ingredient.gameObject);
        }
    }

    // Adds an ingredient to the cauldron and updates its content.
    private void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient.type);
        cauldronContent.AddIngredient(ingredient);
    }

    // Creates a new dish from the ingredients in the cauldron and registers it with the game statistics.
    private void CreateNewDish()
    {
        Recipe newDish = new Recipe(_ingredients);
        GameManager.GameStatistic.RegisterNewDish(newDish);
        EmptyCauldron();
    }
}