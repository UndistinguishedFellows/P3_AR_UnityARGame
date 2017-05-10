using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {

    public float distanceToAdvance;
    public Vector3 movementDirection;

    WaveController waveController;
    Rigidbody rb;
    
	void Start ()
    {
        GameObject goController = GameObject.FindGameObjectWithTag("GameController");
        waveController = goController.GetComponent<WaveController>();
        waveController.onWave += MoveDown;

        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        waveController.onWave -= MoveDown;
    }

    //-------------------------------

    void MoveDown()
    {
        rb.position = rb.position + waveController.enemySpawnDirection * distanceToAdvance;
    }
}
