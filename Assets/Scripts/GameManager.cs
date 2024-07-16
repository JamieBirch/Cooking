using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text ScoreText;
    public Text LastDishText;
    public Text BestDishText;

    private static string dishFormat = "{0} ({1}) [{2}]";
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        GameStatistic.UpdateScoreUI();
    }
    
    public class GameStatistic
    {
        private static float Score = 0;
        private static float bestDishScore = 0;

        public static void RegisterNewDish(string dishName, string dishIngredients, float dishScore)
        {
            Score += dishScore;
            UpdateScoreUI();
            string dishRegistrationString = String.Format(dishFormat, dishName, dishIngredients, dishScore);
            instance.LastDishText.text = dishRegistrationString;
            // compare with best dish
            if (dishScore > bestDishScore)
            {
                bestDishScore = dishScore;
                instance.BestDishText.text = dishRegistrationString;
            }
        }

        internal static void UpdateScoreUI()
        {
            instance.ScoreText.text = Score.ToString();
        }
    }
}
