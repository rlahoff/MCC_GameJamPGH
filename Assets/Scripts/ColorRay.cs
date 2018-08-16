using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRay : MonoBehaviour {

    [SerializeField] float speed = 15f;
    public GameObject hitParticles;

    private COLOR my_color;
    private Player.Facing my_facing;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(COLOR color)
    {
        my_color = color;
    }

    public void SetFacing(Player.Facing facing)
    {
        my_facing = facing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If I collided with a shark I will let the shark handle it,
        // for all others I will destroy myself
        Shark shark = collision.gameObject.GetComponent<Shark>();

        if (shark)
            shark.HitByColorRay(my_color);

        Hit();
    }

    public void Hit()
    {
        // break into pieces
        ParticleSystem.MainModule main = hitParticles.GetComponent<ParticleSystem>().main;

        if (hitParticles.GetComponent<ParticleSystem>())
        {
            switch (my_color)
            {
                case COLOR.Yellow:
                    main.startColor = Color.yellow;  // yellow
                    break;

                case COLOR.Green:
                    main.startColor = Color.green;
                    break;

                case COLOR.Blue:
                    main.startColor = Color.blue;
                    break;

                default:
                    Debug.LogWarning("missing colorRay color");
                    break;
            }

            //Rotate();   // explode in the opposite direction
                        //Instantiate(hitParticles, transform.position, Quaternion.identity);
                        //public enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };
            Vector3[] particleRotation = new [] {
                new Vector3(-90f, 0f, 0f), new Vector3(45f, -45f, 0f),
                new Vector3(0f, -90f, 0f), new Vector3(-45f, -45f, 0f),
                new Vector3(90f, 0f, 0f), new Vector3(-45f, 45f, 0f),
                new Vector3(0f, 90f, 0f), new Vector3(45f, 45f, 0f)
            };
            Instantiate(hitParticles, transform.position, Quaternion.Euler(particleRotation[(int)my_facing]));
        }

        //
        Object.Destroy(gameObject);
    }

    public float GetSpeed()
    {
        return speed;
    }
/*
    private void Rotate()
    {
        switch(my_facing)
        {
            //ParticleSystem.MainModule main = hitParticles.GetComponent<ParticleSystem>().main;

            //public enum Facing { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };
 //           case Player.Facing.NORTH:
 //               hitParticles.transform.rotation = new Vector3(0, 0, 0);
 //               break;
            case Player.Facing.EAST:
                //               hitParticles.transform.Rotate(Vector3.forward);
                //                hitParticles.transform.Rotate(Vector3.left);
                Debug.Log(hitParticles.transform.rotation);
                hitParticles.transform.Rotate(Vector3.up);
                Debug.Log(hitParticles.transform.rotation);
                break;
  //          case Player.Facing.WEST:
  //              hitParticles.transform.rotation = new Vector3(0, -90, 0);
  //              break;

        }
    }*/
}
