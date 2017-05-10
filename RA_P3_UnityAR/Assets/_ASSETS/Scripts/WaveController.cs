using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public float waveTimeSeparetion;
    [Range(0, 1)]
    public float spawnProbabilytyPerTile;
    public GameObject enemyPrefab;
    public Transform waveSpawnTransform;
    public Vector3 enemySpawnDirection;
    public MarkerDetectionScript spawnMarkerScript;

    public int columns;
    public float spawnBoundary;
    [SerializeField]
    private float xSeparation;

    public System.Action onWave;

    private WaitForSeconds waveSeparationWait;

	void Start ()
    {
        xSeparation = (spawnBoundary * 2) / columns;
        waveSeparationWait = new WaitForSeconds(waveTimeSeparetion);

        StartCoroutine(WaveSpawnCorutine());
    }

    //-------------------------

    IEnumerator WaveSpawnCorutine()
    {
        yield return waveSeparationWait;

        while (true)
        {
            if (spawnMarkerScript.markerDetected())
            {
                if (onWave != null)
                    onWave();

                for (int x = 0; x < columns; ++x)
                {
                    if (Random.value <= spawnProbabilytyPerTile)
                    {
                        Vector3 spawnPos = new Vector3(-spawnBoundary + xSeparation * x + xSeparation * 0.5f, 0.0f, 0.0f);
                        GameObject ship = Instantiate(enemyPrefab, spawnPos + waveSpawnTransform.position, Quaternion.AngleAxis(90.0f, enemySpawnDirection.normalized)) as GameObject;
                        
                    }
                }
            }

            yield return waveSeparationWait;
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
