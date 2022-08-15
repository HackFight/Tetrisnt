using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    public float speed;
    private bool onConveyor = false;
    public bool _inFrontOfSpray = false;
    public bool noMoreSparay = false;

    public List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    void Start()
    {
        squareCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if(onConveyor && !_inFrontOfSpray)
        {
            transform.Translate(conveyors[0].GetComponent<Conveyor>().direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Conveyor")
        {
            conveyors.Add(other.gameObject);
            onConveyor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Conveyor")
        {
            conveyors.Remove(other.gameObject);
            onConveyor = false;
        }
    }

    public void InFrontOfSpray()
    {
        _inFrontOfSpray = true;
        Invoke("leaveSpray", 1);
    }

    private void leaveSpray()
    {
        _inFrontOfSpray = false;
        noMoreSparay = true;
    }
}
