using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject shotPrefab;
    public Transform shotSpawnPoint;
    public float shootingRatio;

    public bool autoShot = false;

    private float timeSinceNextShot = 0.0f;

    private Rigidbody rb;
    private AudioSource lazerAudio;

	//--------------------------------------------------

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        lazerAudio = GetComponent<AudioSource>();
    }

	void Update ()
    {
        if (autoShot)
        {
            Shot();
        }
	}

    void Shot()
    {
        if (Time.timeSinceLevelLoad >= timeSinceNextShot)
        {
            GameObject go = Instantiate(shotPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation) as GameObject; //Quaternion.AngleAxis(90.0f, Vector3.right)
            lazerAudio.Play();
            timeSinceNextShot += shootingRatio;
        }
    }

    public void OnClick()
    {
        Shot();
    }

}
