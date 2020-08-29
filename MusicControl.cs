using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl musicInstance;

    public bool audio_mute = false;
   
    void Awake()
    { 
        if (musicInstance == null)
        {
            musicInstance = this;
        }
        else if (musicInstance != this)
        {
            Destroy(gameObject);
        }
    }
}
