using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private GameController gc;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gc = go.GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
            gc.Score(-5);
        
        Destroy(other.gameObject);
    }

}
