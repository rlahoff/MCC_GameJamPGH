using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRay : MonoBehaviour {

    [SerializeField] float speed = 10f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
