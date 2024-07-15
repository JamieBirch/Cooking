using System;
using System.Collections.Generic;
using UnityEngine;

public class IngredientValueManager : MonoBehaviour
{
    public static IngredientValueManager instance;
    public static Dictionary<IngredientType, int> ingredientValueDictionary = new();
    public IngredientValue[] ingredientValues;
    
    [Serializable]
    public class IngredientValue
    {
        public IngredientType type;
        public int value;
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        foreach (IngredientValue ingredientValue in ingredientValues)
        {
            ingredientValueDictionary.Add(ingredientValue.type, ingredientValue.value);
        }
    }
}
