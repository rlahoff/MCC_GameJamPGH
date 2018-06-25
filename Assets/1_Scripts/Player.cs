using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //[SerializeField] Snowflake.COLOR my_Color = Snowflake.COLOR.Yellow;
    [SerializeField] COLOR my_Color = COLOR.Yellow;

    public GameObject[] colorRayPrefabs;

    public float speed = 8;
    public float rayFiringRate = 0.2f;

    enum Facing { LEFT, RIGHT };
    private Facing facing = Facing.RIGHT;

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (facing != Facing.LEFT)
            {
                facing = Facing.LEFT;
                //bool flip = GetComponent<SpriteRenderer>().flipX;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            }
            
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (facing != Facing.RIGHT)
            {
                facing = Facing.RIGHT;
                //bool flip = GetComponent<SpriteRenderer>().flipX;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            }

            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireColorRay", 0.000001f, rayFiringRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireColorRay");
        }
    }

    private void FireColorRay()
    {
        GameObject beam;

        beam = Instantiate(colorRayPrefabs[(int)my_Color], transform.position, Quaternion.identity) as GameObject;

        float speed = beam.gameObject.GetComponent<ColorRay>().GetSpeed();

        if (facing == Facing.RIGHT)
            beam.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        else
            beam.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);

        //AudioSource.PlayClipAtPoint(fireSound, transform.position);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Goal goal = collision.gameObject.GetComponent<Goal>();
        //Debug.Log("OnTriggerEnter2D");

        Debug.Log(collision.name);

        if (collision.name == "Goal")
        {
            TriggerGoal();
        }
        else if (collision.name.Contains("Snowflake"))
        {
            Snowflake snowflake = collision.gameObject.GetComponent<Snowflake>();
            TriggerSnowflake(snowflake.Color());
        }
    }

    private void TriggerSnowflake(COLOR color)
    {
        if (my_Color == color) return;

        my_Color = color;

    }

    private void TriggerGoal()
    {
        GameObject levelManagerGO = GameObject.Find("LevelManager");

        if (levelManagerGO)
        {
            LevelManager levelManager = levelManagerGO.GetComponent<LevelManager>();
            levelManager.LoadNextLevel();
        }
        else
            Debug.LogWarning("Place a LevelManager prefab on this level");
    }
}
