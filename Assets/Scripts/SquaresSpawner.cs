using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresSpawner : MonoBehaviour
{
    public float SpawnCooldown = 2f;

    public GameObject baseSquare;

    private void Start() 
    {
        StartCoroutine(SpawnSquare());
    }

    IEnumerator SpawnSquare()
    {
        while(true)
        {
            Instantiate(baseSquare, transform.position, transform.rotation);
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}
