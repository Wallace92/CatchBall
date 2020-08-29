
using UnityEngine;
using UnityEngine.UI;

public class CoinsTextScript : MonoBehaviour
{

    public Text cointText;
    public static bool coinUpdate = false;
    float overallScore;

    void Start()
    {
        overallScore = 0;

        if (Swap.scoresEasy.Count == 0)
        {
            
            foreach (var item in Swap.scoresEasy)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in Swap.scoresEasy)
            {
                overallScore += item;
            }
        }

        if (Swap.scoresMedium.Count == 0)
        {
           
            foreach (var item in Swap.scoresMedium)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in Swap.scoresMedium)
            {
                overallScore += item;
            }
        }
        if (Swap.scoresHard.Count == 0)
        {
            
            foreach (var item in Swap.scoresHard)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in Swap.scoresHard)
            {
                overallScore += item;
            }
        }

        //Initial value of coins
        cointText.text = "SCORE: " + overallScore.ToString();
    }

    void Update()
    {
        // In GameControl script the coinUpdate is changed after pass the level so the coin amout is updated as well
        if (coinUpdate == true)
        {
            cointText.text = "SCORE: " + GameControl.instance.scoresToLeaderboard.ToString();
            coinUpdate = false;
        }
    }

 
}
