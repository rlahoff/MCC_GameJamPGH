using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

    public int scoreValue = 1;
    private Score score;

    const int FRIENDLY_LAYER = 10;

    [SerializeField] COMP_COLOR my_comp_color;

    // Use this for initialization
    void Start () {
        GameObject scoreGO = GameObject.Find("Shark Progress");

        if (!scoreGO)
        {
            Debug.LogWarning("Create a Canvas (if not present).  Place a Shark Progress in it on this level");
        }
        else
            score = scoreGO.GetComponent<Score>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitByColorRay(COLOR color)
    {
        if (Snowflake.IsColorComplementary(color, my_comp_color))
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
            switch(my_comp_color)
            {
                case COMP_COLOR.Purple:
                    animator.Play("Yellow");
                    break;

                case COMP_COLOR.Red:
                    animator.SetTrigger("Friendly");
                    break;

                case COMP_COLOR.Orange:
                    animator.SetTrigger("Friendly");
                    break;

                default:
                    Debug.LogWarning("missing shark code");
                    break;
            }
            gameObject.layer = FRIENDLY_LAYER;
            score.AddToScore(scoreValue);
        }


    }
}
