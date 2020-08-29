using UnityEngine;

// Attached to dotPrefab
public class DotMove2 : MonoBehaviour
{
    private Rigidbody2D rb2d;

    Color tempolary_color;
    Color OnClick_Color;
    Renderer dot_color_rend;

    private string text_color;
    private string dot_color_string;

    private float ColorChangeOnClick = 2f;
    private float force = 20f;  // initial Value of dots

    // FOR ANDROID
    private string RED = "RGBA(2.000, 0.000, 0.000, 1.000)";
    private string GREEN = "RGBA(0.000, 2.000, 0.000, 1.000)";
    private string BLUE = "RGBA(0.000, 0.000, 2.000, 1.000)";

    /*
    // FOR PC
    private string RED = "RGBA(2,000, 0,000, 0,000, 1,000)";
    private string GREEN = "RGBA(0,000, 2,000, 0,000, 1,000)";
    private string BLUE = "RGBA(0,000, 0,000, 2,000, 1,000)";
    */
    
    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();

        dot_color_string = GetComponent<Renderer>().material.color.ToString();
        OnClick_Color = GetComponent<Renderer>().material.color;
        dot_color_rend = GetComponent<Renderer>();

        // Take the value of color from the text_color which the player shoudl click!
        text_color = GameControl.instance.colorText.text;
        if (text_color == "RED")
        {
            text_color = RED;
        }
        else if (text_color == "GREEN")
        {
            text_color = GREEN;
        }
        else if (text_color == "BLUE")
        {
            text_color = BLUE;
        }
        //Inistantiated dots by DotControll script will starts to move and collide!
        GoBall();
        
    }
    void Update()
    {
        // Normalize the dot velocity in order to posses the same velocity v = sqrt(vx**2 + vy**2) all time! 
        rb2d.velocity = GameControl.instance.dot_velocity * (rb2d.velocity.normalized);
       
        
    }

    void OnMouseOver()
    {
        

        // When the game starts every logical operators are fales so this condition is satisfied
        if (GameControl.instance.gameOver == false && GameControl.instance.clickedCorrect == false && GameControl.instance.clickedWrong == false && GameControl.instance.nextLevelCheck == false)
        {
        //   if (Input.GetMouseButtonDown(0))
            if (Input.touchCount >0)
            {
                

                // Change dot color on click
                tempolary_color = new Color(OnClick_Color.r / ColorChangeOnClick,
                                               OnClick_Color.g / ColorChangeOnClick,
                                               OnClick_Color.b / ColorChangeOnClick);
                dot_color_rend.material.color = tempolary_color;
               
                // If player clicked appropriate color - addScore! in other wavy its GameOver!
                if (text_color == dot_color_string)
                {
                    GameControl.instance.GameScore();

                }
                else
                {
                    GameControl.instance.GameOver();
                }
            }
        }
    }

    void GoBall()
    {
   
        // Initialize the direction of dot movements (4 differents +-x and +-y), 0 or 1
        int znak_x = Random.Range(0, 2);
        int znak_y = Random.Range(0, 2);

        // Random force in x direrection
        float fx = Random.Range(0f, force);
        float fy;

        // Fx direction, change it if znak_x = 0
        if (znak_x == 0)
        {
            fx = fx * (-1);
        }
        
        // Fy force magnitude is the same as fx 
        fy = Mathf.Sqrt(Mathf.Pow(force, 2) - Mathf.Pow(fx, 2));

        // Direction of fy force 
        if (znak_y == 0)
        {
            fy = fy * (-1);
        }

        rb2d.AddForce(new Vector2(fx, fy));

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Debuging the energy conservation in the system (all particles posses the same mass)
        // Debug.Log(Mathf.Pow(rb2d.velocity.x, 2) + Mathf.Pow(rb2d.velocity.y, 2));
        // After collision dots will have the same velocity - check update function as well 
        Vector2 vel;
        vel.x = rb2d.velocity.x;
        vel.y = rb2d.velocity.y;
        rb2d.velocity = vel;


    }
}
