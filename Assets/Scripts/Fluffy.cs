using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluffy : MonoBehaviour {

    bool isFollowing = false;
    float my_speed = .5f;

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        if (!player)
            Debug.LogWarning("No Player on this level");
    }
	
	
	void FixedUpdate () {

        if (isFollowing)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 5)
            {
                //transform.position = new Vector3(player.transform.position.x, player.transform.position.y-5f, player.transform.position.z);
                Vector3 vector = player.transform.position - transform.position;
                //if (vector.)
                transform.position += vector * my_speed * Time.deltaTime;
            }
        }
		
	}

    public void FollowMe()
    {
        isFollowing = true;
    }
}
