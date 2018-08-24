using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluffy : MonoBehaviour {

    bool isFollowing = false;
    bool isScared = false;
    List <Collider2D> sharkColliders;

    float my_speed = .5f;

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        if (!player)
            Debug.LogWarning("No Player on this level");

        if (!GameObject.Find("PenguinDetector"))
            Debug.LogWarning("No FluffysPenguinDetector on this level");
    }

    public void FollowMe()
    {
        isFollowing = true;
    }

    void FixedUpdate () {

        if (isFollowing)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 5)
            {
                Vector3 vectorToPlayer = player.transform.position - transform.position;

                Vector3 wantToMoveTo = transform.position + (vectorToPlayer * my_speed * Time.deltaTime);

                bool okToMove = true;
                if (isScared)
                {
                    // if where I want to move to is farther to the shark, okay to move!

                    for (int i = 0; i < sharkColliders.Count; i++)
                    {
                        // if this would move me closer to a shark, don't do it!
                        if (Vector3.Distance(wantToMoveTo, sharkColliders[i].transform.position) < 
                            Vector3.Distance(transform.position, sharkColliders[i].transform.position))
                        {
                            okToMove = false;
                            break;
                        }
                    }  
                }
                
                if (okToMove)
                    transform.position += vectorToPlayer * my_speed * Time.deltaTime;
                // play with forces, didn't work, penguin just bounced back and forth
                //Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
                //Vector2 vector = new Vector2(vector3.x * my_speed, vector3.y * my_speed);
                //rigidbody.AddForce(vector, ForceMode2D.Impulse);
            }
        }
		
	}

    // add to the list of sharks that fluffy is colliding with, because she won't move near them
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            isScared = true;
            sharkColliders.Add(collision);
            // sharkPosition = collision.transform.position;
        }
    }

    // remove sharks from the list when fluffy is no longer colliding with them
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            for (int i = 0; i < sharkColliders.Count; i++)
                if (sharkColliders[i] == collision)
                {
                    sharkColliders.RemoveAt(i);
                }
            if (sharkColliders.Count == 0)
                isScared = false; 
        }
    }


}
