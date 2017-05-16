using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject shotPrefab;
    public Transform shotSpawnPont;
    public float shootingRatio;

	public MarkerDetectionScript rightMarker;
	public MarkerDetectionScript leftMarker;

    public float xLimit;

	public float speed;
	public float rotTilt = 4.0f;

    private float timeSinceNextShot = 0.0f;

    private Rigidbody rb;
    private AudioSource lazerAudio;
    private GameController gameController;

	//--------------------------------------------------

	void Start ()
    {
        timeSinceNextShot = 0.0f;
        rb = GetComponent<Rigidbody>();
        lazerAudio = GetComponent<AudioSource>();
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gameController = go.GetComponent<GameController>();
    }

	void Update ()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 30.0f);
        if (!gameController.GameEnded && Time.timeSinceLevelLoad >= timeSinceNextShot)
        {
            GameObject go = Instantiate(shotPrefab, shotSpawnPont.position, shotSpawnPont.rotation) as GameObject; //Quaternion.AngleAxis(90.0f, Vector3.right)
            //Rigidbody goRB = go.GetComponent<Rigidbody>();
            //goRB.velocity = Vector3.up * 20;//transform.forward * 20;
            lazerAudio.Play();
            timeSinceNextShot += shootingRatio;
        }

	}

    void FixedUpdate()
	{
		Vector3 inp = Vector3.zero;

		if (rightMarker.markerDetected ())
			inp += Vector3.right * speed;
		if (leftMarker.markerDetected ())
			inp -= Vector3.right * speed;


		
        //Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //rb.velocity = input * speed;

		rb.velocity = inp;
		rb.rotation = Quaternion.Euler(-90.0f, -rotTilt * rb.velocity.x, 0.0f);

		rb.position = new Vector3(Mathf.Clamp(rb.position.x, -xLimit, xLimit),
			transform.position.y, transform.position.z);
		
        //rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            //0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        //rb.position = new Vector3(rb.position.x, 0.0f, rb.position.z);
        //rb.rotation = Quaternion.identity;

        //rb.position = new Vector3(transform.position.x, transform.position.y, 30.0f);
    }
}
