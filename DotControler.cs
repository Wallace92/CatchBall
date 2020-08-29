using System.Collections.Generic;
using UnityEngine;

// Instantiate 3 dots each of different positions and colors
public class DotControler : MonoBehaviour
{
    public GameObject DotPrefab;
    
    private List<Vector2> dotsPositions = new List<Vector2> { new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f) };

    // Components of each dotsPosition
    private List<float> random_x = new List<float> { 0f, 0f, 0f };
    private List<float> random_y = new List<float> { 0f, 0f, 0f };

    private GameObject[] dots;

    Color[] colors = new Color[3];

    private int DotNumbers = 3;
    private bool callspawn = false;
    
    void Awake()
    {
        colors[0] = new Color(2f, 0, 0);
        colors[1] = new Color(0, 2f, 0);
        colors[2] = new Color(0, 0, 2f);

        //At start spawn three dosts
        dotsSpawn();
    }
    void Update()
    {
        
        if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == false)
        {
            ; //Debug.Log("Gra wciaz sie toyczy!");
        }
        else if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == true)
        {
            //Dzwiek klikniecia na kulke trwa 0.22 s, gdy sie skonczy dopiero mozna je zrestartowac
            callspawn = true;
            Invoke("Restart", 0.22f);
        }
        else
        {
            // Gracz zle kliknal, wiec trzeba skasowac kulki!
            Invoke("DeleteDots", 0.22f);   
        }
    }

    public void DeleteDots()
    {
        for (int i = 0; i < DotNumbers; i++)
        {
            Destroy(dots[i]);
        }
    }
    void Restart()
    {
        if (callspawn == true && GameControl.instance.gameOver == false)
        {
            //Gracz dobrze kliknal, wiec trzeba zrestartowac kulki!
           

            for (int i = 0; i < DotNumbers; i++)
            {
                Destroy(dots[i]);
            }
            dotsSpawn();

            GameControl.instance.add_best_time = true;
            GameControl.instance.clickedCorrect = false;
            callspawn = false;
        }
    }

    void dotsSpawn()
    {
        // Screen_min, Screen_max, dotRadius for x and y dimensions
        random_x = dotsPositionsFun(-13f, 13f, 2f);
        random_y = dotsPositionsFun(-18f, 18f, 2f);

        //Dots initial Positions
        for (int i = 0; i < dotsPositions.Count; i++)
        {
            dotsPositions[i] = new Vector2(random_x[i], random_y[i]);
        }
        // Dots List
        dots = new GameObject[DotNumbers];

        // Instantiate Dots
        for (int i = 0; i < DotNumbers; i++)
        {
            dots[i] =  Instantiate(DotPrefab, dotsPositions[i], Quaternion.identity);
        }
        // Assing appropriate color to instantiated dots
        for (int i = 0; i < DotNumbers; i++)
        {
            Renderer rend = dots[i].GetComponent<Renderer>();
            rend.material.color = colors[i];
        }
    }

    List<float> dotsPositionsFun(float a, float b, float dx)
    {
        // Three diffenrets walues fomr the interval (a,b)
        // Each newly dots is spawn at minimum distance dx from the next one
        List<float> rand = new List<float> { 0f, 0f, 0f };

        // Forbidden intervals, in this places the dots canno be instantiated
        List<List<float>> forbidden = new List<List<float>> { new List<float> { 0f, 0f }, new List<float> { 0f, 0f } };

        // First value and first forbbiden interval, because of 1 dot
        rand[0] = Random.Range(a, b);
        forbidden[0] = new List<float> { rand[0] - dx, rand[0] + dx };

        while (true)
        {
            rand[1] = Random.Range(a, b);

            // Second value must be less or greater then first one
            if (rand[1] < forbidden[0][0] || rand[1] > forbidden[0][1])
            {
                break;
            }
        }

        // Second forbidden interval
        forbidden[1] = new List<float> { rand[1] - dx, rand[1] + dx };

        while (true)
        {
            rand[2] = Random.Range(a, b);
            //Third dot cannot be near the first and the second one
            if ((rand[2] < forbidden[0][0] || rand[2] > forbidden[0][1]) && (rand[2] < forbidden[1][0] || rand[2] > forbidden[1][1]))
                {
                    break;
                }
        }
        return rand;
    }
}
