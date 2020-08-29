using UnityEngine;

public class BackgroundDontDestroy : MonoBehaviour
{
   
    //Two prefabs: skyOne and SkyTwo, one is next to the other
    public GameObject backgroundOne;
    public GameObject backgroundTwo;


    void Update()
    {
        // Move both prefabs with the same speed in -x direction, if the speed will increases an additional component of s = v * t should be included
        float speed = 1;
        backgroundOne.transform.localPosition += new Vector3(-8f, 0, 0) * Time.deltaTime * speed;
        backgroundTwo.transform.localPosition += new Vector3(-8f, 0, 0) * Time.deltaTime * speed;
        
        //Shift both backgrounds when the -x component is less them size of image. Some offset of 18 is included to satisfy the currently speed = 10
        if (backgroundOne.transform.localPosition.x < -6000f)
        {
            backgroundOne.transform.localPosition = new Vector3(5982f, 0f, 0f);
        }
        if (backgroundTwo.transform.localPosition.x < -6000f)
        {
            backgroundTwo.transform.localPosition = new Vector3(5982f, 0f, 0f);
        }

        // Acces to width of both images
        // float width = backgroundOne.GetComponent<SpriteRenderer>().bounds.size.x;
        // float width2 = backgroundTwo.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
