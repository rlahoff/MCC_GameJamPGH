using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //[SerializeField] Snowflake.COLOR my_Color = Snowflake.COLOR.Yellow;
    COLOR my_Color = COLOR.Yellow;

    enum STATE { LEVEL_PLAY, FINAL_SCREEN, START_SCREEN };
    [SerializeField] STATE my_state = STATE.LEVEL_PLAY;

    public GameObject[] colorRayPrefabs;    // set these in the inspector
    public AudioClip fireSound;             // andrea

    public float speed = 8;
    public float rayFiringRate = 0.2f;

    enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };
    
    private Facing my_facing = Facing.EAST;
    //private Facing my_facing = Facing.EAST;
    private Facing my_oldFacing = Facing.EAST;

    static float playingTimeElapsed = 0;
    static int totalSharks = 0;

    // for tutorial
    bool firstFireColorRay = false;
    // Use this for initialization
    void Start()
    {
        //Check if the current Active Scene's name is your first Scene
        String sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level 01")
        {
            playingTimeElapsed = 0;
            totalSharks = 0;
        }


        switch (my_state)
        {
            case STATE.LEVEL_PLAY:
                totalSharks += Score.SharksOnThisLevel();
                break;

            case STATE.FINAL_SCREEN:
                if (sceneName != "Final")
                {
                    Debug.LogError("Player my_state set to FINAL_SCREEN.  Change to LEVEL_PLAY in inspector.");
                    my_state = STATE.LEVEL_PLAY;
                }
                else
                {
                    StartForFinalScene();
                }
                break;

            case STATE.START_SCREEN:
                if (sceneName != "Start")
                {
                    Debug.LogError("Player my_state set to START_SCREEN.  Change to LEVEL_PLAY in inspector.");
                    my_state = STATE.LEVEL_PLAY;
                }
                else
                {
                    StartForStartScene();
                }
                break;
        }
    }

    private void StartForStartScene()
    {
        GameObject penguin = GameObject.Find("Penguin_Blue");
        penguin.GetComponent<Animator>().SetTrigger("BlueSwim");

        penguin = GameObject.Find("Penguin_Green");
        penguin.GetComponent<Animator>().SetTrigger("GreenSwim");
    }

    private void StartForFinalScene()
    {
        GameObject shark = GameObject.Find("Shark_Yellow");
        shark.GetComponent<Animator>().Play("Yellow");

        shark = GameObject.Find("Shark_Green");
        shark.GetComponent<Animator>().SetTrigger("Friendly");

        GameObject text = GameObject.Find("ScoreText");
        text.GetComponent<Text>().text = "Score: " + Score.GetGameScore().ToString();

        int missed = totalSharks - Score.GetGameScore();
        text = GameObject.Find("MissedText");
        text.GetComponent<Text>().text = "Missed: " + missed.ToString();

        text = GameObject.Find("TimeText");
        //playingTimeElapsed.ToString(
        string str = string.Format("{0:0.00}", playingTimeElapsed);
        text.GetComponent<Text>().text = "Time: " + str + " seconds";
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        playingTimeElapsed += Time.fixedDeltaTime;
    }

    private void ProcessInput()
    {
        // as an easter egg intentionally leaving in the ability to move around the final screen
        if (my_state == STATE.START_SCREEN)
            return;

        bool moveRight = false;
        bool moveLeft = false;
        bool moveUp = false;
        bool moveDown = false;

        bool moving = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveRight = true;
            moving = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveUp = true;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveDown = true;
            moving = true;
        }

        //todo, support number pad?
        
        if (moving) MovePlayer(moveRight, moveLeft, moveUp, moveDown);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireColorRay", 0.000001f, rayFiringRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireColorRay");
        }
    }

    private void MovePlayer(bool moveRight, bool moveLeft, bool moveUp, bool moveDown)
    {
        my_oldFacing = my_facing;

        if (moveLeft)
        {
            if (moveUp)
                my_facing = Facing.NORTHWEST;
            else if (moveDown)
                my_facing = Facing.SOUTHWEST;
            else
                my_facing = Facing.WEST;
        }
        else if (moveRight)
        {
            if (moveUp)
                my_facing = Facing.NORTHEAST;
            else if (moveDown)
                my_facing = Facing.SOUTHEAST;
            else
                my_facing = Facing.EAST;
        }
        else if (moveUp)
            my_facing = Facing.NORTH;
        else
            my_facing = Facing.SOUTH;

       // Debug.Log("facing: " + my_facing);

        Vector3 moveVector = FacingVector(my_facing);
       
        // move
        transform.position += moveVector * speed * Time.deltaTime;

        // rotate
        if (my_facing != my_oldFacing)
        {
            Vector3 rotateVector = RotationVector(my_facing);
            transform.Rotate(rotateVector);

            //enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };
            int[] flip = new int[] { 0, 0, 0, 0, 0, 1, 1, 1 };
            if (flip[(int)my_facing] != flip[(int)my_oldFacing])
                //bool flip = GetComponent<SpriteRenderer>().flipX;
                GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;


        }
    }

    private Vector3 RotationVector(Facing facing)
    {
        //enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };

        int steps;

        // do not allow facing up or down (when moving up or down have to face east or west)
        if (my_facing == Facing.NORTH || my_facing == Facing.SOUTH)
        {
            if (my_oldFacing > Facing.NORTH && my_oldFacing < Facing.SOUTH)
                my_facing = Facing.EAST;
            else
                my_facing = Facing.WEST;
        }

        steps = my_oldFacing - my_facing;
        //Debug.Log(steps);

        return new Vector3(0, 0, steps * 45);
    }

    private Vector3 FacingVector(Facing facing)
    {
        Vector3 moveVector;

        switch (facing)
        {
            case (Facing.NORTH):
                moveVector = new Vector3(0, 1, 0);
                break;
            case (Facing.NORTHEAST):
                moveVector = new Vector3(1, 1, 0);
                break;
            case (Facing.EAST):
                moveVector = new Vector3(1, 0, 0);
                break;
            case (Facing.SOUTHEAST):
                moveVector = new Vector3(1, -1, 0);
                break;
            case (Facing.SOUTH):
                moveVector = new Vector3(0, -1, 0);
                break;
            case (Facing.SOUTHWEST):
                moveVector = new Vector3(-1, -1, 0);
                break;
            case (Facing.WEST):
                moveVector = new Vector3(-1, 0, 0);
                break;
            case (Facing.NORTHWEST):
                moveVector = new Vector3(-1, 1, 0);
                break;
            default:    // should never get here
                Debug.LogWarning("unhandled facing");
                moveVector = new Vector3(0, 0, 0);
                break;
        }
        //Debug.Log(moveVector);
        return moveVector;
    }

    private void FireColorRay()
    {
        // for tutorial state
        if (!firstFireColorRay)
        {
            GameObject tutorial = GameObject.Find("TutorialCollider");
            if (tutorial)
            {
                GameObject tutorialText = GameObject.Find("Tutorial Text");
                if (tutorialText.GetComponent<TutorialText>().GetTutorialState() == TUTORIAL_STATE.SPACEBAR)
                {
                    firstFireColorRay = true;
                }
                else
                    return; // you can't fire yet
            }
            else
                firstFireColorRay = true;
        }
        // on my computer if i'm holding UP and LEFT at the same time then SPACEBAR doesn't work
        // but I'm fine using WA or the numberkey pad with numlock off

        //Debug.Log("FireColorRay " + my_facing);
        GameObject beam;

        beam = Instantiate(colorRayPrefabs[(int)my_Color], transform.position, Quaternion.identity) as GameObject;

        float speed = beam.gameObject.GetComponent<ColorRay>().GetSpeed();
        beam.gameObject.GetComponent<ColorRay>().SetColor(my_Color);


        //enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };
        Vector3 facingVector = FacingVector(my_facing);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * facingVector.x, speed * facingVector.y);

        AudioSource.PlayClipAtPoint(fireSound, transform.position); // andrea
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);

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
        Animator animator = GetComponent<Animator>();

        switch (my_Color)
        {
            case COLOR.Yellow:
                animator.SetTrigger("YellowSwim");
                break;
            case COLOR.Green:
                animator.SetTrigger("GreenSwim");
                break;
            case COLOR.Blue:
                animator.SetTrigger("BlueSwim");
                break;
            default:
                Debug.LogWarning("Missing Penguin animation state or handling " + my_Color + "in this swtich statement");
                break;
        }

        

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


