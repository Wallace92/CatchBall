using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class DontDestroy : MonoBehaviour
{
    public AudioClip BackgroundClip;
    AudioSource BackgroundAudio;

  //#if UNITY_IOS
  //    private string gameId = "3639335";
  //#elif UNITY_ANDROID
    public static string store_id = "3639335";
    public static string banner_ad = "TopBanner";
    public static string reward_video = "rewardedVideo";
    public static string video_ad = "video";
  //#endif

    private bool testMode = false;  // Testujemy! Gdy ta wersja bedzie opublikowana to dać false!

    private float soundVolume = 0.05f; // 0.05

    void Awake()
    {
        // Search the "Music" Tag which is assostiated with BackGroundMusic script
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        // When the music plays, this code protect us from playing it again, in this situation 
        // two or more music wont play in the same time
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        BackgroundAudio = GetComponent<AudioSource>();
        BackgroundAudio.volume = soundVolume;

        // Initialize advertisment! Gdy gra bedzie opublikowa to test Mode = false!
        Advertisement.Initialize(store_id, testMode);
        StartCoroutine(ShowAdd());
    }

    void Update()
    {
        // Mute audio when user click mute button
        if (MusicControl.musicInstance.audio_mute == true)
        {
            BackgroundAudio.mute = !BackgroundAudio.mute;
            MusicControl.musicInstance.audio_mute = false;
        }
    }

    IEnumerator ShowAdd()
    {
        while (!Advertisement.IsReady(video_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show(video_ad);
    }
}