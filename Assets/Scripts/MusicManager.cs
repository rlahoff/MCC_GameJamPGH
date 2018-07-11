using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip levelMusic;

    AudioSource audioSource;

    // this causes the music manager to persist accross levels
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            Debug.LogWarning("audioSource component not found");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //the following three functions replace the depracated private void OnLevelWasLoaded(int level)
    void OnEnable()
    {
        //Tell our 'OnLevelLoaded' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelLoaded' function to stop listening for a scene change as soon as this script is disabled. 
        // Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //    if (levelMusic[scene.buildIndex])
        //    {
        //        audioSource.clip = levelMusic[scene.buildIndex];
        // play the ambient water sounds everywhere but the splash screen

        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource)
                Debug.LogWarning("audioSource component not found");
        }


        if (levelMusic /*&& LevelManager.GetLevel() != LevelManager.LEVELS.SPLASH*/)
        {
            audioSource.clip = levelMusic;
            audioSource.loop = true;
            audioSource.volume = 1;// PPrefsMgr.GetMusicVolume();
            audioSource.Play();
        }
        else
            Debug.LogWarning("levelMusic not set in inspector");
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
