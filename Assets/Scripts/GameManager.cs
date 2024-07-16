using System;
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

    private static string dishFormat = "{0} ({1}) [{2}]";

    private static string scoreSaveKey = "Score";
    private static string lastDishSaveKey = "LastDish";
    private static string bestDishSaveKey = "BestDish";
    
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
        private static float Score = 0;
        private static float bestDishScore = 0;

        public static void RegisterNewDish(string dishName, string dishIngredients, float dishScore)
        {
            Score += dishScore;
            string dishRegistrationString = String.Format(dishFormat, dishName, dishIngredients, dishScore);
            // compare with best dish
            if (dishScore > bestDishScore)
            {
                bestDishScore = dishScore;
                // instance.BestDishText.text = dishRegistrationString;
                PlayerPrefs.SetString(bestDishSaveKey, dishRegistrationString);
            }
            // instance.ScoreText.text = Score.ToString();
            // instance.LastDishText.text = dishRegistrationString;
            PlayerPrefs.SetFloat(scoreSaveKey, Score);
            PlayerPrefs.SetString(lastDishSaveKey, dishRegistrationString);
            instance.UpdateUI(
                    PlayerPrefs.GetFloat(scoreSaveKey).ToString(),
                    PlayerPrefs.GetString(lastDishSaveKey),
                    PlayerPrefs.GetString(bestDishSaveKey)
                );
        }
    }
}
