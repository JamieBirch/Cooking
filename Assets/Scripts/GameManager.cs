using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Cauldron cauldron;

    public Text ScoreText;
    public Text LastDishText;
    public Text BestDishText;


    private static string scoreSaveKey = "Score";
    private static string lastDishSaveKey = "LastDish";
    private static string bestDishSaveKey = "BestDish";
    private static string bestDishScoreSaveKey = "BestDishScore";
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadData();
    }

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

    public void Reload()
    {
        PurgeSave();
        ReloadScene();
    }

    public void LoadData()
    {
        UpdateUI(
            PlayerPrefs.GetFloat(scoreSaveKey).ToString(),
            PlayerPrefs.GetString(lastDishSaveKey),
            PlayerPrefs.GetString(bestDishSaveKey)
            );
        GameStatistic.UpdateBestScore(PlayerPrefs.GetInt(bestDishScoreSaveKey));
    }

    private void UpdateUI(string score, string lastDish, string bestDish)
    {
        ScoreText.text = score;
        LastDishText.text = lastDish;
        BestDishText.text = bestDish;
    }

    /*private void UpdateUI()
    {
        ScoreText.text = PlayerPrefs.GetFloat(scoreSaveKey).ToString();
        LastDishText.text = PlayerPrefs.GetString(lastDishSaveKey);
        BestDishText.text = PlayerPrefs.GetString(bestDishSaveKey);
    }*/

    private void PurgeSave()
    {
        PlayerPrefs.DeleteAll();
        UpdateUI(
            PlayerPrefs.GetFloat(scoreSaveKey).ToString(),
            PlayerPrefs.GetString(lastDishSaveKey),
            PlayerPrefs.GetString(bestDishSaveKey)
        );
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public class GameStatistic
    {
        private static int Score = 0;
        private static int bestDishScore = 0;

        public static void RegisterNewDish(Recipe newDish)
        {
            string dishRegistrationString = RecipeUtils.DishRegistrationString(newDish);
            // compare with best dish
            if (newDish.Score > bestDishScore)
            {
                bestDishScore = newDish.Score;
                instance.BestDishText.text = dishRegistrationString;
                PlayerPrefs.SetString(bestDishSaveKey, dishRegistrationString);
                PlayerPrefs.SetInt(bestDishScoreSaveKey, newDish.Score);
            }
            Score += newDish.Score;
            PlayerPrefs.SetFloat(scoreSaveKey, Score);
            PlayerPrefs.SetString(lastDishSaveKey, dishRegistrationString);
            instance.UpdateUI(
                    PlayerPrefs.GetFloat(scoreSaveKey).ToString(),
                    PlayerPrefs.GetString(lastDishSaveKey),
                    PlayerPrefs.GetString(bestDishSaveKey)
                );
        }

        public static void UpdateBestScore(int newBestDishScore)
        {
            bestDishScore = newBestDishScore;
        }
    }
}