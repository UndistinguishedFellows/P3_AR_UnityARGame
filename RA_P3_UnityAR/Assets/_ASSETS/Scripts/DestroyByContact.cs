using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Boundary"))
        {
            Instantiate(asteroidExplosion, transform.position, transform.rotation);

            if(other.CompareTag("Player"))
            {
                //TODO: Notify game over
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            }

            //TODO: Punctuation??

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
