using System.Collections.Generic; // Lista
using UnityEngine;
using UnityEngine.UI;
using System; //Math
using System.IO;

//This script is attached to sprite! 
public class Timer : MonoBehaviour
{
    public Text timeText;
    public Text initialTimeText;
    public Text initialVelocityText;

    public AudioClip LevelUpAudio;
    AudioSource NewLevelAudio;

    private SpriteRenderer sprRend;

    public static List<float> reactiontimes = new List<float> { };

    Vector2 czasy;

    private bool soundPlay;

    private float width;
    private float height;
    private float tempTime;
    private float secondsCount;
    private float new_time;
    private float initial_time;
    private float offsetTime = 0.22f;

    int averageBonus;
    int bestBonus;
    
    public static long wynik;

    void Awake()
    {
        // Width and height of the sprite
        sprRend = GetComponent<SpriteRenderer>();
        width = sprRend.size.x;
        height = sprRend.size.y;

        //Offset = 0.22s due to invoke function! Time need to listen the click sound!

        initial_time = GameControl.instance.initialTimeSecondLevel; //+ offsetTime;
        GameControl.instance.add_best_time = true;

        //Time and velocities UI text
        initialTimeText.text = "TIME: " + GameControl.instance.initialTimeSecondLevel;
        initialVelocityText.text = "SPEED: " + GameControl.instance.dot_velocity;
      
        NewLevelAudio = GetComponent<AudioSource>();
        soundPlay = false;

        //Reset times when round starts (sometimes few times if player clickedWrong)
        reactiontimes.Clear();

    }
    void Update()
    {
        //If player clicked correct, this condition is satisfied for 0.22 due to invoke function
        if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == true)
        {
            //Add bestTime to list -> each round
            if (GameControl.instance.add_best_time == true)
            {
                tempTime = secondsCount;
                reactiontimes.Add(tempTime);
                GameControl.instance.add_best_time = false;
            }
            //Reset time before next round!
            secondsCount = 0;
            sprRend.size = new Vector2(width, height);
        }
        //If player not clicked yet!
        secondsCount += Time.deltaTime;
        new_time =  initial_time - secondsCount;  // 10,9,7 .... 1, 0, -1...

        //When the time of the round not passed 
        if (new_time > 0)
        {
            // If the Player clicked correct and the final scene are not discovered yet show the scores of the round
            if (GameControl.instance.nextLevelCheck == true) // && GameControl.instance.finalScene == false)
            {
                //Times for this round!
                czasy = ReactionTime();
                //Calculated bonuses of this round
                long showScore = calculateScoresTimer(); 

                // Average, best Time
               // if (GameControl.instance.finalScene == false)
             //   { }
                GameControl.instance.ReactionTimesText.gameObject.SetActive(true);

                //Numbers
                GameControl.instance.MeantimeText.text =  czasy.y.ToString().Replace(",", ".") + " s | " + czasy.x.ToString().Replace(",", ".") + " s";
                GameControl.instance.MeantimeText.gameObject.SetActive(true);

                //Scores!
                GameControl.instance.ReactionTimeText.text = "SCORE: " + showScore;
                GameControl.instance.ReactionTimeText.gameObject.SetActive(true);

                if (!soundPlay)
                {
                    NewLevelAudio.PlayOneShot(LevelUpAudio);
                    soundPlay = true;
                }
            }  
               
            /*
            Zanim uzytkownik podejmnie akcje to clickedCorrect jak i clickedWrong sa false
            dlatego warunek jest spelniony i czas plynie - prostokat sie zmniejsza
            gdy nastapi klikniecie to jedna ze zmiennych przyjmnie wartosc true i warunek nie jest
            juz spelniony. Prostokat przestaje sie zmniejszac, funkcja (RectLenght) nie jest juz wywolywana,
            ale zmienna secondCount caly czas rosnie rosnie i po uplywnie np. 5 sekund
            new_time bedzie <0
            */
            // Shrink the rectangle if they player not clicked at the dots or nextLevel screen is not appear
            if (GameControl.instance.clickedCorrect == false && GameControl.instance.clickedWrong == false && GameControl.instance.nextLevelCheck == false)
            {
                timeText.text = new_time.ToString("0.0").Replace(",", ".");
                RectLenght();
            }

            if (GameControl.instance.clickedCorrect == false && GameControl.instance.clickedWrong == true)
            {
                sprRend.size = new Vector2(0, height);
            }
        }
        else
        {  
            /*
             Gdy czas jest ujemny, a bedzie zawsze po przekroczeniu 5/4/3/2/1 s w zaleznosci od initial time
             to w przypadku, gdy gracz kliknal w prawidlowy obiekt (clickedCorrent == true)
             w innym przypadku czas juz minal i gracz przegral (gameOver)
            */
            if (GameControl.instance.clickedCorrect == true || GameControl.instance.nextLevelCheck == true)
            {
                if (GameControl.instance.nextLevelControl == true)
                {
                    // Do nothing!;
                }
                   
            }
               
            else
            {
                // Your times is ends it GameOver!
                sprRend.size = new Vector2(0, height);
                GameControl.instance.GameOver();
            }
        }
    }
    long calculateScoresTimer()
    {
        float bestTime = czasy.x;

        float averageTime = czasy.y;

        // Linear approximation of scores
        // Best time: y = - 50 * t + 200, y(0) = 200, y(4) = 0
        // Average time: y = - 20 * t + 200, y(0) = 200, y(10) = 0

        if (bestTime > 4)
        {
            bestBonus = 0;
        }
        else
        {
            bestBonus = (int) (-50 * bestTime + 200);
        }
        if (averageTime > 10)
        {
            averageBonus = 0;
        }
        else
        {
            averageBonus = (int)(-20 * averageTime + 200);
        }
 
        float pointForLevels =  200;
      //  Debug.Log("bestTime " + bestTime + " averageTime " + averageTime + " 500 for LEVEL" + " bestBonus " + bestBonus + " averageBonus" + averageBonus);
        wynik = (long) (pointForLevels + bestBonus  + averageBonus);
        return wynik;
    }

 
    void RectLenght()
    {
        sprRend.size -= new Vector2(width * (Time.deltaTime / initial_time), 0);
    }

    Vector2 ReactionTime()
    {
        float suma = 0;
        float min = 10;
        float srednia;
        foreach (float i in reactiontimes)
        {
            if (i < min)
                min = i;
            //Debug.Log(i);
            suma += i;
        }
        min = (float) Math.Round(min, 3);
        //Debug.Log("Wartosc minimalna: " + min);
        srednia = suma / reactiontimes.Count;
        srednia = (float) Math.Round(srednia, 3);
        return new Vector2(min, srednia);
    }
}
