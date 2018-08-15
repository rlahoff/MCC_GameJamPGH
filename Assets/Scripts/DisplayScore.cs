using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// used to display the score on the final screen 
public class DisplayScore : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Text text = GetComponent<Text>();
        if (text) text.text = Score.GetGameScore().ToString();
        //Score.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
