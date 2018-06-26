using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    //private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // keep camera bounds inside level
    float xMin;
    float xMax;
    float xMinOffsetFromEdge = 0; // 20;  // TODO probably better to calculate this
    float xOffsetFromPlayer = 0; // 10;  // TODO probably better to calculate this

    float yMin;
    float yMax;
    float yMinOffsetFromEdge = 0; // 10;  // TODO probably better to calculate this
    float yOffsetFromPlayer = 0; // 10;  // TODO probably better to calculate this

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        player = GameObject.Find("Player");
        //Debug.Log("Player pos: " + player.transform.position);

        GameObject levelBoundaryLowerLeft = GameObject.Find("LevelBoundaryLowerLeft");
        GameObject levelBoundaryUpperRight = GameObject.Find("LevelBoundaryUpperRight");

        if (!levelBoundaryLowerLeft)
            Debug.LogWarning("Place a LevelBoundary named LevelBoundaryLowerLeft on this level");
        if (!levelBoundaryUpperRight)
            Debug.LogWarning("Place a LevelBoundary named LevelBoundaryUpperRight on this level");

        //Debug.Log("lower left: " + levelBoundaryLowerLeft.transform.position );
        //Debug.Log(levelBoundaryUpperRight.transform.position);
        //Debug.Log("camera: " + transform.position); 

        // how to get the size of what the camera is seeing
        float distance = player.transform.position.z - GetComponent<Camera>().transform.position.z;
        Vector3 leftMost = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 righttMost = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1, 1, distance));
        //Debug.Log("leftMost " + leftMost);
        //Debug.Log("righttMost " + righttMost);
        //Vector3 topMost = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 1, distance));
        //Vector3 bottomtMost = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0, distance));

        xMinOffsetFromEdge = (righttMost.x - leftMost.x) / 2.0f;
        yMinOffsetFromEdge = (righttMost.y - leftMost.y) / 2.0f;
        //yMinOffsetFromEdge = (topMost.y - bottomtMost.y) / 2.0f;
        //Debug.Log(xMinOffsetFromEdge + " yMinOffsetFromEdge = " + yMinOffsetFromEdge);

        xMin = levelBoundaryLowerLeft.transform.position.x + xMinOffsetFromEdge;
        xMax = levelBoundaryUpperRight.transform.position.x - xMinOffsetFromEdge;
        //Debug.Log("xmin " + xMin + " " + xMax);

        yMin = levelBoundaryLowerLeft.transform.position.y + yMinOffsetFromEdge;
        yMax = levelBoundaryUpperRight.transform.position.y - yMinOffsetFromEdge;
        //Debug.Log("ymin " + yMin + " " + yMax);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        // Debug.Log(transform.position.x);

        //float xpos = player.transform.position.x + offset.x - xOffsetFromPlayer;
        //float ypos = player.transform.position.y + offset.y - yOffsetFromPlayer;

        float xpos = player.transform.position.x - xOffsetFromPlayer;
        float ypos = player.transform.position.y - yOffsetFromPlayer;

        xpos = Mathf.Clamp(xpos, xMin, xMax);
        ypos = Mathf.Clamp(ypos, yMin, yMax);

        transform.position = new Vector3(xpos, ypos, transform.position.z);
        //Debug.Log("camera pos" + transform.position);
    }
}
