using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Advertisements;
using System.Collections; // IEnumerator
using System;  // Math


public class GameControl : MonoBehaviour
{
    /*
    Instant variable from singelton Patter allow to 
    change value of others variables
    */ 
    public static GameControl instance;

    AudioSource GameFinishAudio;
    AudioSource GameOverAudio;
    public AudioSource CoinAudio;
    public AudioClip GameOverClip;
    public AudioClip GameFinishClip;

    public GameObject GameOverText;  
    
    public Text colorText;      // Red, Green, Blue
    public Text Congratulation;
    public Text LevelTextVisible;
    public Text ReactionTimeText;
    public Text MeantimeText;
    public Text ReactionTimesText;
    public Text gratulation;

    public Button btnPlayAgain;
    public Button nextLevel;
    public Button btnMenu;

    public Dictionary<string, (List<string>, string)> plik = new Dictionary<string, (List<string>, string)>();

    private bool playAgainControl = false;
    private bool soundGameOver;

    public bool gameOver = false;
    public bool clickedCorrect = false;
    public bool clickedWrong = false;
    public bool nextLevelControl = false;
    public bool nextLevelCheck = false;
    public bool add_best_time = true;
    public bool audio_mute = false;
    public bool finalScene = false;
    public bool bannerOn = false;
   
    public string initialTimeStringText;
    public string initialVelocityStringText;
    public string SceneName;

    public float scoresToLeaderboard;
    public float initialTimeSecondLevel;
    public float dot_velocity;

    public long scoreLeaderboard;

    public int StarNumbers = 3;
    public int score = 0;
    public int activeLevel;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        CoinAudio = GetComponent<AudioSource>();
        GameOverAudio = GetComponent<AudioSource>();
        GameFinishAudio = GetComponent<AudioSource>();

        soundGameOver = false;

        activeLevel = MoveCanvas.currentLevelNumber;

        // Take information about time and velocity of current level
        if (LevelChangerController.currentScene == "Level0")
        {
            dot_velocity = Swap.levelsEasy[activeLevel - 1].Item1;
            initialTimeSecondLevel = Swap.levelsEasy[activeLevel - 1].Item2;
            LevelTextVisible.text = "LEVEL\n" + activeLevel + "/" + LevelEasyController.levelNumbers;
        }
        else if (LevelChangerController.currentScene == "Level1")
        {
            dot_velocity = Swap.levelsMedium[activeLevel - 1].Item1;
            initialTimeSecondLevel = Swap.levelsMedium[activeLevel - 1].Item2;
            LevelTextVisible.text = "LEVEL\n" + activeLevel + "/" + LevelMediumController.levelNumbers;
        }else if (LevelChangerController.currentScene == "Level2")
        {
            dot_velocity = Swap.levelsHard[activeLevel - 1].Item1;
            initialTimeSecondLevel = Swap.levelsHard[activeLevel - 1].Item2;
            LevelTextVisible.text = "LEVEL\n" + activeLevel + "/" + LevelHardController.levelNumbers;
        }
        
    }

    void Start()
    { 
        btnPlayAgain.onClick.AddListener(PlayAgainFun);
        nextLevel.onClick.AddListener(NextLevelFun);
        btnMenu.onClick.AddListener(btnMenuFun);
        StartCoroutine(HideBaner());
    }

    void btnMenuFun()
    {      
        //When you back to menu destry starts and dots
        gameObject.GetComponent<DotControler>().DeleteDots();
        gameObject.GetComponent<StarControler>().DeleteStart();

        SceneManager.UnloadSceneAsync(LevelChangerController.currentScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);    
    }

    void NextLevelFun()
    {
        nextLevelControl = true; 
    }

    void PlayAgainFun()
    {
        playAgainControl = true;
    }

    void Update()
    {
        if (gameOver == true && playAgainControl == true)
        {
            // Unload and Load the same scene again after "playagain" button pressed
            score = 0;

            gameObject.GetComponent<StarControler>().DeleteStart();

            SceneManager.UnloadSceneAsync(LevelChangerController.currentScene);
            SceneManager.LoadSceneAsync(LevelChangerController.currentScene, LoadSceneMode.Additive);
        }
        else if (nextLevelControl == true)
        { 
            // Destroy starts and dots
            gameObject.GetComponent<DotControler>().DeleteDots();
            gameObject.GetComponent<StarControler>().DeleteStart();

            saveToFile();

            // Button UI element
            nextLevel.gameObject.SetActive(false);
            
            StartCoroutine(HideBaner());

            SceneManager.UnloadSceneAsync(LevelChangerController.currentScene);
            //ANIMACJA ZDOBYTEJ MONETY! 
            if (LevelChangerController.currentScene == "Level0")
            {
                SceneManager.LoadSceneAsync("LevelEasyChanger", LoadSceneMode.Additive);
            }
            else if (LevelChangerController.currentScene == "Level1")
            {
                SceneManager.LoadSceneAsync("LevelMediumChanger", LoadSceneMode.Additive);
            }
            else if (LevelChangerController.currentScene == "Level2")
            {
                SceneManager.LoadSceneAsync("LevelHardChanger", LoadSceneMode.Additive);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.GetComponent<DotControler>().DeleteDots();
            gameObject.GetComponent<StarControler>().DeleteStart();

            CoinsTextScript.coinUpdate = true;
            SceneManager.UnloadSceneAsync(LevelChangerController.currentScene);
            
            if (LevelChangerController.currentScene == "Level0")
            {
                SceneManager.LoadSceneAsync("LevelEasyChanger", LoadSceneMode.Additive);
            }
            else if (LevelChangerController.currentScene == "Level1")
            {
                SceneManager.LoadSceneAsync("LevelMediumChanger", LoadSceneMode.Additive);
            }
            else if (LevelChangerController.currentScene == "Level2")
            {
                SceneManager.LoadSceneAsync("LevelHardChanger", LoadSceneMode.Additive);
            }
        }   
    }

    public void GameOver()
    {
        
        // Avoid sound loop when the time of the round end
        if (!soundGameOver)
        {
            GameOverAudio.PlayOneShot(GameOverClip);
            soundGameOver = true;
        }

        //Hide top Text and show banner with gameoverText and playAgain button
        colorText.gameObject.SetActive(false);
        StartCoroutine(ShowBanner());

        GameOverText.SetActive(true);
        btnPlayAgain.gameObject.SetActive(true);

        //additional variables
        clickedWrong = true; // Timer, DotMove
        gameOver = true; // Timer, DotMove, DotControler, ColorText Script x3
       
    }

    void saveToFile()
    {

        // LEVEL0
        if (LevelChangerController.currentScene == "Level0")
        {
            // When you pass the round for the first time! 
            // (You will do this statement each time when you start higher level!)
           // Debug.Log("Timer scores" + Timer.wynik);
            if (Swap.scoresEasy.Count < MoveCanvas.currentLevelNumber)
            {
                Swap.scoresEasy.Add(Timer.wynik);
            }
            else if (Swap.scoresEasy.Count == MoveCanvas.currentLevelNumber)
            {
                Swap.scoresEasy.RemoveAt(Swap.scoresEasy.Count - 1);
                Swap.scoresEasy.Add(Timer.wynik);
            }
            else
            {
                // When you do again an level which you passed first of all 
                // remove existing score at currentLevelNumber and add an newScore 
                Swap.scoresEasy.RemoveAt(MoveCanvas.currentLevelNumber);
                Swap.scoresEasy.Insert(MoveCanvas.currentLevelNumber, Timer.wynik);
            }

            // Save to file
            string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";

            bool fileExist2 = File.Exists(pathStats);  // True or false
            if (fileExist2)  // If true, it means if file exist!
            {
                using (StreamWriter outputFile = new StreamWriter(pathStats))
                {
                    for (int i = 0; i < Swap.scoresEasy.Count; i++)
                    {
                        outputFile.Write("Level" + (i + 1) + " " + Swap.scoresEasy[i] + "\n");
                    }

                }
            }
        }
        else if (LevelChangerController.currentScene == "Level1") // MEDIUM
        {
            // When you pass the round for the first time! 
            // (You will do this statement each time when you start higher level!)
            if (Swap.scoresMedium.Count < MoveCanvas.currentLevelNumber)
            {
                Swap.scoresMedium.Add(Timer.wynik);
            }
            else if (Swap.scoresMedium.Count == MoveCanvas.currentLevelNumber)
            {
                Swap.scoresMedium.RemoveAt(Swap.scoresEasy.Count - 1);
                Swap.scoresMedium.Add(Timer.wynik);
            }
            else
            {
                // When you do again an level which you passed first of all 
                // remove existing score at currentLevelNumber and add an newScore 
                Swap.scoresMedium.RemoveAt(MoveCanvas.currentLevelNumber);
                Swap.scoresMedium.Insert(MoveCanvas.currentLevelNumber, Timer.wynik);
            }

            // Save to file
            string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";

            bool fileExist2 = File.Exists(pathStats);  // True or false

            if (fileExist2)  // If true, it means if file exist!
            {
                using (StreamWriter outputFile = new StreamWriter(pathStats))
                {
                    for (int i = 0; i < Swap.scoresMedium.Count; i++)
                    {
                        outputFile.Write("Level" + (i + 1) + " " + Swap.scoresMedium[i] + "\n");
                    }

                }
            }
        }
        else if (LevelChangerController.currentScene == "Level2") // MEDIUM
        {
            // When you pass the round for the first time! 
            // (You will do this statement each time when you start higher level!)
            if (Swap.scoresHard.Count < MoveCanvas.currentLevelNumber)
            {
                Swap.scoresHard.Add(Timer.wynik);
            }
            else if (Swap.scoresHard.Count == MoveCanvas.currentLevelNumber)
            {
                Swap.scoresHard.RemoveAt(Swap.scoresEasy.Count - 1);
                Swap.scoresHard.Add(Timer.wynik);
            }
            else
            {
                // When you do again an level which you passed first of all 
                // remove existing score at currentLevelNumber and add an newScore 
                Swap.scoresHard.RemoveAt(MoveCanvas.currentLevelNumber);
                Swap.scoresHard.Insert(MoveCanvas.currentLevelNumber, Timer.wynik);
            }

            // Save to file
            string pathStats = Application.persistentDataPath + "/" + LevelChangerController.currentScene + "Stats.MG";

            bool fileExist2 = File.Exists(pathStats);  // True or false

            if (fileExist2)  // If true, it means if file exist!
            {
                using (StreamWriter outputFile = new StreamWriter(pathStats))
                {
                    for (int i = 0; i < Swap.scoresMedium.Count; i++)
                    {
                        outputFile.Write("Level" + (i + 1) + " " + Swap.scoresMedium[i] + "\n");
                    }

                }
            }
        }

        foreach (var item in Swap.scoresEasy)
        {
            scoresToLeaderboard += item;
        }
        
        foreach (var item in Swap.scoresMedium)
        {
            scoresToLeaderboard += item;
        }
        
        foreach (var item in Swap.scoresHard)
        {
            scoresToLeaderboard += item;
        }
        

        //AddScores to leaderboard
        if (PlayGamesScript.signedToGooglePlays == true)
        {
            PlayGamesScript.AddScoreToLeaderboard((long)scoresToLeaderboard);
        }    

        //Update coins scores
        CoinsTextScript.coinUpdate = true;

        //Write maximum level to file
        using (StreamWriter sw2 = new StreamWriter(Application.persistentDataPath + "/" + "maximumLevel.MG"))
        {
            sw2.Write(LevelChangerController.maxLevelEasy.ToString() + " " + LevelChangerController.maxLevelMedium.ToString() + " " + LevelChangerController.maxLevelHard.ToString());
        }
    }

    public void GameScore()
    {
        if (gameOver)
        {
            return;  
        }

        CoinAudio.Play();
        CoinAudio.volume = 0.1f;

        score++;
        clickedCorrect = true;
        
        //If player corectly cliced 10 stars!
        if (score == StarNumbers)
        {
            if (LevelChangerController.currentScene == "Level0")
            {
                //If player trying to pass the newest level
                if (LevelChangerController.maxLevelEasy <= LevelEasyController.levelNumbers)
                {
                    if (LevelChangerController.maxLevelEasy == MoveCanvas.currentLevelNumber - 1 || LevelChangerController.maxLevelEasy == MoveCanvas.currentLevelNumber)
                    {
                        LevelChangerController.maxLevelEasy++;
                    }
                    // Update levels canvas
                    MoveCanvas.screenMoved = true;
                }

                // If the currentLevel is less then number of TOTAL levels
                if (activeLevel < LevelEasyController.levelNumbers)
                {

                    StartCoroutine(ShowBanner());

                    Congratulation.text = "LEVEL STATUS:\n" + activeLevel + "/" + LevelEasyController.levelNumbers;
                    Congratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }
                else
                {
                    // Player passed whole level! 
                    GameFinishAudio.PlayOneShot(GameFinishClip);

                    finalScene = true;

                    gratulation.text = "GRATULATION\nYOU PASS\nTHIS LEVEL";
                    gratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }

            }
            else if (LevelChangerController.currentScene == "Level1")
            {
                if (LevelChangerController.maxLevelMedium <= LevelMediumController.levelNumbers)
                {
                    if (LevelChangerController.maxLevelMedium == MoveCanvas.currentLevelNumber - 1 || LevelChangerController.maxLevelMedium == MoveCanvas.currentLevelNumber)
                    {
                        LevelChangerController.maxLevelMedium++;
                    }
                    // Update levels canvas
                    MoveCanvas.screenMoved = true;
                }

                // If the currentLevel is less then number of TOTAL levels
                if (activeLevel < LevelMediumController.levelNumbers)
                {

                    StartCoroutine(ShowBanner());

                    Congratulation.text = "LEVEL STATUS:\n" + activeLevel + "/" + LevelMediumController.levelNumbers;
                    Congratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }
                else
                {
                    // Player passed whole level! 
                    GameFinishAudio.PlayOneShot(GameFinishClip);

                    finalScene = true;

                    gratulation.text = "GRATULATION\nYOU PASS\nTHIS LEVEL";
                    gratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }
            }
            else if (LevelChangerController.currentScene == "Level2")
            {
                if (LevelChangerController.maxLevelHard <= LevelHardController.levelNumbers)
                {
                    if (LevelChangerController.maxLevelHard == MoveCanvas.currentLevelNumber - 1 || LevelChangerController.maxLevelHard == MoveCanvas.currentLevelNumber)
                    {
                        LevelChangerController.maxLevelHard++;
                    }
                    // Update levels canvas
                    MoveCanvas.screenMoved = true;
                }

                // If the currentLevel is less then number of TOTAL levels
                if (activeLevel < LevelHardController.levelNumbers)
                {

                    StartCoroutine(ShowBanner());

                    Congratulation.text = "LEVEL STATUS:\n" + activeLevel + "/" + LevelHardController.levelNumbers;
                    Congratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }
                else
                {
                    // Player passed whole level! 
                    GameFinishAudio.PlayOneShot(GameFinishClip);

                    finalScene = true;
                   
                    gratulation.text = "GRATULATION\nYOU PASS\nTHIS LEVEL";
                    gratulation.gameObject.SetActive(true);

                    nextLevelCheck = true;
                    nextLevel.gameObject.SetActive(true);
                }
            }

           
        }
    }

    IEnumerator ShowBanner()
    {
        while (!Advertisement.IsReady(DontDestroy.banner_ad))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(DontDestroy.banner_ad);
    }

    IEnumerator HideBaner()
    {
        while (!Advertisement.IsReady(DontDestroy.banner_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Hide();
    }
}
