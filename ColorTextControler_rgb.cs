using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Level 1: MEDIUM
// String "colorText" with content "red, green, blue" only in red, gree or blue color
public class ColorTextControler_rgb : MonoBehaviour
{
    public Text colorText;

    private List<string> colorlist = new List<string> { "RED", "GREEN", "BLUE" };

    private Dictionary<string, Color> colory = new Dictionary<string, Color>();

    private int randcolorText;
    private int randcolor;
    private int textNumbers = 3;

    void Awake()
    {
        // Add three colors to the list and select one of them
        colory.Add("RED", new Color(2f, 0, 0));
        colory.Add("GREEN", new Color(0, 2f, 0));
        colory.Add("BLUE", new Color(0, 0, 2f));

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
        randcolorText = Random.Range(0, textNumbers);     //0 "z"  0, 1, 2
        colorText.text = colorlist[randcolorText];        // "Red" = colorlist[0]

        // Random color from red, green, blue
        randcolor = Random.Range(0, textNumbers);         // 2 "z" 0, 1, 2                                           
        colorText.color = colory[colorlist[randcolor]];   // "Blue" = colorlist[2]   - > colory["Blue"]
    }

}