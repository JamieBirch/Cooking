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
            //TODO cook
            //increase count
            //remember dish
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touches Pot");
        if (other.TryGetComponent(out Ingredient ingredient))
        {
            PutIn(ingredient);
            Destroy(ingredient.gameObject);
        }
    }

    private void PutIn(Ingredient ingredient)
    {
        Debug.Log("Ingredient added");
        _ingredients.Add(ingredient);
    }
}
