using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Square")
        {
            Destroy(other.gameObject);
        }
    }
}
