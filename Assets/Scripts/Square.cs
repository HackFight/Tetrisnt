using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Square : MonoBehaviour
{
    private const int PLAYER_INDEX = 0;

    private bool onConveyor = false;
    private bool _inFrontOfSpray = false;
    private bool _inFrontOfBuilder = false;
    private bool noMoreSparay = false;
    private bool noMoreBuilder = false;
    public SquareData squareData;
    // private SprayControls _sprayControls;

    private SpriteRenderer _spriteRenderer;

    private int _type;
    private bool typeSet;

    private Vector3 conveyorDir;

    private List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    private PlayerInputReciever _playerInputReciever;

    private void Awake()
    {
        // _sprayControls = new SprayControls();

        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();

        _playerInputReciever = recievers.FirstOrDefault(i => i.PlayerIndex == PLAYER_INDEX);
    }

    private void OnEnable()
    {
        // _sprayControls.Enable();
    }

    private void OnDisable()
    {
        // _sprayControls.Disable();
    }

    void Start()
    {
        squareCollider = GetComponent<Collider2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = squareData.GetBaseSprite();
    }

    void Update()
    {
        if (onConveyor && !_inFrontOfSpray && !_inFrontOfBuilder)
        {
            transform.Translate(conveyorDir * squareData.GetSpeed() * Time.deltaTime);
        }

        if (_inFrontOfSpray)
        {
            if (_playerInputReciever.ButtonEast)
            {
                _playerInputReciever.ButtonEast = false;

                SetType(1);
            }
            else if (_playerInputReciever.ButtonSouth)
            {
                _playerInputReciever.ButtonSouth = false;

                SetType(2);
            }
            else if (_playerInputReciever.ButtonWest)
            {
                _playerInputReciever.ButtonWest = false;

                SetType(3);
            }
            else if (_playerInputReciever.ButtonNorth)
            {
                _playerInputReciever.ButtonNorth = false;

                SetType(4);
            }
        }
        else
        {
            _playerInputReciever.ButtonNorth = false;
            _playerInputReciever.ButtonWest = false;
            _playerInputReciever.ButtonSouth = false;
            _playerInputReciever.ButtonEast = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Conveyor")
        {
            conveyors.Add(other.gameObject);
            conveyorDir = conveyors[0].GetComponent<Conveyor>().direction;
            onConveyor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Conveyor")
        {
            conveyors.Remove(other.gameObject);

            if (conveyors.Count <= 0)
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

    private void leaveBuilder()
    {
        _inFrontOfBuilder = false;
    }

    public void InFrontOfSpray()
    {
        if (!noMoreSparay)
        {
            noMoreSparay = true;
            _inFrontOfSpray = true;
            Invoke("leaveSpray", 1);
        }
    }

    public void InFrontOfBuilder()
    {
        if (!noMoreBuilder)
        {
            noMoreBuilder = true;
            _inFrontOfBuilder = true;
            Invoke("leaveBuilder", 1);
        }
    }

    public void SetType(int type)
    {
        if (type != 0 && !typeSet)
        {
            typeSet = true;
            _type = type;
            _spriteRenderer.sprite = squareData.GetSprite(type - 1);
        }
    }
}
