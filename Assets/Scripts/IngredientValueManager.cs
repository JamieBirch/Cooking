using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientValueManager : MonoBehaviour
{
    public static Dictionary<IngredientType, int> ingredientValueDictionary = new();
    public static string IngredientValueJsonPath = "Assets/ingredient_value.json";
    
    [Serializable]
    public class IngredientValue
    {
        public string type;
        public int value;
    }
    
    [Serializable]
    private class IngredientValueArrayWrapper
    {
        public IngredientValue[] ingredients;
    }
    
    private void Awake()
    {
        if (ingredientValueDictionary.Count == 0)
        {
            string json = ExtractJsonString();

            if (!string.IsNullOrEmpty(json))
            {
                // Deserialize the JSON data to the IngredientValueArrayWrapper class
                IngredientValueArrayWrapper ingredientDataWrapper = JsonUtility.FromJson<IngredientValueArrayWrapper>(json);

                // Populate the dictionary with the deserialized data
                foreach (var ingredientValue in ingredientDataWrapper.ingredients)
                {
                    if (Enum.TryParse(ingredientValue.type, true, out IngredientType ingredientType))
                    {
                        ingredientValueDictionary[ingredientType] = ingredientValue.value;
                    }
                    else
                    {
                        Debug.LogWarning($"Unknown IngredientType: {ingredientValue.type}");
                    }
                }
            }
        }
    }

    private static string ExtractJsonString()
    {
        return File.ReadAllText(IngredientValueJsonPath);
    }
}