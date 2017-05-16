using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    public int enemyScore;

    private GameController gameController;

	void Start ()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gameController = go.GetComponent<GameController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Boundary"))
        {
            Instantiate(asteroidExplosion, transform.position, transform.rotation);

            if(other.CompareTag("Player"))
            {
                gameController.GameOver();
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            }

            gameController.Score(enemyScore);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
