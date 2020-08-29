using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class PlayGamesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool signedToGooglePlays = false;
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        SignIn();
    
    }
    void SignIn()
    {
        Social.localUser.Authenticate(success => 
        { 
            if (success)
            {
                signedToGooglePlays = true; // Debug.Log("Authenticate successfully");
            }
            else
            {
                ; // Debug.Log("Authenticate failed");
            }
        });
    }

    #region Leaderboards
    public static void AddScoreToLeaderboard(long score) 
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ReportScore(score, GPGSIds.leaderboard_catchball, (bool success2) =>
                {
                    if (success2)
                    {
                        ;// Debug.Log("Reported score successfully");
                    }
                    else
                    {
                        ; // Debug.Log("Failed to report score");
                    }
                    
                });
            }
       }); 
    }
  
    public static void ShowLeaderboardsUI()
    {
          Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards


}
