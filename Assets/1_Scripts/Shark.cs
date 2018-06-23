using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PowerRay powerRay = collision.gameObject.GetComponent<PowerRay>();

        if (powerRay)
        {
            ChangeColorState();
            powerRay.Hit();
        }
    }

    private void ChangeColorState()
    {
        //Animator anim = GetComponent<Animator>();
        Animation anim = GetComponent<Animation>();
        //AnimationClip anim = GetComponent<AnimationClip>();
        Debug.Log("anim " + anim );
        
        if (anim && anim.IsPlaying("Purple"))
        {
            Debug.Log("Purple");
            //anim.Stop();
            //anim.Play("Yellow");
        }
    }
}
