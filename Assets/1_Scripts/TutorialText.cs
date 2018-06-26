using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TUTORIAL_STATE { UNFRIENDLY, SPACEBAR, FRIENDLY, SNOWFLAKE, STAR };

public class TutorialText : MonoBehaviour {

    int count = 0;
    string[] instructions = {"Unfriendly shark!", "Press spacebar to shoot ", "Friendly shark!",
        "Snowflake changes your color", "Star exits level" }; 
     

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextText()
    {
        GetComponent<Text>().text = instructions[count];
        count++;
    }
}
