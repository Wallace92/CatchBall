using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCanvas : MonoBehaviour
{

    public static bool screenMoved = false;
    public static int currentLevelNumber;
    private Vector2 startTouchPosition, endTouchPosition;
    float easing = 0.5f;

    void Start()
    {
        // Intentiaol shift of all levels corresponding to the currentLevel
        if (LevelChangerController.currentScene == "Level0")
        {
            if (LevelEasyController.distanceToMiddlePoint != 0)
            {
                transform.localPosition += new Vector3(0, LevelEasyController.distanceToMiddlePoint, 0);
                screenMoved = true;
            }
        }
        else if (LevelChangerController.currentScene == "Level1")
        {
            if (LevelMediumController.distanceToMiddlePoint != 0)
            {
                transform.localPosition += new Vector3(0, LevelMediumController.distanceToMiddlePoint, 0);
                screenMoved = true;
            }
        } 
        else if (LevelChangerController.currentScene == "Level2")
        {
            if (LevelHardController.distanceToMiddlePoint != 0)
            {
                transform.localPosition += new Vector3(0, LevelHardController.distanceToMiddlePoint, 0);
                screenMoved = true;
            }
        }
          
       
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.y < startTouchPosition.y)
            {
                {
                    StartCoroutine(SmoothMove(transform.localPosition, transform.localPosition + new Vector3(0, -1000, 0), easing));
                    screenMoved = true;
                }

            }
            else
            {
                {
                    StartCoroutine(SmoothMove(transform.localPosition, transform.localPosition + new Vector3(0, 1500, 0), easing));
                    screenMoved = true;
                }
            }

        }
        /*
        if (LevelChangerController.currentScene == "Level0")
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began )
            {
                startTouchPosition = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;

                if (endTouchPosition.y < startTouchPosition.y)
                {
                    {
                        StartCoroutine(SmoothMove(transform.localPosition, transform.localPosition + new Vector3(0, -1000, 0), easing));
                        screenMoved = true;
                    }
                   
                }
                else 
                {
                    {
                        StartCoroutine(SmoothMove(transform.localPosition, transform.localPosition + new Vector3(0, 1500, 0), easing));
                        screenMoved = true;
                    }
                }

            }
           
            // Scrolls of levles
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && LevelEasyController.lastItemPosition.y < -850)
            {
                transform.localPosition += new Vector3(0, 100, 0);
                screenMoved = true;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f && LevelEasyController.firstItemPosition.y > 550)
            {
                transform.localPosition += new Vector3(0, -100, 0);
                screenMoved = true;
            }
            
        }
        else if (LevelChangerController.currentScene == "Level1")
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && LevelMediumController.lastItemPosition.y < -850)
            {
                transform.localPosition += new Vector3(0, 100, 0);
                screenMoved = true;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f && LevelMediumController.firstItemPosition.y > 550)
            {
                transform.localPosition += new Vector3(0, -100, 0);
                screenMoved = true;
            }
        }
        else if (LevelChangerController.currentScene == "Level2")
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && LevelHardController.lastItemPosition.y < -850)
            {
                transform.localPosition += new Vector3(0, 100, 0);
                screenMoved = true;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f && LevelHardController.firstItemPosition.y > 550)
            {
                transform.localPosition += new Vector3(0, -100, 0);
                screenMoved = true;
            }
        }
      */

    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while ( t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void buttonOnClick()
    {
        // Take the number of clicked level
        currentLevelNumber = int.Parse(gameObject.GetComponentInChildren<Text>().text);
        if (LevelChangerController.currentScene == "Level0")
        {
            SceneManager.UnloadSceneAsync("LevelEasyChanger");
            SceneManager.LoadSceneAsync("Level0", LoadSceneMode.Additive);
        }
        else if (LevelChangerController.currentScene == "Level1")
        {
            SceneManager.UnloadSceneAsync("LevelMediumChanger");
            SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
        }
        else if (LevelChangerController.currentScene == "Level2")
        {
            SceneManager.UnloadSceneAsync("LevelHardChanger");
            SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Additive);
        }
          
   
    }
}
