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
        GameStatistic.UpdateScoreUI();
    }
    
    public class GameStatistic
    {
        private static float Score = 0;
        private static float bestDishScore = 0;

        public static void RegisterNewDish(float number)
        {
            CompareWithRecord(number);
            Score += number;
            UpdateScoreUI();
        }

        internal static void UpdateScoreUI()
        {
            instance.ScoreText.text = Score.ToString();
        }

        public static void CompareWithRecord(float number)
        {
            if (number > bestDishScore)
            {
                bestDishScore = number;
            }
        }
    
    
    }
}
