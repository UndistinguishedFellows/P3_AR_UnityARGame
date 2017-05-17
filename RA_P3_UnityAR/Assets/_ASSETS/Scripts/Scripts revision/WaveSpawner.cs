using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public Transform columnSpawn1;
	public Transform columnSpawn2;

	Vector3 spawnLine;

	void Start () 
	{
		
	}

	void Update ()
	{
		spawnLine = columnSpawn2.position - columnSpawn1.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (columnSpawn1.position, columnSpawn2.position);

		float dist = spawnLine.magnitude / 2;
		Vector3 midPos = columnSpawn1.position + new Vector3(dist, dist, dist);

		Gizmos.DrawSphere (midPos, 0.2f);
		//Gizmos.DrawLine ();
	}
}
