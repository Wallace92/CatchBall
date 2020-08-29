using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Level 0: EASY
// String "colorText" with content "red, green, blue" only in black color
public class ColorTextControler_black : MonoBehaviour
{
    public Text colorText;

    private List<string> colorlist = new List<string> {"RED", "GREEN", "BLUE"};

    private int randcolorText;
    private int textNumbers = 3;
    
    void Awake()
    {
        // Select one from three colors
        RandomColor();
    }
    void Update()
    {
        // Select again if game is over or player clicked correct
        if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == true)
        {
            RandomColor();
        }
    }
    void RandomColor()
    {
        randcolorText = Random.Range(0, textNumbers); ////0 "z"  0,1,2
        colorText.text = colorlist[randcolorText];   //"Red" = colorlist[0]
    }
}