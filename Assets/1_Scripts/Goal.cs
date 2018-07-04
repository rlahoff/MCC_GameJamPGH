using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    static int rotationCount = 0;
    static int steps = 16;
    static float timeCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timeCount += Time.deltaTime;
        if (timeCount > .25)
        {
            //float newRotation = 360 / steps * rotationCount;
            float newRotation = 360 / steps;
            //Debug.Log("newRotation" + newRotation);
            Vector3 rotateVector = new Vector3(0, 0, newRotation);
            transform.Rotate(rotateVector);
            rotationCount++;
            if (rotationCount > steps - 1)
                rotationCount = 0;
            timeCount = 0;
        }

    }

}
