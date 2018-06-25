using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COLOR { Yellow, Green };

public class Snowflake : MonoBehaviour {

    //public enum COLOR { Yellow, Green };
    
    [SerializeField] COLOR my_Color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public COLOR Color()
    {
        return my_Color;
    }
}
