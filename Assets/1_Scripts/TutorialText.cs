using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TUTORIAL_STATE { UNFRIENDLY, SPACEBAR, FRIENDLY, SNOWFLAKE, STAR };

public class TutorialText : MonoBehaviour {

    int count = 0;
    string[] instructions = {"Can't pass unfriendly shark!", "Press spacebar to shoot ", "Swim through Friendly shark!",
        "Snowflake changes your color", "Star exits level" }; 
     

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public TUTORIAL_STATE GetTutorialState()
    {
        return (TUTORIAL_STATE)(count - 1);
    }

    public void NextText()
    {
        GetComponent<Text>().text = instructions[count];
        count++;
    }
}
