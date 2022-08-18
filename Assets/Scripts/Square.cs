using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Square : MonoBehaviour
{
    private const int PLAYER_INDEX1 = 0;
    private const int PLAYER_INDEX2 = 1;

    private bool onConveyor = false;
    private bool _inFrontOfSpray = false;
    private bool _inFrontOfBuilder = false;
    private bool noMoreSparay = false;
    private bool noMoreBuilder = false;
    public SquareData squareData;

    public bool isDying;

    private BuildGrid _buildGrid;

    private SpriteRenderer _spriteRenderer;

    public int _type;
    private bool typeSet;
    public int _randomNumber;

    private ColorSignal _colorSignal;

    private Vector3 conveyorDir;

    private List<GameObject> conveyors = new List<GameObject>();

    private Collider2D squareCollider;

    private PlayerInputReciever _playerInputReciever1;
    public PlayerInputReciever _playerInputReciever2;

    private void Awake()
    {
        squareCollider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _buildGrid = FindObjectOfType<BuildGrid>();
        _colorSignal = FindObjectOfType<ColorSignal>();

        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
        _playerInputReciever2 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX2);
    }

    void Start()
    {
        _spriteRenderer.sprite = squareData.GetBaseSprite();

        _randomNumber = Random.Range(1, 5);
        _colorSignal.SetColor(_randomNumber - 1);
    }

    void Update()
    {
        if (onConveyor && !_inFrontOfSpray && !_inFrontOfBuilder)
        {
            transform.Translate(conveyorDir * squareData.GetSpeed() * Time.deltaTime);
        }

        if (_inFrontOfSpray)
        {
            if (_playerInputReciever1.ButtonEast)
            {
                SetType(1);
            }
            else if (_playerInputReciever1.ButtonSouth)
            {
                SetType(2);
            }
            else if (_playerInputReciever1.ButtonWest)
            {
                SetType(3);
            }
            else if (_playerInputReciever1.ButtonNorth)
            {
                SetType(4);
            }
        }
    }

    private void LateUpdate()
    {
        if (isDying)
        {
            Destroy(this.gameObject);
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
