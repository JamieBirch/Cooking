using System.Collections.Generic;
using System.Linq;

public class Recipe
{
    public int Score;
    public string Name;
    public Dictionary<IngredientType, int> Ingredients;

    public Recipe(Dictionary<IngredientType, int> ingredients)
    {
        Ingredients = ingredients;
        Score = RecipeUtils.CalculateDishScore(ingredients);
        Name = RecipeUtils.DefineDishName(ingredients);
    }
    
    public Recipe(IEnumerable<IngredientType> recipeIngredients)
    {
        Dictionary<IngredientType,int> ingredients = recipeIngredients
            .GroupBy(ingredient => ingredient)
            .ToDictionary(group => group.Key, group => group.Count());

        Ingredients = ingredients;
        Score = RecipeUtils.CalculateDishScore(ingredients);
        Name = RecipeUtils.DefineDishName(ingredients);
    }
}