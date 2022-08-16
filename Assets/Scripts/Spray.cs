using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    public float sprayOffset;

    public List<GameObject> squaresInFrontOfSpray = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        foreach (GameObject square in squaresInFrontOfSpray)
        {
            Square squareScript = square.GetComponent<Square>();

            if(square.transform.position.y >= transform.position.y - sprayOffset && square.gameObject.transform.position.y <= transform.position.y + sprayOffset)
            {
                squareScript.InFrontOfSpray();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Square")
        {
            squaresInFrontOfSpray.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Square")
        {
            squaresInFrontOfSpray.Remove(other.gameObject);
        }
    }
}
