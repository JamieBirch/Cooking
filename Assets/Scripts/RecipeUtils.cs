using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RecipeUtils
{
    public static int ingredientsInRecipe = 5;
    
    private static float allIngredientsDifferentMultiplier = 2;
    private static float twoIngredientMultiplier = 2;
    private static float threeIngredientMultiplier = 1.5f;
    private static float fourIngredientMultiplier = 1.25f;
    private static float defaultMultiplier = 1f;

    private static string fiveMeatDishName = "Мясо в собственном соку";
    private static string fourMeatDishName = "Мясо с гарниром";
    private static string twotreeMeatDishName = "Рагу";
    private static string noMeatDishName = "Овощное рагу";
    private static string fourfiveOnionDishName = "Луковый суп";
    private static string fourfivePotatoDishName = "Картофельное пюре";
    private static string defaultDishName = "Суп";
    
    private static string dishFormat = "{0} ({1}) [{2}]";


    private static Dictionary<IngredientType, string> IngredientTypeToRussianName =
        new()
        {
            {IngredientType.meat, "мясо"},
            {IngredientType.onion, "лук"},
            {IngredientType.potato, "картофель"},
            {IngredientType.carrot, "морковь"},
            {IngredientType.bellPepper, "перец"}
        };

    public static string DishRegistrationString(Recipe dish)
    {
        return String.Format(dishFormat, dish.Name, IngredientsToString(dish.Ingredients), dish.Score);
    }
    
    public static string IngredientsToString(Dictionary<IngredientType,int> ingredientGroupsByType)
    {
        return string.Join
        (
            ", ",
            ingredientGroupsByType.Select(pair => string.Format("{0} {1}", pair.Value, IngredientTypeToRussianName[pair.Key])).ToArray()
        );
    }
    
    public static int CalculateDishScore(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        int totalScore = 0;

        if (AllIngredientsAreDifferent(ingredientGroupsByType))
        {
            foreach (var ingredientGroup in ingredientGroupsByType)
            {
                totalScore += IngredientValueManager.ingredientValueDictionary[ingredientGroup.Key];
            }
            totalScore *= (int)allIngredientsDifferentMultiplier;
        }
        else
        {
            foreach (var ingredientGroup in ingredientGroupsByType)
            {
                float multiplier = GetMultiplier(ingredientGroup.Value);
                int currentIngredientScore = (int) (IngredientValueManager.ingredientValueDictionary[ingredientGroup.Key] * ingredientGroup.Value * multiplier);
                // Debug.Log("for " + ingredientGroup.Key + ": amount " + ingredientGroup.Value + ", ingredient score " + currentIngredientScore);
                totalScore += currentIngredientScore;
            }
        }
        // Debug.Log("total score for dish: " + totalScore);
        return totalScore;
    }

    private static bool AllIngredientsAreDifferent(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        return ingredientGroupsByType.Count() == ingredientsInRecipe;
    }

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
    
    public static string DefineDishName(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        //find dish name by onion
        if (ingredientGroupsByType.TryGetValue(IngredientType.onion, out int onionCount))
        {
            if (onionCount == 5 || onionCount == 4) //alternatively: >3
            {
                return fourfiveOnionDishName;
            }
        }

        //find dish name by potato
        if (ingredientGroupsByType.TryGetValue(IngredientType.potato, out int potatoCount))
        {
            if (potatoCount == 5 || potatoCount == 4) //alternatively: >3 
            {
                return fourfivePotatoDishName;
            }
        }
        
        //find dish name by meat
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
