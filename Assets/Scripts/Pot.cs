using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private int _ingredientCapacity = 5;
    private List<Ingredient> _ingredients = new();

    private void Update()
    {
        if (_ingredients.Count == _ingredientCapacity)
        {
            int dishScore = CalculateDishScore();
            GameManager.GameStatistic.RegisterNewDish(dishScore);
            EmptyPot();
        }
    }

    private void EmptyPot()
    {
        _ingredients = new();
    }

    private int CalculateDishScore()
    {
        int totalScore = 0;
        //calculate score for dish
        foreach (Ingredient ingredient in _ingredients)
        {
            totalScore += IngredientValueManager.ingredientValueDictionary[ingredient.type];
        }
        return totalScore;
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
