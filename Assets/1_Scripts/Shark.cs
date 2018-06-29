using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

    int scoreValue = 1;
    private Score score;

    public AudioClip friendSound;             // andrea

    const int FRIENDLY_LAYER = 10;

    [SerializeField] COMP_COLOR my_comp_color;

    static bool firstColorChange = false;

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

        if (!firstColorChange)
        {
            GameObject tutorial = GameObject.Find("TutorialCollider");
            if (tutorial)
            {
                GameObject tutorialText = GameObject.Find("Tutorial Text");
                tutorialText.GetComponent<TutorialText>().NextText();
            }
            firstColorChange = true;
        }

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
            AudioSource.PlayClipAtPoint(friendSound, transform.position);
            gameObject.layer = FRIENDLY_LAYER;
            if (score) // because no score on Start level
                score.AddToScore(scoreValue);
        }


    }
}
