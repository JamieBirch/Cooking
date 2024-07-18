using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientValueManager : MonoBehaviour
{
    // Dictionary to store ingredient types and their corresponding values.
    public static Dictionary<IngredientType, int> IngredientValueDictionary = new();

    // Path to the JSON file containing ingredient values.
    public static string IngredientValueJsonPath = "Assets/ingredient_value.json";
    
    // Serializable class representing an ingredient's type and value.
    [Serializable]
    public class IngredientValue
    {
        public string type; // Type of the ingredient.
        public int value;   // Value associated with the ingredient.
    }
    
    // Serializable class to wrap an array of IngredientValue objects.
    [Serializable]
    private class IngredientValueArrayWrapper
    {
        public IngredientValue[] ingredients; // Array of IngredientValue objects.
    }
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // If IngredientValueDictionary is empty, load data from JSON file.
        if (IngredientValueDictionary.Count == 0)
        {
            string json = ExtractJsonString();

            // Check if JSON data was successfully extracted.
            if (!string.IsNullOrEmpty(json))
            {
                // Deserialize the JSON data to the IngredientValueArrayWrapper class.
                IngredientValueArrayWrapper ingredientDataWrapper = JsonUtility.FromJson<IngredientValueArrayWrapper>(json);

                // Populate the dictionary with the deserialized data.
                foreach (var ingredientValue in ingredientDataWrapper.ingredients)
                {
                    // Attempt to parse the ingredient type from string.
                    if (Enum.TryParse(ingredientValue.type, true, out IngredientType ingredientType))
                    {
                        IngredientValueDictionary[ingredientType] = ingredientValue.value;
                    }
                    else
                    {
                        Debug.LogWarning($"Unknown IngredientType: {ingredientValue.type}");
                    }
                }
            }
        }
    }

    // Extracts and returns the JSON string from the specified file path.
    private static string ExtractJsonString()
    {
        return File.ReadAllText(IngredientValueJsonPath);
    }
}
