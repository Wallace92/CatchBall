using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;  // .Last()
using System.Collections.Generic;
public class LevelChangerController : MonoBehaviour
{
    public Text overallScores;
    public Button btn_Easy;
    public Button btn_Medium;
    public Button btn_Hard;
    public Button btn_Menu;

    public Vector3 initialCameraPos;

    public static int maxLevelEasy;
    public static int maxLevelMedium;
    public static int maxLevelHard;
    public static string currentScene;

    void Start()
    {
        //Open maximumLevel file and take the information about each levels
        List<int> plik2 = new List<int>();
    
        string line;
        string path = Application.persistentDataPath + "/maximumLevel.MG";

        int counter = 0;

        System.IO.StreamReader file = new System.IO.StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {

            maxLevelEasy = int.Parse(line.Split()[0]);
            maxLevelMedium = int.Parse(line.Split()[1]);
            maxLevelHard = int.Parse(line.Split()[2]);
            counter++;
        }
        file.Close();

        btn_Easy.onClick.AddListener(btn_Easy_fun);
        btn_Medium.onClick.AddListener(btn_Medium_fun);
        btn_Hard.onClick.AddListener(btn_Hard_fun);
        btn_Menu.onClick.AddListener(btn_Menu_fun);
    }
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync("LevelChanger");
            SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        }
    }

    void btn_Easy_fun()
    {
        currentScene = "Level0";
        SceneManager.UnloadSceneAsync("LevelChanger");
        SceneManager.LoadSceneAsync("LevelEasyChanger", LoadSceneMode.Additive);
    }

    void btn_Medium_fun()
    {
        currentScene = "Level1";
        SceneManager.UnloadSceneAsync("LevelChanger");
        SceneManager.LoadSceneAsync("LevelMediumChanger", LoadSceneMode.Additive);
    }

    void btn_Hard_fun()
    {
        currentScene = "Level2";
        SceneManager.UnloadSceneAsync("LevelChanger");
        SceneManager.LoadSceneAsync("LevelHardChanger", LoadSceneMode.Additive);
    }

    void btn_Menu_fun()
    {
        SceneManager.UnloadSceneAsync("LevelChanger");
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
    }
}
