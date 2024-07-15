using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text ScoreText;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public class GameStatistic
    {
        private static int Score = 0;
        private static int bestDishScore = 0;

        public static void RegisterNewDish(int number)
        {
            CompareWithRecord(number);
            Score += number;
            instance.ScoreText.text = Score.ToString();
        }

        public static void CompareWithRecord(int number)
        {
            if (number > bestDishScore)
            {
                bestDishScore = number;
            }
        }
    
    
    }
}
