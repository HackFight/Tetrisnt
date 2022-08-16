using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresSpawner : MonoBehaviour
{
    public float SpawnCooldown = 2f;

    public GameObject baseSquare;

    private void Start() 
    {
        InvokeRepeating("SpawnSquare", SpawnCooldown, SpawnCooldown);
    }

    void SpawnSquare()
    {
        Instantiate(baseSquare, transform.position, transform.rotation);
    }
}
