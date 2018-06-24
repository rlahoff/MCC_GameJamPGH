using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

    public int scoreValue = 100;
    private Score score;

    const int FRIENDLY_LAYER = 10;

	// Use this for initialization
	void Start () {
        score = GameObject.Find("ScoreUI").GetComponent<Score>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HitByPowerRay(string color)
    {
            ChangeColorState();
    }

 /*   private void OnOnTriggerEnter2D(Collider2D collision)
    {

        PowerRay powerRay = collision.gameObject.GetComponent<PowerRay>();

        if (powerRay)
        {
            ChangeColorState();
            powerRay.Hit();
        }
    }*/

    private void ChangeColorState()
    {
        Animator animator = GetComponent<Animator>();

        //Get the animator clip information from the Animator Controller
        //AnimatorClipInfo[] m_AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);

        //Output the name of the starting clip
        //Debug.Log("Starting clip : " + m_AnimatorClipInfo[0].clip);
        //Debug.Log("animator " + animator); // shark
        
        if (animator /*&& anim.IsPlaying("Purple")*/)
        {
            animator.Play("Yellow");
            gameObject.layer = FRIENDLY_LAYER;
            score.AddToScore(scoreValue);
        }


    }
}
