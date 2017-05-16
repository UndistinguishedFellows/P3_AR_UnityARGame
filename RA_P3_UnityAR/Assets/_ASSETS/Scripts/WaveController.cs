using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public float innerWaveTimeSeparetion;
    public float waveTimeSeparation;
    [Range(0, 1)]
    public float spawnProbabilytyPerTile;
    public GameObject enemyPrefab;
    public Transform waveSpawnTransform;
    public Vector3 enemySpawnDirection;

    public int columns;
    public float spawnBoundary;
    [SerializeField]
    private float xSeparation;

    public int innerWaveAmmount;
    private int innerWaveCounter = 0;
    public int waveAmmount;
    private int waveCounter = 0;

    public System.Action onWave;

    private WaitForSeconds innerWaveSeparationWait;
    private WaitForSeconds waveSeparationWait;

    private GameController gameController;

	void Start ()
    {
        innerWaveCounter = 0;
        waveCounter = 0;

        gameController = GetComponent<GameController>();
        
        xSeparation = (spawnBoundary * 2) / columns;
        innerWaveSeparationWait = new WaitForSeconds(innerWaveTimeSeparetion);
        waveSeparationWait = new WaitForSeconds(waveTimeSeparation);

        StartCoroutine(WaveSpawnCorutine());
    }

    //-------------------------

    IEnumerator WaveSpawnCorutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (!gameController.GameEnded)
        {
            while (!gameController.GameEnded && innerWaveCounter < innerWaveAmmount)
            {
                if (onWave != null)
                    onWave();

                for (int x = 0; x < columns; ++x)
                { 
                    if (Random.value <= spawnProbabilytyPerTile)
                    {
                        Vector3 spawnPos = new Vector3(-spawnBoundary + xSeparation * x + xSeparation * 0.5f, 0.0f, 0.0f);
                        GameObject ship = Instantiate(enemyPrefab, spawnPos + waveSpawnTransform.position, Quaternion.identity, waveSpawnTransform) as GameObject;

                    }
                }

                ++innerWaveCounter;

                yield return innerWaveSeparationWait;
            }


            ++waveCounter;
            spawnProbabilytyPerTile += 0.10f;

            if (waveCounter > waveAmmount)
            {
                gameController.Win();
                break;
            }
            
            StartCoroutine(MoveWhileWaitForWave());

            yield return waveSeparationWait;

            innerWaveCounter = 0;
        }
    
    }

    IEnumerator MoveWhileWaitForWave()
    {
        while (innerWaveCounter > 0)
        {
            if (onWave != null)
                onWave();

            yield return new WaitForSeconds(innerWaveTimeSeparetion);

            if (innerWaveCounter > 0)
                break;
        }
    }

    //------------------------------

    private void OnDrawGizmos()
    {
        float speparation = (spawnBoundary * 2) / columns;
        Gizmos.color = Color.green;
        Vector3 pos = new Vector3((spawnBoundary - spawnBoundary) / 2.0f, 0.0f, 0.0f);
        Gizmos.DrawWireCube(pos + waveSpawnTransform.position, new Vector3((spawnBoundary * 2), 1.0f, 1.0f));
        Gizmos.color = Color.red;
        for (int x = 0; x < columns; ++x)
        {
            Vector3 spawnPos = new Vector3(-spawnBoundary + speparation * x + speparation * 0.5f, 0.0f, 0.0f);
            Gizmos.DrawWireCube(spawnPos + waveSpawnTransform.position, Vector3.one * 0.5f); //TODO: Get the prefab size or separation??
            Gizmos.DrawLine(spawnPos + waveSpawnTransform.position, enemySpawnDirection.normalized + spawnPos + waveSpawnTransform.position);
        }
    }
}
