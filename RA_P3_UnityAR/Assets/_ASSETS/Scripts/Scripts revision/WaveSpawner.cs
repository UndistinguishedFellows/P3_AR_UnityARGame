using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    private GameController gameController;

	public GameObject enemyPrefab;
    public float enemySize;
    public float enemySeparation;

    [Range(0, 1)]
    public float spawnProbPerTile;

    private int columns;
    private float distanceBetweenEnemies;

    public float innerWaveWaitTime;
    public float waveWaitTime;

    public int innerWavesAmountLines;
    public int waveAmmount;

    private int innerWaveLinesCounter = 0;
    private int waveCounter = 0;

    //-------------------------------------------------

	public Transform columnSpawnA;
	public Transform columnSpawnB;
    public Transform columnSpawnC;
    public Transform columnSpawnD;

    private bool boundariesCalculed = false;

    MarkerDetectionScript aMarker;
    MarkerDetectionScript bMarker;
    MarkerDetectionScript cMarker;
    MarkerDetectionScript dMarker;

	Vector3 spawnDirection;
    Vector3 ab;
    Vector3 cd;
    Vector3 abMid;
    Vector3 cdMid;

    public bool BoundariesReady
    {
        get { return boundariesCalculed; }
    }

	void Start () 
	{
        gameController = GetComponent<GameController>();

        aMarker = columnSpawnA.GetComponent<MarkerDetectionScript>();
        bMarker = columnSpawnB.GetComponent<MarkerDetectionScript>();
        cMarker = columnSpawnC.GetComponent<MarkerDetectionScript>();
        dMarker = columnSpawnD.GetComponent<MarkerDetectionScript>();

        StartCoroutine(Wave());
	}

	void Update ()
	{
        if (aMarker.markerDetected() && bMarker.markerDetected() && cMarker.markerDetected() && dMarker.markerDetected())
        {
            //Calc the plane

            ab = columnSpawnB.position - columnSpawnA.position;
            ab.Normalize();
            cd = columnSpawnD.position - columnSpawnC.position;
            cd.Normalize();

            //Calc de waves direction
            //Calc mid points for top and bottom
            abMid = columnSpawnA.position + new Vector3((columnSpawnB.position.x - columnSpawnA.position.x) / 2,
                (columnSpawnB.position.y - columnSpawnA.position.y) / 2,
                (columnSpawnB.position.z - columnSpawnA.position.z) / 2);

            cdMid = columnSpawnC.position + new Vector3((columnSpawnD.position.x - columnSpawnC.position.x) / 2,
                (columnSpawnD.position.y - columnSpawnC.position.y) / 2,
                (columnSpawnD.position.z - columnSpawnC.position.z) / 2);

            //Calc wave direction
            spawnDirection = cdMid - abMid;
            spawnDirection.Normalize();

            //Now boundaries are calculed...

            //Calc how many enemies must spawn in each line
            columns = (int)(Vector3.Distance(columnSpawnA.position, columnSpawnB.position) / (enemySize + enemySeparation));
            distanceBetweenEnemies = Vector3.Distance(columnSpawnA.position, columnSpawnB.position) / columns; //TODO: Stupid calcs...

        }
	}

    //------------------------------------------------------------------

    void SpawnLine()
    {
        for (int i = 0; i < columns; ++i)
        {
            if (Random.value < spawnProbPerTile)
            {
                Vector3 sp = columnSpawnA.position + ab * distanceBetweenEnemies * i + ab * distanceBetweenEnemies * 0.5f;
                GameObject ship = Instantiate(enemyPrefab, sp, transform.rotation) as GameObject;

                EnemyMover mover = ship.GetComponent<EnemyMover>();
                mover.SetDirection(spawnDirection);
            }
        }
    }

    IEnumerator Wave()
    {
        yield return new WaitForSeconds(waveWaitTime);

        while (waveCounter < waveAmmount && !gameController.GameEnded)
        {
            while(innerWaveLinesCounter < innerWavesAmountLines && !gameController.GameEnded)
            {
                SpawnLine();

                yield return new WaitForSeconds(innerWaveWaitTime);

                ++innerWaveLinesCounter;
            }

            innerWaveLinesCounter = 0;

            ++waveCounter;
            spawnProbPerTile += 0.1f;

            if (waveCounter >= waveAmmount)
            {
                gameController.Win();
                break;
            }

            yield return new WaitForSeconds(waveWaitTime);
        }
    }


    //----------------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine (columnSpawnA.position, columnSpawnB.position);
        Gizmos.DrawLine (columnSpawnB.position, columnSpawnC.position);
        Gizmos.DrawLine (columnSpawnC.position, columnSpawnD.position);
        Gizmos.DrawLine (columnSpawnD.position, columnSpawnA.position);

        if (!boundariesCalculed)
        {
            //Gizmos.DrawLine(columnSpawnA.position, gamePlane.normal + columnSpawnA.position);
            //Gizmos.DrawLine(columnSpawnB.position, gamePlane.normal + columnSpawnB.position);
            //Gizmos.DrawLine(columnSpawnC.position, gamePlane.normal + columnSpawnC.position);
            //Gizmos.DrawLine(columnSpawnD.position, gamePlane.normal + columnSpawnD.position);

            Gizmos.color = Color.green;
            //Gizmos.DrawWireSphere(abMid, 0.2f);
            //Gizmos.DrawWireSphere(cdMid, 0.2f);
            Gizmos.DrawLine(abMid, abMid + spawnDirection);

            Gizmos.color = Color.blue;
            for (int i = 0; i < columns; ++i)
            {
                Vector3 sp = columnSpawnA.position + ab * distanceBetweenEnemies * i + ab * distanceBetweenEnemies * 0.5f;
                Gizmos.DrawWireSphere(sp, 0.1f);
            }
        }
        else
        {
            Gizmos.DrawLine(columnSpawnA.position, columnSpawnA.position + columnSpawnA.up * 1);
            Gizmos.DrawLine(columnSpawnB.position, columnSpawnB.position + columnSpawnB.up * 1);
            Gizmos.DrawLine(columnSpawnC.position, columnSpawnC.position + columnSpawnC.up * 1);
            Gizmos.DrawLine(columnSpawnD.position, columnSpawnD.position + columnSpawnD.up * 1);

            Gizmos.color = Color.green;

            Vector3 abMidTmp = columnSpawnA.position + new Vector3((columnSpawnB.position.x - columnSpawnA.position.x) / 2,
                (columnSpawnB.position.y - columnSpawnA.position.y) / 2,
                (columnSpawnB.position.z - columnSpawnA.position.z) / 2);

            Vector3 cdMidTmp = columnSpawnC.position + new Vector3((columnSpawnD.position.x - columnSpawnC.position.x) / 2,
                (columnSpawnD.position.y - columnSpawnC.position.y) / 2,
                (columnSpawnD.position.z - columnSpawnC.position.z) / 2);

            //Gizmos.DrawWireSphere(abMidTmp, 0.2f);
            //Gizmos.DrawWireSphere(cdMidTmp, 0.2f);

            Vector3 tmpDir = cdMidTmp - abMidTmp;
            Gizmos.DrawLine(abMidTmp, abMidTmp + tmpDir.normalized);
        }
    }
}
