using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    string[] levelNames = { "", "Tutorial", "", "Adventure", "You shall not pass!", "Shark Tower", "Night", "Icebergs", "Sharkfest" };

    void Start()
    {
        GameObject text = GameObject.Find("LevelName");
        if (text)
        {
            text.GetComponent<Text>().text = GetLevelName();
        }
        else
            Debug.Log("no LevelName found");

        Debug.Log(GetLevelName());
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

    public void QuitRequest()
    {
        //Debug.Log("Quit requested");
        Application.Quit();
    }
}
