using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.IO;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    public Text coingText;
  
    Button rewardButton;
 
    
    void Start()
    {
        rewardButton = GetComponent<Button>();
        // Set interactivity to be dependent on the Placement’s status true/false:
        rewardButton.interactable = Advertisement.IsReady(DontDestroy.reward_video); 

        // If video is ready you can click at rewardButton and showAdd
        if (rewardButton) rewardButton.onClick.AddListener(ShowRewardedVideo);   

        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (DontDestroy.store_id, true);

    }

    void ShowRewardedVideo()
    {
        Advertisement.Show(DontDestroy.reward_video);
    }


    // Implement IUnityAdsListener interface methods from Unity doc.:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == DontDestroy.reward_video)
        {
            rewardButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (LevelChangerController.currentScene == "Level0")
            {
                //If player dont pass the game yet
                if (LevelChangerController.maxLevelEasy <= LevelEasyController.levelNumbers)
                {
                    //Unlock new level, move leves and write additional 500 point to coinText and stats file
                    LevelChangerController.maxLevelEasy++;
                    MoveCanvas.screenMoved = true;

                    long cointTextAmount = long.Parse(coingText.text.Split()[1]);

                    if (PlayGamesScript.signedToGooglePlays == true)
                    {
                        PlayGamesScript.AddScoreToLeaderboard(cointTextAmount + 500);
                    }

                    coingText.text = "Scores: " + (cointTextAmount + 500).ToString();
                    if (Swap.scoresEasy.Count < MoveCanvas.currentLevelNumber)
                    {
                        Swap.scoresEasy.Add(500);
                    }
                    else
                    {
                        Swap.scoresEasy.Insert(MoveCanvas.currentLevelNumber, 500);
                    }

                    string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";
                    bool fileExist2 = File.Exists(pathStats);
                    if (fileExist2)
                    {
                        using (StreamWriter outputFile = new StreamWriter(pathStats))
                        {
                            for (int i = 0; i < Swap.scoresEasy.Count; i++)
                            {
                                outputFile.Write("Level" + (i + 1) + " " + Swap.scoresEasy[i] + "\n");
                            }

                        }
                    }
                    using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + "maximumLevel.MG"))
                    {
                        sw.WriteLine(LevelChangerController.maxLevelEasy.ToString() + " " + LevelChangerController.maxLevelMedium.ToString() + " " + LevelChangerController.maxLevelHard.ToString());
                    }
                }
            }
            else if (LevelChangerController.currentScene == "Level1")
            {
                if (LevelChangerController.maxLevelMedium <= LevelMediumController.levelNumbers)
                {
                    //Unlock new level, move leves and write additional 500 point to coinText and stats file
                    LevelChangerController.maxLevelMedium++;
                    MoveCanvas.screenMoved = true;

                    long cointTextAmount = long.Parse(coingText.text.Split()[1]);
                    PlayGamesScript.AddScoreToLeaderboard(cointTextAmount + 500);

                    coingText.text = "Scores: " +  (cointTextAmount + 500).ToString();
                    if (Swap.scoresMedium.Count < MoveCanvas.currentLevelNumber)
                    {
                        Swap.scoresMedium.Add(500);
                    }
                    else
                    {
                        Swap.scoresMedium.Insert(MoveCanvas.currentLevelNumber, 500);
                    }

                    string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";
                    bool fileExist2 = File.Exists(pathStats);
                    if (fileExist2)
                    {
                        using (StreamWriter outputFile = new StreamWriter(pathStats))
                        {
                            for (int i = 0; i < Swap.scoresMedium.Count; i++)
                            {
                                outputFile.Write("Level" + (i + 1) + " " + Swap.scoresMedium[i] + "\n");
                            }

                        }
                    }
                    using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + "maximumLevel.MG"))
                    {
                        sw.WriteLine(LevelChangerController.maxLevelEasy.ToString() + " " + LevelChangerController.maxLevelMedium.ToString() + " " + LevelChangerController.maxLevelHard.ToString());
                    }
                }
            }
            else if (LevelChangerController.currentScene == "Level2")
            {
                if (LevelChangerController.maxLevelHard <= LevelHardController.levelNumbers)
                {
                    //Unlock new level, move leves and write additional 500 point to coinText and stats file
                    LevelChangerController.maxLevelHard++;
                    MoveCanvas.screenMoved = true;

                    long cointTextAmount = long.Parse(coingText.text.Split()[1]);
                    PlayGamesScript.AddScoreToLeaderboard(cointTextAmount + 500);

                    coingText.text = "Scores: " +  (cointTextAmount + 500).ToString();
                    if (Swap.scoresHard.Count < MoveCanvas.currentLevelNumber)
                    {
                        Swap.scoresHard.Add(500);
                    }
                    else
                    {
                        Swap.scoresHard.Insert(MoveCanvas.currentLevelNumber, 500);
                    }

                    string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";
                    bool fileExist2 = File.Exists(pathStats);
                    if (fileExist2)
                    {
                        using (StreamWriter outputFile = new StreamWriter(pathStats))
                        {
                            for (int i = 0; i < Swap.scoresHard.Count; i++)
                            {
                                outputFile.Write("Level" + (i + 1) + " " + Swap.scoresHard[i] + "\n");
                            }

                        }
                    }
                    using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + "maximumLevel.MG"))
                    {
                        sw.WriteLine(LevelChangerController.maxLevelEasy.ToString() + " " + LevelChangerController.maxLevelMedium.ToString() + " " + LevelChangerController.maxLevelHard.ToString());
                    }
                }
            }



        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
        else
        {
            Debug.Log("Error");
        }

        Advertisement.RemoveListener(this);
        rewardButton.interactable = false;
      
    }

    public void OnUnityAdsDidError(string message)
    {
        Advertisement.RemoveListener(this);
        rewardButton.interactable = false;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
