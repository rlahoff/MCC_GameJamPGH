using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COLOR { Yellow, Green, COLOR_COUNT };
public enum COMP_COLOR { Purple, Red, COLOR_COUNT };

public class Snowflake : MonoBehaviour {

    //public enum COLOR { Yellow, Green };
    
    [SerializeField] COLOR my_Color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public bool IsColorComplementary(COLOR color, COMP_COLOR comp_color)
    {
        if ((int)color == (int)comp_color)
            return true;
        else
            return false;
    }


    public COLOR Color()
    {
        return my_Color;
    }
}
