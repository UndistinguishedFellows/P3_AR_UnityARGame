using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private GameController gameController;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gameController = go.GetComponent<GameController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestroyByContact dbc = other.gameObject.GetComponent<DestroyByContact>();
            gameController.Score(-dbc.enemyScore/2);
        }

        Destroy(other.gameObject);
    }

}
