using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

   static int gameScore = 0;
    int currentSharkCount;
    int maxSharkCount;

    float maxScaleX;
    Vector3 originalPosition;
    float originalWidth;

    // Use this for initialization
    void Start()
    {
        maxScaleX = GetComponent<RectTransform>().transform.localScale.x;
        originalPosition = GetComponent<RectTransform>().transform.position;
        originalWidth = GetComponent<RectTransform>().rect.width;

        currentSharkCount = 0;
        maxSharkCount = SharksOnThisLevel();
        Debug.Log("maxSharksCount " + maxSharkCount );

        UpdateProgressBar();
    }

    public static int SharksOnThisLevel()
    {
        int sharkCount = 0;

        GameObject sharksGO = GameObject.Find("Sharks");

        if (!sharksGO)
        {
            Debug.LogWarning("Create an Empty Gameobject and name it Sharks.  Put all sharks in it. ");
        }
        else
            sharkCount = sharksGO.transform.childCount;

        return sharkCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Reset()
    {
        Debug.Log("ScoreReset");

        gameScore = 0;
    }

    public static int GetGameScore()
    {
        return gameScore;
    }

    public void AddToScore(int points)
    {
        gameScore += points;
        currentSharkCount += points;
        UpdateProgressBar();
  
    }

    private void UpdateProgressBar()
    {
        if (SceneManager.GetActiveScene().name == "Intermission") return;

        // keep the pivot of the progress bar the same as the outline or there will be trouble!
        //maxSharkCount = 10;
        // i think it's an art problem and i'm going to try to work around it
        if (maxSharkCount > 0)
        {
            ProgressBar();
        }
        else
            Debug.LogError("maxSharkCount <= 0");  
     }

 void ProgressBar()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();
        //Debug.Log("position: " + rectTrans.transform.position);
        //Debug.Log("original: " + originalPosition);
        //Debug.Log("width   : " + rectTrans.rect.width);
        // Debug.Log("maxSharkCount   : " + maxSharkCount);

        //Debug.Log(GetComponent<RectTransform>().transform.localScale.x);
        float percentageComplete = (float)currentSharkCount / (float)maxSharkCount;
        float newx = maxScaleX * percentageComplete;
        //Debug.Log("UpdateProgressBar" + " " + maxScaleX + " " + currentSharkCount + " = " + newx);

        //Debug.Log("before: " + rectTrans.transform.localScale);
        rectTrans.transform.localScale = new Vector3(newx, rectTrans.transform.localScale.y, rectTrans.transform.localScale.z);

        //float shiftx = (float)rectTrans.rect.width * (1f - percentageComplete);
        float shiftx = originalWidth * (1f - percentageComplete);

        // the bad art (too much transparent space to the left and right resulted in a faded progress bar for levels 
        // with lots of sharks

        // kluge , add 10%?, not sure why I still have this problem after getting art w/o transparent space on left and right
        // test smaller kluge
        shiftx += (originalWidth * (1f - percentageComplete)) / 15f;

        //Debug.Log(percentageComplete + " newx = " + newx + " shiftX " + shiftx);
        //Debug.Log("shiftx = " + shiftx);
        //Debug.Log("after: " + rectTrans.transform.localScale);
        RectTransform rectT = GetComponent<RectTransform>();
        rectT.transform.position = originalPosition - new Vector3(shiftx, 0, 0);
        //Debug.Log("rectT.transform.position = " + rectT.transform.position.x);
    }
}

