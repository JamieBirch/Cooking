using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private int _ingredientCapacity = 5;
    private List<Ingredient> _ingredients = new();
    
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

    private void Update()
    {
        if (_ingredients.Count == _ingredientCapacity)
        {
            //group ingredients to Dictionary, where key is ingredient type and value is number of ingredients of this type
            Dictionary<IngredientType,int> ingredientGroupsByType = _ingredients
                .GroupBy(ingredient => ingredient.type)
                .ToDictionary(group => group.Key, group => group.Count());

            float dishScore = CalculateDishScore(ingredientGroupsByType);
            string dishName = DefineDishName(ingredientGroupsByType);
            Debug.Log(String.Format("Cooked {0} for {1} points.", dishName, dishScore) );
            GameManager.GameStatistic.RegisterNewDish(dishScore);
            EmptyPot();
        }
    }

    private string DefineDishName(Dictionary<IngredientType, int> ingredientGroupsByType)
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

    private void EmptyPot()
    {
        _ingredients = new();
    }

    private float CalculateDishScore(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        float totalScore = 0;

        if (AllIngredientsAreDifferent(ingredientGroupsByType))
        {
            foreach (Ingredient ingredient in _ingredients)
            {
                totalScore += IngredientValueManager.ingredientValueDictionary[ingredient.type];
            }
            totalScore *= allIngredientsDifferentMultiplier;
            Debug.Log("all ingredients different, total score for dish: " + totalScore);
        }
        else
        {
            foreach (var ingredientGroup in ingredientGroupsByType)
            {
                float multiplier = GetMultiplier(ingredientGroup.Value);
                float currentIngredientScore = IngredientValueManager.ingredientValueDictionary[ingredientGroup.Key] * multiplier;
                Debug.Log("for " + ingredientGroup.Key + ": amount " + ingredientGroup.Value + ", ingredient score " + currentIngredientScore);
                totalScore += currentIngredientScore;
            }
            Debug.Log("total score for dish: " + totalScore);
        }
        return totalScore;
    }

    private bool AllIngredientsAreDifferent(Dictionary<IngredientType, int> ingredientGroupsByType)
    {
        return ingredientGroupsByType.Count() == _ingredientCapacity;
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
        Debug.Log("Ingredient added: " + ingredient.type);
        _ingredients.Add(ingredient);
    }
}
