using UnityEngine;

public class StarControler : MonoBehaviour
{
    public GameObject StarPrefab;
    private GameObject[] stars;

    private int StarNumbers;
    
    Color gray;
    Color white;

    void Start()
    {
        StarNumbers = GameControl.instance.StarNumbers;
        stars = new GameObject[StarNumbers];
        
        gray = Color.gray;
        white = Color.white;

        if (LevelChangerController.currentScene == "Level0")
        {
            // Instantiate starts
            for (int i = 0; i < StarNumbers; i++)
            {
                Vector2 start_position = new Vector2(-24f + 5.5f * i, -39f);
                stars[i] = Instantiate(StarPrefab, start_position, Quaternion.identity);
            }
        }
        else if (LevelChangerController.currentScene == "Level1")
        {
            // Instantiate starts
            for (int i = 0; i < StarNumbers; i++)
            {
                Vector2 start_position = new Vector2(-24f + 5.5f * i, -33f);
                stars[i] = Instantiate(StarPrefab, start_position, Quaternion.identity);
            }
        }
        else if (LevelChangerController.currentScene == "Level2")
        {
            // Instantiate starts
            for (int i = 0; i < StarNumbers; i++)
            {
                Vector2 start_position = new Vector2(-24f + 5.5f * i, -33f);
                stars[i] = Instantiate(StarPrefab, start_position, Quaternion.identity);
            }
        }
       
        
        // At start all stars are gray
        for (int i = 0; i < StarNumbers; i++)
        {
            Renderer rend = stars[i].GetComponent<Renderer>();
            rend.material.color = gray;
        }
    }

    void Update()
    {
        //If clickedCorrect one star will color to white 
        if (GameControl.instance.clickedCorrect == true)
        {
            // - 1 because at the beggining an player posses score 0
            Renderer rend = stars[GameControl.instance.score - 1].GetComponent<Renderer>();
            rend.material.color = white;
        }

    }

    public void DeleteStart()
    {
        for (int i = 0; i < StarNumbers; i++)
        {
            Destroy(stars[i]);
        }
    }
}
