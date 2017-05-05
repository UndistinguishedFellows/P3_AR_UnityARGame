using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMover : MonoBehaviour {

    public float initialSpeed = 10.0f;

    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = transform.forward * initialSpeed;
        }
	}
}
