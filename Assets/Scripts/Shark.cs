using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shark : MonoBehaviour {

    int scoreValue = 1;
    private Score score;

    public AudioClip friendSound;
    public GameObject hitParticles;

    const int FRIENDLY_LAYER = 10;

    [SerializeField] COMP_COLOR my_comp_color;
    [SerializeField] float changetime;

    static bool firstColorChange = false;

    // Use this for initialization
    void Start () {
        changetime = 0.15f;

        GameObject scoreGO = GameObject.Find("Shark Progress");

        if (!scoreGO)
        {
            if (LevelManager.IsLevel())
                Debug.LogWarning("Create a Canvas (if not present).  Place the 3 Shark Progress prefabs in it on this level");
        }
        else
            score = scoreGO.GetComponent<Score>();

        if (tag != "enemy")
            Debug.LogError("Shark tag not enemy, fix!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitByColorRay(COLOR color)
    {
        if (Snowflake.IsColorComplementary(color, my_comp_color))
            ChangeColorState();
        else
            Mock();
    }

    private void ChangeColorState()
    {
        //Animator animator = GetComponent<Animator>();

        if (!firstColorChange && LevelManager.GetLevel() == LevelManager.LEVELS.TUTORIAL0)
        {
            GameObject tutorial = GameObject.Find("TutorialCollider");
            if (tutorial)
            {
                GameObject tutorialText = GameObject.Find("Tutorial Text");
                tutorialText.GetComponent<TutorialText>().NextText();
            }
            firstColorChange = true;
        }

        ParticleSystem.MainModule main = hitParticles.GetComponent<ParticleSystem>().main;

        if (hitParticles.GetComponent<ParticleSystem>())
        {
            switch (my_comp_color)
            {
                case COMP_COLOR.Purple:
                    main.startColor = Color.yellow;  // yellow
                    break;

                case COMP_COLOR.Red:
                    main.startColor = Color.green;
                    break;

                case COMP_COLOR.Orange:
                    main.startColor = Color.blue;
                    break;

                default:
                    Debug.LogWarning("missing shark code");
                    break;
            }

            Instantiate(hitParticles, transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(friendSound, transform.position, PPrefsMgr.GetSfxVolume());
            gameObject.layer = FRIENDLY_LAYER;
            if (score) // because no score on Start level
                score.AddToScore(scoreValue);

            Invoke("ChangeToFriendly", changetime); // will change shark color after particles 
        }
    }

    void ChangeToFriendly() // used after particles 
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
            animator.SetTrigger("Friendly");
    }

    void Mock()
    {
        Debug.Log("Mock");
        Animator animator = GetComponent<Animator>();
        if (animator)
            animator.Play("Mock");

        Invoke("ChangeToEnemy", 0.75f); // will change shark color after mocking 
    }

    void ChangeToEnemy() // used after particles 
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
            animator.SetTrigger("Enemy");
    }

    /*
        void Party()
        {
            Animator animator = GetComponent<Animator>();
            if (animator)
                animator.SetTrigger("Party");
        }*/
}

