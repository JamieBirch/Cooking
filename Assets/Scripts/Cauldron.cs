using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public CauldronContent cauldronContent;
    
    private List<IngredientType> _ingredients = new();

    private void Update()
    {
        if (_ingredients.Count == RecipeUtils.ingredientsInRecipe)
        {
            //group ingredients to Dictionary, where key is ingredient type and value is number of ingredients of this type
            Dictionary<IngredientType,int> ingredientGroupsByType = _ingredients
                .GroupBy(ingredient => ingredient)
                .ToDictionary(group => group.Key, group => group.Count());

            float dishScore = RecipeUtils.CalculateDishScore(ingredientGroupsByType);
            string dishName = RecipeUtils.DefineDishName(ingredientGroupsByType);
            string dishIngredients = RecipeUtils.IngredientsToString(ingredientGroupsByType);
            GameManager.GameStatistic.RegisterNewDish(dishName, dishIngredients, dishScore);
            EmptyCauldron();
        }
    }

    public void EmptyCauldron()
    {
        _ingredients = new();
        cauldronContent.EmptyContent();
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
