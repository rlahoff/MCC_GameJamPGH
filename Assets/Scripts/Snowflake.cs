using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COLOR { Yellow, Green, Blue, COLOR_COUNT };
public enum COMP_COLOR { Purple, Red, Orange, COLOR_COUNT };

public class Snowflake : MonoBehaviour {

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
