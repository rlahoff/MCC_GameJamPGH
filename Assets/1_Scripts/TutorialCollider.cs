using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if ( player )
        {
            GameObject tutorialText = GameObject.Find("Tutorial Text");
            tutorialText.GetComponent< TutorialText>().NextText();

        }
    }
}
