using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    private GameController gameController;
    private EnemyMover enemyMover;

	void Start ()
    {
        GameObject gcGO = GameObject.FindGameObjectWithTag("GameController");
        gameController = gcGO.GetComponent<GameController>();
        enemyMover = GetComponent<EnemyMover>();
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

            gameController.Score(enemyMover.score);


            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
