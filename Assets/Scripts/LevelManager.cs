using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // these two should match
    string[] levelNames = 
                  { "Splash", "Start", "Tutorial", "Intermission", "Adventure", "You shall not pass!", "Shark Tower", "NightCross",

                    "Icebergs", "Sharkfest", "Final Challenge", "Final", "Options", "Z Test", "Z Test Transition" };
    public
    enum LEVELS   { SPLASH, START, TUTORIAL0, INTERMISSION, ADVENTURE1, NOTPASS2, TOWER3, NIGHTCROSS4, 

                     ICEBERGS10, SHARKFEST11, FINAL12, SCORE, OPTIONS, TEST, TEST2 };

    static LEVELS thisLevel;

    public float autoLoadNextLevelDelay;

    static bool sfxVolumeInitialized = false;

    void Start()
    {
        if (autoLoadNextLevelDelay > 0) // probably only used for the splash screen
            Invoke("LoadNextLevel", autoLoadNextLevelDelay);

        Debug.Log("******* Level " + GetLevelName() + " *******");
        thisLevel = (LEVELS)SceneManager.GetActiveScene().buildIndex;

        // initial sfx volume from disk
        if (!sfxVolumeInitialized)
        {
            PPrefsMgr.GetSfxVolumeFromDisk();
            sfxVolumeInitialized = true;
        }

        GameObject text = GameObject.Find("LevelName");
        if (text)
        {
            text.GetComponent<Text>().text = GetLevelName();
        }
        else if (IsLevel())
            Debug.LogWarning("no LevelName text gameobject found in canvas");

        StartByLevel();

        GameObject gO = GameObject.Find("AudioAmbient");
        if (gO)
             Debug.LogError("Level " + GetLevelName() + ": AudioAmbient gameobject found, remove from this level");

        //Debug.Log(GetLevelName());
    }

    void StartByLevel()
    {
        switch(thisLevel)
        {
            case LEVELS.START:
                // we set the fade screen to inactive so it isn't blocking our view while working in unity
                // so we have to turn it on when we actually run the game
                GameObject dialog = GameObject.Find("FadeScreenParent");
                if (dialog)
                {
                    dialog.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case LEVELS.ADVENTURE1:
                Score.Reset();
                break;
        }
    }

    public static bool IsLevel()
    {
        bool islevel = true;
        switch(thisLevel)
        {
            case LEVELS.SPLASH:
            case LEVELS.START:
            case LEVELS.INTERMISSION:
            case LEVELS.SCORE:
            case LEVELS.OPTIONS:
            case LEVELS.TEST:
            case LEVELS.TEST2:
                islevel = false;
                break;
        }
        return islevel;
    }

    public static LEVELS GetLevel()
    {
        return thisLevel;
    }

    public void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int nextScene = scene.buildIndex + 1;

 //       if (nextScene < SceneManager.sceneCount)
            SceneManager.LoadScene(nextScene);
 //       else
 //           SceneManager.LoadScene("TestTransition");
    }

    public void LoadLevel(string name)
    {
        //Debug.Log ("New Level load: " + name);
        if (name == "Start")
            Score.Reset();

        SceneManager.LoadScene(name);
    }

    public string GetLevelName()
    {
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        return levelNames[SceneManager.GetActiveScene().buildIndex];
    }

    public void QuitButtonCall()
    {
        GameObject dialog = GameObject.Find("QuitDialogParent");
        if (dialog)
        {
            dialog.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void QuitButtonCancel()
    {
        GameObject dialog = GameObject.Find("QuitDialogParent");
        if (dialog)
        {
 //           Debug.Log("setactive");
            dialog.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

 /*   public void QuitRequest()
    {
        //Debug.Log("Quit requested");
        Application.Quit();
    }
    */
}
