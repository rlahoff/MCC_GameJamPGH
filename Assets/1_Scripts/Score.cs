using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    //public static int score = 0;
    static int gameScore = 0;
    int currentSharkCount;
    int maxSharkCount;

    public float maxScaleX;

    // Use this for initialization
    void Start()
    {
        maxScaleX = transform.localScale.x;
        
        currentSharkCount = 0;


        maxSharkCount = 5;

        UpdateProgressBar();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Reset()
    {
        //Debug.Log("ScoreReset");

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
        if (maxSharkCount > 0)
        {
            RectTransform rectTrans = GetComponent<RectTransform>();
            rectTrans.transform.localScale = new Vector3((currentSharkCount / maxSharkCount) * maxScaleX, rectTrans.transform.localScale.y);
        }
        else
            Debug.LogError("maxSharkCount <= 0");  
     }
}
