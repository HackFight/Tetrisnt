using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    private bool onConveyor = false;
    private bool _inFrontOfSpray = false;
    private bool noMoreSparay = false;
    public SquareData squareData;
    private SprayControls _sprayControls;

    private SpriteRenderer _spriteRenderer;

    private int _type;
    private bool typeSet;

    private Vector3 conveyorDir;

    private List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    private void Awake() 
    {
        _sprayControls = new SprayControls();
    }

    private void OnEnable() 
    {
        _sprayControls.Enable();
    }

    private void OnDisable() 
    {
        _sprayControls.Disable();
    }

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

        if(_inFrontOfSpray)
        {
            if(_sprayControls.Spray.Paint1.triggered)
        {
            SetType(1);
        }
        else if(_sprayControls.Spray.Paint2.triggered)
        {
            SetType(2);
        }
        else if(_sprayControls.Spray.Paint3.triggered)
        {
            SetType(3);
        }
        else if(_sprayControls.Spray.Paint4.triggered)
        {
            SetType(4);
        }
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

    public void InFrontOfSpray()
    {
        if(!noMoreSparay)
        {
            noMoreSparay = true;
            _inFrontOfSpray = true;
            Invoke("leaveSpray", 1);
        }
    }

    public void SetType(int type)
    {
        if(type != 0 && !typeSet)
        {
            typeSet = true;
            _type = type;
            _spriteRenderer.sprite = squareData.GetSprite(type - 1);
        }
    }
}
