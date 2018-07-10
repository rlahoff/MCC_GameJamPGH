using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRay : MonoBehaviour {

    [SerializeField] float speed = 15f;

    private COLOR my_color;

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
        Object.Destroy(gameObject);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
