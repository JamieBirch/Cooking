using System.Collections.Generic;
using UnityEngine;

public static class RecipeGenerator
{
    public static List<Recipe> GenerateCombinations(int ingredientsInRecipe, IngredientType[] ingredientTypes)
    {
        List<Recipe> allRecipes = new();
        
        //we need to create all possible combinations of 5-ingredient recipe => with repetitions, order is not important
        Debug.Log("start combining");

        IngredientType[] recipe = new IngredientType [ingredientsInRecipe]; //buffer for instrumental purposes
        combineIngredients(0, 0, ingredientTypes, recipe, allRecipes); //recursive

        return allRecipes;
    }
    
    //recursively fills recipe array with ingredients, going through ingredient types
    private static void combineIngredients(int recipeIngredientCurrentIndex, int startWithIndex,
        IngredientType[] ingredientTypes, IngredientType[] recipe, List<Recipe> allRecipes)
    {
        for (int ingredientIndex = startWithIndex; ingredientIndex < ingredientTypes.Length; ingredientIndex++)
        {
            recipe [recipeIngredientCurrentIndex] = ingredientTypes[ingredientIndex];
            if (recipeIngredientCurrentIndex + 1 < recipe.Length)
            {
                combineIngredients(recipeIngredientCurrentIndex + 1, ingredientIndex, ingredientTypes, recipe, allRecipes); //recursive call
            }
            else
            {
                allRecipes.Add(new Recipe(recipe));
                // Debug.Log(string.Join(",", recipe));
            }
        }
    }
}
