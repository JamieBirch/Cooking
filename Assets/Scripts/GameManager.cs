using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager.
    public static GameManager instance;

    // Reference to the Cauldron in the game.
    public Cauldron cauldron;

    // UI elements to display score and dish information.
    public Text ScoreText;
    public Text LastDishText;
    public Text BestDishText;

    // Keys for saving and loading data using PlayerPrefs.
    private static string scoreSaveKey = "Score";
    private static string lastDishSaveKey = "LastDish";
    private static string bestDishSaveKey = "BestDish";
    private static string bestDishScoreSaveKey = "BestDishScore";
    
    // Initializes the GameManager instance and loads saved data on awake.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadData();
    }

    // Handles input to reload the scene, empty the cauldron, or log recipe combinations.
    private void Update()
    {
        if (Input.GetKey("r"))
        {
            Reload();
        }
        
        if (Input.GetKey("l"))
        {
            cauldron.EmptyCauldron();
            LoadData();
        }
        
        if (Input.GetKey("t"))
        {
            List<Recipe> allRecipes = RecipeGenerator.GenerateCombinations(RecipeUtils.ingredientsInRecipe, Enum.GetValues(typeof(IngredientType)) as IngredientType[]);
            allRecipes.OrderBy(recipe => recipe.Score)
                .ToList()
                .ForEach(recipe => Debug.Log(RecipeUtils.DishRegistrationString(recipe)));
        }
    }

    // Reloads the scene and purges saved data.
    public void Reload()
    {
        PurgeSave();
        ReloadScene();
    }

    // Loads saved data from PlayerPrefs and updates the UI accordingly.
    public void LoadData()
    {
        UpdateUI();
        GameStatistic.UpdateBestScore(PlayerPrefs.GetInt(bestDishScoreSaveKey));
    }

    // Updates the UI elements with new score and dish information.
    private void UpdateUI()
    {
        ScoreText.text = PlayerPrefs.GetInt(scoreSaveKey).ToString();
        LastDishText.text = PlayerPrefs.GetString(lastDishSaveKey);
        BestDishText.text = PlayerPrefs.GetString(bestDishSaveKey);
    }

    // Purges all saved data and updates the UI.
    private void PurgeSave()
    {
        PlayerPrefs.DeleteAll();
        UpdateUI();
    }
    
    // Reloads the current scene.
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Nested class to handle game statistics like score and best dish.
    public class GameStatistic
    {
        private static int Score = 0;
        private static int bestDishScore = 0;

        // Registers a new dish, updates scores, and saves data if it's the best dish.
        public static void RegisterNewDish(Recipe newDish)
        {
            string dishRegistrationString = RecipeUtils.DishRegistrationString(newDish);
            
            // Compare with best dish and update if current dish is better.
            if (newDish.Score > bestDishScore)
            {
                bestDishScore = newDish.Score;
                instance.BestDishText.text = dishRegistrationString;
                PlayerPrefs.SetString(bestDishSaveKey, dishRegistrationString);
                PlayerPrefs.SetInt(bestDishScoreSaveKey, newDish.Score);
            }
            
            Score += newDish.Score;
            PlayerPrefs.SetInt(scoreSaveKey, Score);
            PlayerPrefs.SetString(lastDishSaveKey, dishRegistrationString);
            instance.UpdateUI();
        }

        // Updates the best dish score.
        public static void UpdateBestScore(int newBestDishScore)
        {
            bestDishScore = newBestDishScore;
        }
    }
}
