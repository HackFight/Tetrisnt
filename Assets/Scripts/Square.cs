using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    private bool onConveyor = false;
    private bool _inFrontOfSpray = false;
    private bool noMoreSparay = false;
    public SquareData squareData;

    private Vector3 conveyorDir;

    private List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    void Start()
    {
        squareCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if(onConveyor && !_inFrontOfSpray)
        {
            transform.Translate(conveyorDir * squareData.GetSpeed() * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Conveyor")
        {
            conveyors.Add(other.gameObject);
            conveyorDir = conveyors[0].GetComponent<Conveyor>().direction;
            onConveyor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Conveyor")
        {
            conveyors.Remove(other.gameObject);

            if(conveyors.Count <= 0)
            {
                onConveyor = false;
            }
            else
            {
                conveyorDir = conveyors[0].GetComponent<Conveyor>().direction;
                onConveyor = true;
            }
        }
    }

    private void leaveSpray()
    {
        _inFrontOfSpray = false;
        noMoreSparay = true;
    }

    public void InFrontOfSpray()
    {
        if(!noMoreSparay)
        {
            _inFrontOfSpray = true;
        Invoke("leaveSpray", 1);
        }
    }
}
