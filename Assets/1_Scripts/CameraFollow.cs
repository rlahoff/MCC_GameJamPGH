using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // keep camera bounds inside level
    float xMin;
    float xMax;
    float xMinOffsetFromEdge = 20;  // TODO probably better to calculate this
    float xOffsetFromPlayer = 10;  // TODO probably better to calculate this

    //float yMin;
    //float yMax;
    //float yMinOffset;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;

        GameObject levelBoundaryLowerLeft = GameObject.Find("LevelBoundaryLowerLeft");
        GameObject levelBoundaryUpperRight = GameObject.Find("LevelBoundaryUpperRight");

        Debug.Log("Screen Width = " + Screen.width + "  Screen Width = " + Screen.height);

        if (!levelBoundaryLowerLeft)
            Debug.LogWarning("Place a LevelBoundary named LevelBoundaryLowerLeft on this level");
        if (!levelBoundaryUpperRight)
            Debug.LogWarning("Place a LevelBoundary named LevelBoundaryUpperRight on this level");

        //Debug.Log(levelBoundaryLowerLeft.transform.position );
        //Debug.Log(levelBoundaryUpperRight.transform.position);

        xMin = levelBoundaryLowerLeft.transform.position.x + xMinOffsetFromEdge;
        xMax = levelBoundaryUpperRight.transform.position.x - xMinOffsetFromEdge;

        //Debug.Log(xMin + " " + xMax);
        //yMin = levelBoundaryLowerLeft.transform.position.y + yMinOffset;
        //yMax = levelBoundaryUpperRight.transform.position.y - yMinOffset;

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        // Debug.Log(transform.position.x);

        // currently only following the player in x
        float xpos = player.transform.position.x + offset.x - xOffsetFromPlayer;
        transform.position = new Vector3(Mathf.Clamp(xpos, xMin, xMax), transform.position.y, transform.position.z);

    }
}
