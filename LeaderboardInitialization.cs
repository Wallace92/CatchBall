
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardInitialization : MonoBehaviour
{
    public Button leaderBoard_btn;

    void Start()
    {
        leaderBoard_btn.onClick.AddListener(ShowLeaderboards);
    }

    public void ShowLeaderboards()
    {
        PlayGamesScript.ShowLeaderboardsUI();
    }


}

