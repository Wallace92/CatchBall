using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class LevelEasyController : MonoBehaviour
{

    //Temporary gameObject for levels
    GameObject itemInOneTab;
    //Prefab of levls with differents velocities
    public GameObject level_v4_pref;
    public GameObject level_v5_pref;
    public GameObject level_v6_pref;
    public GameObject level_v7_pref;
    public GameObject level_v8_pref;
    public GameObject level_v9_pref;
    public GameObject level_v10_pref;
    public GameObject level_v11_pref;
    public GameObject level_v12_pref;
    

    public Button btn_Menu;

    public List<GameObject> firstTab = new List<GameObject>();
    
    private List<Color> colorList = new List<Color> { };

  //  public RectTransform textRect;  // to chyba nie jest potrzebne

    // Get the possition of first and last prefab of the lattice
    public static Vector3 firstItemPosition;
    public static Vector3 lastItemPosition;

    public static float levelNumbers;
    public static float distanceToMiddlePoint;
    
    //Coeffiecients need for color of each prefab!
    float a;
    float b;
    float c;
    int offset = 100;
    int yComponentInteractable =  2*Screen.height;

    void Awake()
    {

        buttonColors();
        buttonInstantiate();
        btn_Menu.onClick.AddListener(btn_Menu_fun);

        // Poczatkowe polozenie pierwszego i ostatniego komponentu, ktore zmienia sie gdy skrolujemy
        firstItemPosition = firstTab[0].GetComponent<Button>().transform.localPosition;
        lastItemPosition = firstTab[firstTab.Count - 1].GetComponent<Button>().transform.localPosition;

        //Set all buttons expect of the FIRST to being noninteractable
        for (int item = 1; item < firstTab.Count; item++)
        {
            firstTab[item].GetComponent<Button>().interactable = false;
        }

        //Set appropriate colors it depends of as well at the position of buttons
        changeColors();
        levelNumbers = firstTab.Count;

        // Initial posiiton of all buttons
        if (LevelChangerController.maxLevelEasy > 15 && LevelChangerController.maxLevelEasy <= levelNumbers)
        {
         //   Debug.Log("last active position " + firstTab[LevelChangerController.maxLevelEasy].GetComponent<Button>().transform.localPosition.y);
            distanceToMiddlePoint = - firstTab[LevelChangerController.maxLevelEasy-1].GetComponent<Button>().transform.localPosition.y;
        }
        else if (LevelChangerController.maxLevelEasy > levelNumbers)
        {
            // Jakas mala liczba > 0 , aby pokolorowaly sie poszczegolne poziomy
             distanceToMiddlePoint = 1;
        }
        else
        {
            distanceToMiddlePoint = 1;
        }

        
       
    }

    void Update()
    {
        if (MoveCanvas.screenMoved == true)
        {
            changeColors();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync("LevelEasyChanger");
            SceneManager.LoadSceneAsync("LevelChanger", LoadSceneMode.Additive);
        }
    }

    void btn_Menu_fun()
    {
        //Clear whole list if you back to menu!
        Swap.levelsEasy.Clear();
        SceneManager.UnloadSceneAsync("LevelEasyChanger");
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);

    }

    void changeColors()
    {
        firstItemPosition = firstTab[0].GetComponent<Button>().transform.localPosition;
        lastItemPosition = firstTab[firstTab.Count-1].GetComponent<Button>().transform.localPosition;
     
        foreach (var item in firstTab)
        {
            // Gdy wspolrzedna Buttona y > 650 to jego kolor jest przezroczysty i nie jest on klikalny 
            if (item.transform.localPosition.y > yComponentInteractable )
            {
                
                //BUTTON
                var tempColor = item.GetComponent<Button>().colors;
                tempColor.disabledColor = new Color(tempColor.disabledColor.r, tempColor.disabledColor.g, tempColor.disabledColor.b, 0);
                item.GetComponent<Button>().colors = tempColor;

                //BUTTON TEXT
                var textTempColor = item.GetComponentInChildren<Text>().color;
                textTempColor = new Color(textTempColor.r, textTempColor.g, textTempColor.b, 0);
                item.GetComponentInChildren<Text>().color = textTempColor;

                item.GetComponent<Button>().interactable = false;
            }
            else
            {    
                var tempColor = item.GetComponent<Button>().colors;
                tempColor.disabledColor = new Color(tempColor.disabledColor.r, tempColor.disabledColor.g, tempColor.disabledColor.b, 1);
                item.GetComponent<Button>().colors = tempColor;
             

                var textTempColor = item.GetComponentInChildren<Text>().color;
                textTempColor = new Color(textTempColor.r, textTempColor.g, textTempColor.b, 1);
                item.GetComponentInChildren<Text>().color = textTempColor;

               // item.GetComponent<Button>().interactable = true;
            }
        }
        // PIERWSZY POZIOM! ZAWSZE WLACZONY, TYLKO MOZE BYC CZASAMI NIEWIDOCZNY
        if (firstTab[0].transform.localPosition.y > yComponentInteractable)
        {

        }
        else
        {
            firstTab[0].GetComponent<Button>().interactable = true;

        }
       
        // POZOSTALE POZIOMY!
        if (LevelChangerController.maxLevelEasy < levelNumbers)
        {
            for (int i = 1; i < LevelChangerController.maxLevelEasy; i++)
            {
                if (firstTab[i].transform.localPosition.y > yComponentInteractable)
                {

                }
                else
                {
                    firstTab[i].GetComponent<Button>().interactable = true;

                }
            }
        }
        else
        {
            for (int i = 1; i < levelNumbers; i++)
            {
                if (firstTab[i].transform.localPosition.y > yComponentInteractable)
                {

                }
                else
                {
                    firstTab[i].GetComponent<Button>().interactable = true;

                }
            }
        }
 
        MoveCanvas.screenMoved = false;
    }

    void buttonColors()
    {
        int z = 1;
       
        float coefficient = 51.6f;
        for (int i = 0; i < 12; i++)
        {
            if (i < 3)
            {
                a = offset + coefficient * z;
                b = offset + coefficient * z;
                c = 0;
            }
            else if (i >= 3 && i < 6)
            {
                a = 0;
                b = offset + coefficient * z;
                c = 0;
            }
            else if (i >= 6 && i < 9)
            {
                a = 0;
                b = 0;
                c = offset + coefficient * z;
            }
            else
            {
                a = offset + coefficient * z;
                b = 0;
                c = 0;
            }
            //   Debug.Log(z);
            //   Debug.Log(a + " " +  b + " " +  c);

            Color differentColors = new Color(a / 255f, b / 255f, c / 255f);
            colorList.Add(differentColors);
            if (z % 3 == 0)
            {
                z = 0;
            }
            z++;
        }
    }

    void buttonInstantiate()
    {
        int w = 1;
        int prefIter = 1;
        int colorIterator = 0;
        for (int k = 0; k < 5; k++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    offset = 1750 * k;
                    //   Debug.Log(i + " " + j + " " + k + " colorIterator: " + colorIterator);

                    if (prefIter < 10)
                    {
                        itemInOneTab = Instantiate(level_v4_pref) as GameObject;
                    }
                    else if (prefIter < 20)
                    {
                        itemInOneTab = Instantiate(level_v5_pref) as GameObject;
                    }
                    else if (prefIter < 30)
                    {
                        itemInOneTab = Instantiate(level_v6_pref) as GameObject;
                    }
                    else if (prefIter < 40)
                    {
                        itemInOneTab = Instantiate(level_v7_pref) as GameObject;
                    }
                    else if (prefIter < 50)
                    {
                        itemInOneTab = Instantiate(level_v8_pref) as GameObject;
                    }
                    else if (prefIter < 60)
                    {
                        itemInOneTab = Instantiate(level_v9_pref) as GameObject;
                    }
                    else if (prefIter < 70)
                    {
                        itemInOneTab = Instantiate(level_v10_pref) as GameObject;
                    }
                    else 
                    {
                        itemInOneTab = Instantiate(level_v11_pref) as GameObject;
                    }

                    itemInOneTab.transform.SetParent(transform, false);
                    itemInOneTab.transform.localPosition = new Vector3(-380 + 380 * i, 550 - 350 * j - offset, 0);
                    itemInOneTab.GetComponentInChildren<Text>().text = (w).ToString();

                    if (w < 10)
                    {
                        itemInOneTab.GetComponentInChildren<Text>().transform.localPosition = new Vector2(32, 10);
                    }
                    else
                    {
                        itemInOneTab.GetComponentInChildren<Text>().transform.localPosition = new Vector2(20, 20);
                    }
                    if (colorIterator < 12)
                    {
                        var colors = itemInOneTab.GetComponent<Button>().colors;
                        colors.normalColor = colorList[colorIterator];
                        itemInOneTab.GetComponent<Button>().colors = colors;

                    }
                    else
                    {

                        colorIterator = 0;
                        var colors = itemInOneTab.GetComponent<Button>().colors;
                        colors.normalColor = colorList[colorIterator];
                        itemInOneTab.GetComponent<Button>().colors = colors;
                    }
                    colorIterator++;

                    firstTab.Add(itemInOneTab);
                    w++;
                    prefIter++;
                }
            }
        }
    }

}

        /*
        one.text = "1";
        one.transform.localPosition =  new Vector2(40, 10);
        two.text = "22";
        two.transform.localPosition = new Vector2(20, 10);
        three.text = "300";
        three.transform.localPosition = new Vector2(1, 10);
        */
