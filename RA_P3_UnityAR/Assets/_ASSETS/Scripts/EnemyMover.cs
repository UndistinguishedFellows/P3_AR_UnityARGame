using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {

    public float speed;
    public Vector3 movementDirection;

    public int score;

    Rigidbody rb;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    //-------------------------------

    public void SetDirection(Vector3 dir)
    {
        movementDirection = dir;
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        
        rb.velocity = dir * speed;
    }
}
