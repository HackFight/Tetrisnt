using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    private bool onConveyor = false;
    private bool _inFrontOfSpray = false;
    private bool noMoreSparay = false;
    public SquareData squareData;

    private SpriteRenderer _spriteRenderer;

    private int _type;

    private Vector3 conveyorDir;

    private List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    void Start()
    {
        squareCollider = GetComponent<Collider2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    public void InFrontOfSpray(int type)
    {
        if(!noMoreSparay)
        {
            noMoreSparay = true;
            _inFrontOfSpray = true;
            SetType(type);
            Invoke("leaveSpray", 1);
        }
    }

    public void SetType(int type)
    {
        if(type != 0)
        {
            _type = type;
            _spriteRenderer.color = squareData.GetColor(type - 1);
        }
    }
}
