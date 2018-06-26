using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score = 0;
    static Text myText;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        // just want to set the text if at the beginning of the game
        if (score == 0)
            Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Reset()
    {
        //Debug.Log("ScoreReset");

        score = 0;
        if (myText) myText.text = score.ToString();
    }

    public void AddToScore(int points)
    {
        score += points;
        myText.text = score.ToString();
    }
}
