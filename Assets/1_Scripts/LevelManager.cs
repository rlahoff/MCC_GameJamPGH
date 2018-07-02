using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // these two should match
    string[] levelNames = 
                  { "", "Tutorial", "", "Adventure", "You shall not pass!", "Shark Tower", "Night",

                    "Icebergs", "Sharkfest", "Final Challenge", "", "", "" };
    public
    enum LEVELS   { START, TUTORIAL0, INTERMISSION, ADVENTURE1, NOTPASS2, TOWER3, NIGHT4, 

                     ICEBERGS10, SHARKFEST11, FINAL12, SCORE, TEST, TEST2 };

    LEVELS thisLevel;

    void Start()
    {
        GameObject text = GameObject.Find("LevelName");
        if (text)
        {
            text.GetComponent<Text>().text = GetLevelName();
        }
        else
            Debug.Log("no LevelName text found");

        thisLevel = (LEVELS)SceneManager.GetActiveScene().buildIndex;

        GameObject gO = GameObject.Find("AudioAmbient");
        if (!gO)
             Debug.LogError("Level " + GetLevelName() + ": no AudioAmbient found");

        //Debug.Log(GetLevelName());
    }

    public LEVELS GetLevel()
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
