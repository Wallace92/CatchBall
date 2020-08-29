using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Level 3: HARD
// String "colorText" with content "red, green, blue" only in red, gree, blue or color
public class ColorTextControler_rgb_black : MonoBehaviour
{

    public Text colorText;

    // Name of the textColor and its color in two different list because of black color
    private List<string> textName = new List<string> { "RED", "GREEN", "BLUE"};
    private List<string> colorlist = new List<string> { "RED", "GREEN", "BLUE", "BLACK" };

    private Dictionary<string, Color> colory = new Dictionary<string, Color>();

    private int randcolorText;
    private int randcolor;
    private int textNumbers = 3;

    void Awake()
    {
        // Add colors to the color list and select one of them without repetitions
        colory.Add("RED", new Color(2f, 0, 0));
        colory.Add("GREEN", new Color(0, 2f, 0));
        colory.Add("BLUE", new Color(0, 0, 2f));
        colory.Add("BLACK", new Color(0f, 0f, 0f));

        RandomColorWithRepetitions();
    }
    void Update()
    {
        // Select again if game is over or player clicked correct
        if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == true)
        {
            RandomColorWithRepetitions();
        }
    }
    void RandomColorWithRepetitions()
    {
        // Random text from red, green, blue
        randcolorText = Random.Range(0, textNumbers);  // 0 "from"  0, 1, 2
        colorText.text = textName[randcolorText]; ;  // "Red" = textName[0]

        // Random color from red, green, blue
        randcolor = Random.Range(0, textNumbers + 1);  // 2 "z" 0, 1, 2, 3 
        colorText.color = colory[colorlist[randcolor]]; // "Blue" = colorlist[2] - > colory["Blue"]
    }

}