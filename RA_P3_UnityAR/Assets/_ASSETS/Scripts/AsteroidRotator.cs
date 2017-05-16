using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotator : MonoBehaviour 
{
    public float rotationAngle;
    [SerializeField]
    private Vector3 axis;

    void Start()
    {
        axis = new Vector3(Random.value, Random.value, Random.value);
    }

	void Update () 
    {
        transform.Rotate(axis, rotationAngle * Mathf.Deg2Rad);
	}
}
