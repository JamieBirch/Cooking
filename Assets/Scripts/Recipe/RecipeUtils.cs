using System;
using System.Collections.Generic;
using System.Linq;

public static class RecipeUtils
{
    // Number of ingredients in a recipe.
    public static int ingredientsInRecipe = 5;
    
    // Multipliers for different ingredient counts.
    private static float allIngredientsDifferentMultiplier = 2;
    private static float twoIngredientMultiplier = 2;
    private static float threeIngredientMultiplier = 1.5f;
    private static float fourIngredientMultiplier = 1.25f;
    private static float defaultMultiplier = 1f;

    // Dish names for specific ingredient compositions.
    private static string fiveMeatDishName = "Мясо в собственном соку";
    private static string fourMeatDishName = "Мясо с гарниром";
    private static string twotreeMeatDishName = "Рагу";
    private static string noMeatDishName = "Овощное рагу";
    private static string fourfiveOnionDishName = "Луковый суп";
    private static string fourfivePotatoDishName = "Картофельное пюре";
    private static string defaultDishName = "Суп";
    
    // Format for displaying a dish registration string.
    private static string dishFormat = "{0} ({1}) [{2}]";

    // Mapping from IngredientType to Russian names.
    private static Dictionary<IngredientType, string> IngredientTypeToRussianName = new Dictionary<IngredientType, string>
    {
        {IngredientType.meat, "мясо"},
        {IngredientType.onion, "лук"},
        {IngredientType.potato, "картофель"},
        {IngredientType.carrot, "морковь"},
        {IngredientType.bellPepper, "перец"}
    };

    // Generates a string representation of a dish's registration details.
    public static string DishRegistrationString(Recipe dish)
    {
        return String.Format(dishFormat, dish.Name, IngredientsToString(dish.Ingredients), dish.Score);
    }
    
    // Converts ingredient groups to a formatted string.
    public static string IngredientsToString(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        return string.Join(", ",
            ingredientGroupsByType.Select(pair => string.Format("{0} {1}", pair.Value, IngredientTypeToRussianName[pair.Key])).ToArray());
    }
    
    // Calculates the score for a dish based on ingredient groups.
    public static int CalculateDishScore(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        int totalScore = 0;

        if (AllIngredientsAreDifferent(ingredientGroupsByType))
        {
            foreach (var ingredientGroup in ingredientGroupsByType)
            {
                totalScore += IngredientValueManager.IngredientValueDictionary[ingredientGroup.Key];
            }
            totalScore *= (int)allIngredientsDifferentMultiplier;
        }
        else
        {
            foreach (var ingredientGroup in ingredientGroupsByType)
            {
                float multiplier = GetMultiplier(ingredientGroup.Value);
                int currentIngredientScore = (int) (IngredientValueManager.IngredientValueDictionary[ingredientGroup.Key] * ingredientGroup.Value * multiplier);
                totalScore += currentIngredientScore;
            }
        }

        return totalScore;
    }

    // Checks if all ingredients in a dish are different.
    private static bool AllIngredientsAreDifferent(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        return ingredientGroupsByType.Count() == ingredientsInRecipe;
    }

    // Retrieves the multiplier based on the number of ingredients.
    private static float GetMultiplier(int ingredientCount)
    {
        switch (ingredientCount)
        {
            case 2:
                return twoIngredientMultiplier;
            case 3:
                return threeIngredientMultiplier;
            case 4:
                return fourIngredientMultiplier;
            default:
                return defaultMultiplier;
        }
    }
    
    // Determines the name of the dish based on ingredient groups.
    public static string DefineDishName(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        // Check for dish name based on onion count.
        if (ingredientGroupsByType.TryGetValue(IngredientType.onion, out int onionCount))
        {
            if (onionCount == 5 || onionCount == 4)
            {
                return fourfiveOnionDishName;
            }
        }

        // Check for dish name based on potato count.
        if (ingredientGroupsByType.TryGetValue(IngredientType.potato, out int potatoCount))
        {
            if (potatoCount == 5 || potatoCount == 4)
            {
                return fourfivePotatoDishName;
            }
        }
        
        // Check for dish name based on meat count.
        if (ingredientGroupsByType.TryGetValue(IngredientType.meat, out int meatCount))
        {
            switch (meatCount)
            {
                case 5:
                    return fiveMeatDishName;
                case 4:
                    return fourMeatDishName;
                case 3:
                case 2:
                    return twotreeMeatDishName;
                default:
                    break;
            }
        }
        else
        {
            return noMeatDishName;
        }

        return defaultDishName;
    }
}
