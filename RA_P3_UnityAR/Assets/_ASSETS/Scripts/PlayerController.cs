using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public GameObject shotPrefab;
    public Transform shotSpawnPont;
    public float shootingRatio;

    public Boundary boundary;


    private float timeSinceNextShot = 0.0f;

    private Rigidbody rb;
    private AudioSource lazerAudio;

    //--Some testing variables
    private float speed = 10.0f;
    private float rotTilt = 4.0f;
    //------------------------

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        lazerAudio = GetComponent<AudioSource>();
    }

	void Update ()
    {
        if (Time.time >= timeSinceNextShot)
        {
            Instantiate(shotPrefab, shotSpawnPont.position, shotSpawnPont.rotation);
            lazerAudio.Play();
            timeSinceNextShot += shootingRatio;
        }

	}

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        rb.velocity = input * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, -rotTilt * rb.velocity.x);

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
    }
}
