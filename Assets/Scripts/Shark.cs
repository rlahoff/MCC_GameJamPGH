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

    static bool firstColorChange = false;

    // Use this for initialization
    void Start () {
        GameObject scoreGO = GameObject.Find("Shark Progress");

        if (!scoreGO)
        {
            if (LevelManager.IsLevel())
                Debug.LogWarning("Create a Canvas (if not present).  Place the 3 Shark Progress prefabs in it on this level");
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

    private void ChangeColorState()
    {
        Animator animator = GetComponent<Animator>();

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

        if (animator)
        {
            switch (my_comp_color)
            {
                case COMP_COLOR.Purple:
                    main.startColor = new Color(0.5f, 0f, 1f);   // purple
                    animator.Play("Yellow");
                    break;

                case COMP_COLOR.Red:
                    main.startColor = Color.red;
                    animator.SetTrigger("Friendly");
                    break;

                case COMP_COLOR.Orange:
                    main.startColor = new Color(1f, 0.5f, 0f);  // orange
                    animator.SetTrigger("Friendly");
                    break;

                default:
                    Debug.LogWarning("missing shark code");
                    break;
            }

            
            GameObject particles = Instantiate(hitParticles, transform.position, Quaternion.identity) as GameObject;

            AudioSource.PlayClipAtPoint(friendSound, transform.position, PPrefsMgr.GetSfxVolume());
            gameObject.layer = FRIENDLY_LAYER;
            if (score) // because no score on Start level
                score.AddToScore(scoreValue);
        }
    }
}

