using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;
using System.Globalization;
// using CloudOnce;
public class Swap : MonoBehaviour
{
    public Sprite musicOff;
    public Sprite musicON;

    public Button btn_Start;
    public Button btn_Mute;

    public Button btn_Restart;
    public Button btn_Level1;
    public Button btn_Level2;
    public Button btn_Level3;

    public Button yes;
    public Button no;
    
    public Text you_sure;
    public Text textStats;
    public Text pickLevelText;

    public bool audio_mute = false;
    private bool clicked = false;

    // VELOCITIES and TIMES of level Easy
    public static Dictionary<int, (int, float)> levelsEasy = new Dictionary<int, (int, float)>();
    public static Dictionary<int, (int, float)> levelsMedium = new Dictionary<int, (int, float)>();
    public static Dictionary<int, (int, float)> levelsHard = new Dictionary<int, (int, float)>();

    public static List<float> scoresEasy = new List<float>();
    public static List<float> scoresMedium = new List<float>();
    public static List<float> scoresHard = new List<float>();

    private string levelText;

    public static float overallScore;

    private int i;
    
    void Awake()
    {
        //Initial value of overallscore - coinAmout!
        overallScore = 0;
       
      //  BUTTONS 
        yes.onClick.AddListener(delegate { Restart(i); });
        no.onClick.AddListener(NoRestart);
       
        btn_Start.onClick.AddListener(btn_Start_fun);
        btn_Restart.onClick.AddListener(btn_Restart_fun);
        btn_Mute.onClick.AddListener(btn_Mute_fun);

        btn_Level1.onClick.AddListener(delegate { Reset(0); });
        btn_Level2.onClick.AddListener(delegate { Reset(1); });
        btn_Level3.onClick.AddListener(delegate { Reset(2); });
  
       
        //If VELOCITY and TIME and the STATS files are not exist create them!
        create_Files();

        //Open Stats files and add every number to appropriate list 
        if (scoresEasy.Count == 0)
        {
            OpenFile(scoresEasy, 0);
            foreach (var item in scoresEasy)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in scoresEasy)
            {
                overallScore += item;
            }
        }
        
        if (scoresMedium.Count == 0)
        {
            OpenFile(scoresMedium, 1);
            foreach (var item in scoresMedium)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in scoresMedium)
            {
                overallScore += item;
            }
        }
        if (scoresHard.Count == 0)
        {
            OpenFile(scoresHard, 2);
            foreach (var item in scoresHard)
            {
                overallScore += item;
            }
        }
        else
        {
            //If files was opened just add the appropriate scores do not open it again! It will multiply the scores
            foreach (var item in scoresHard)
            {
                overallScore += item;
            }
        }
       
    }


    void OpenFile(List<float> scoresList, int level)
    {
        //Temporary variably of file content
        List<string> plik2 = new List<string>();

        int counter = 0;
        string line;

       // Debug.Log(Application.persistentDataPath + "/" + "Level" + level + "Stats" + ".MG");
        System.IO.StreamReader file = new System.IO.StreamReader(Application.persistentDataPath + "/" + "Level" + level + "Stats" + ".MG");

        while ((line = file.ReadLine()) != null)
        {
            plik2.Add(line);
            counter++;
        }
        file.Close();

        for (int i = 0; i < plik2.Count; i++)
        {
            string[] words = plik2[i].Split(' ');
            scoresList.Add(float.Parse(words[1], CultureInfo.InvariantCulture));
          
        }
    }

    void btn_Mute_fun()
    {
        MusicControl.musicInstance.audio_mute = true;
        if (clicked == false)
        {
            btn_Mute.image.sprite = musicOff;
            clicked = true;
        }
        else
        {
            btn_Mute.image.sprite = musicON;
            clicked = false;
        }
    }
    

    void Reset(int level)
    {
        btn_Level1.gameObject.SetActive(false);
        btn_Level2.gameObject.SetActive(false);
        btn_Level3.gameObject.SetActive(false);
        pickLevelText.gameObject.SetActive(false);
      
        btn_Restart.interactable = false;

        i = level;
        if (level == 0)
        {
            levelText = "EASY";
        }
        else if (level == 1)
        {
            levelText = "MEDIUM";
        }
        else if (level == 2)
        {
            levelText = "HARD";
        }
        // After click Yes player will reset the scores of all levels
        you_sure.text = "RESET " + levelText + "?";   //: " + ((int)level + 1).ToString();
        you_sure.gameObject.SetActive(true);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);
       
    }

    void create_Files()
    {

        // In the case when player during the game was back to the menu, every time an additional dictrionary levelEasy have been created\
        // This condition will protect the user by create addtional numbers in the levelEasy dictionary
        if (levelsEasy.Count ==0)
        {
            CreateLevels(2, 20, 50, 2f, 0);
        }
        if (levelsMedium.Count == 0)
        {
            CreateLevels(2, 30, 70, 2f, 1);
        }
        if (levelsHard.Count == 0)
        {
            CreateLevels(2, 30, 102, 2f, 2);
        }
      

        // create MAXIMUM LEVELS
        string path = Application.persistentDataPath + "/maximumLevel.MG";
        bool fileExist = File.Exists(path);
        if (fileExist)
        {
            ; // Debug.Log("FileExist");
        }
        else
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {

                outputFile.Write("1 1 1");
            }
        }
        
        // create STATS FILES
        for (int i = 0; i < 3; i++)
        {
            string pathStats = Application.persistentDataPath + "/Level" + i + "Stats.MG";

            bool fileExist2 = File.Exists(pathStats);
            if (fileExist2)
            {
                ; // Debug.Log("FileStatsExist");
            }
            else
            {
                if (i == 0)
                {
                    using (StreamWriter outputFile = new StreamWriter(pathStats))
                    {
                        outputFile.Write("");
                    }
                }
                if (i == 1)
                {
                    using (StreamWriter outputFile = new StreamWriter(pathStats))
                    {
                        outputFile.Write("");
                    }
                }
                if (i == 2)
                {
                    using (StreamWriter outputFile = new StreamWriter(pathStats))
                    {
                        outputFile.Write("");
                    }
                }
            }
        }
    }
    void btn_Start_fun()
    {
        SceneManager.LoadSceneAsync("LevelChanger", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MenuScene");
    }

    void btn_Restart_fun()
    {
        textStats.gameObject.SetActive(false);
        pickLevelText.gameObject.SetActive(true);
        btn_Level1.gameObject.SetActive(true);
        btn_Level2.gameObject.SetActive(true);
        btn_Level3.gameObject.SetActive(true);
    }


    void Restart(int level)
    {
        Restart_File(level);

        btn_Restart.interactable = true;
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        you_sure.gameObject.SetActive(false);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void Restart_File(int filenumber)
    {

        using (StreamWriter outputFile = new StreamWriter(Application.persistentDataPath + "/" + "Level" + filenumber + "Stats" + ".txt"))
        {
            outputFile.Write("");
        }

    }

    void NoRestart()
    {
     
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        you_sure.gameObject.SetActive(false);
        btn_Restart.interactable = true;
      
    }

    void CreateLevels(int step, int initial_speed, float final_speed, float final_time, int level)
    {
        float initial_time;
        final_speed = final_speed + step;
        final_time = final_time - step;
        int i = 0;  

        while (initial_speed < final_speed)
        {
            initial_time = 10;
            while (initial_time > final_time)
            {
                float time = (float) Math.Round(initial_time, 2);
                int speed = initial_speed;
              
                if (level == 0)
                {
                    levelsEasy.Add(i, (speed, time));
                 //   Debug.Log("i " + i +  "speed "+ speed + "time " + time );

                }
                else if (level == 1)
                {
                    levelsMedium.Add(i, (speed, time));
                }
                else if (level == 2)
                {
                    levelsHard.Add(i, (speed, time));
                
                }

                initial_time = initial_time - step;
                i = i + 1;
              
            }
        initial_speed = initial_speed + step;
        }    
    }
}