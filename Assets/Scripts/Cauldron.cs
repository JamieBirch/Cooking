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

    private void Update()
    {
        if (_ingredients.Count == _ingredientCapacity)
        {
            float dishScore = CalculateDishScore();
            GameManager.GameStatistic.RegisterNewDish(dishScore);
            EmptyPot();
        }
    }

    private void EmptyPot()
    {
        _ingredients = new();
    }

    private float CalculateDishScore()
    {
        float totalScore = 0;

        var ingredientGroupsByType = _ingredients
            .GroupBy(ingredient => ingredient.type);
        
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
                float multiplier = GetMultiplier(ingredientGroup.Count());
                float currentIngredientScore = IngredientValueManager.ingredientValueDictionary[ingredientGroup.Key] * multiplier;
                Debug.Log("for " + ingredientGroup.Key + ": amount " + ingredientGroup.Count() + ", ingredient score " + currentIngredientScore);
                totalScore += currentIngredientScore;
            }
            Debug.Log("total score for dish: " + totalScore);
        }
        return totalScore;
    }

    private bool AllIngredientsAreDifferent(IEnumerable<IGrouping<IngredientType, Ingredient>> ingredientGroupsByType)
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
