using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BuildSquare : MonoBehaviour
{
    private const int PLAYER_INDEX1 = 0;
    private const int PLAYER_INDEX2 = 1;

    public bool _isSelected = true;

    [SerializeField][Range(0, 1)] private float _threshold;

    [SerializeField][Range(0, 5)] private float _speed;

    [SerializeField] private Transform[] _points = new Transform[4];

    [SerializeField] private LayerMask _buildGridCellLM;
    public LayerMask _gridSquareLM;

    private Transform dirTransform;

    public bool _isBuilt;

    private int _type;
    private bool typeSet;
    public SpriteRenderer _spriteRenderer;

    public SquareData _squareData;

    private bool _canMove;

    private Rigidbody2D _rb;

    private PlayerInputReciever _playerInputReciever1;
    private PlayerInputReciever _playerInputReciever2;

    [HideInInspector]
    public Vector3 _clawOffset;

    private GameObject _claw;
    private bool _isFalling;
    private bool _isInClaw;

    private Collider2D _collider;

    private TetrisntGrid tetrisntGrid;

    public List<GameObject> _otherSquaresOfShape = new List<GameObject>();

    private void Awake()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
        _playerInputReciever2 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX2);

        _rb = GetComponent<Rigidbody2D>();
        _claw = FindObjectOfType<Claw>().transform.GetChild(0).gameObject;
        _collider = GetComponent<Collider2D>();
        tetrisntGrid = FindObjectOfType<TetrisntGrid>();
    }

    private void Start()
    {
        _canMove = true;
        _isInClaw = true;

        _rb.gravityScale = 0;

        _spriteRenderer.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        _spriteRenderer.sortingOrder = 5;

        gameObject.layer = LayerMask.NameToLayer("Default");

        FindObjectOfType<AudioManager>().Play("Assemble1");
    }

    private void Update()
    {
        if (!_isSelected)
        {
            _spriteRenderer.gameObject.transform.localScale = new Vector3(1, 1, 1);
            _spriteRenderer.sortingOrder = 3;

            gameObject.layer = LayerMask.NameToLayer("GridSquare");
        }

        if (_playerInputReciever2.Joystick.magnitude > _threshold && _isSelected)
        {
            Transform nearestPoint = _points[0];

            foreach (Transform point in _points)
            {
                if (Vector2.Distance(_playerInputReciever2.Joystick, point.position - transform.position) < Vector2.Distance(_playerInputReciever2.Joystick, nearestPoint.position - transform.position))
                {
                    nearestPoint = point;
                }
            }

            dirTransform = nearestPoint;
        }
        else
        {
            dirTransform = null;
        }

        if (_playerInputReciever2.Joystick.magnitude < 0.1f)
        {
            _canMove = true;
        }

        if (dirTransform != null && Physics2D.OverlapPoint(dirTransform.position, _buildGridCellLM) && _canMove && _isSelected)
        {
            _canMove = false;

            transform.position = dirTransform.position;

            Invoke("CanMoveAgain", _speed);
        }

        if (_isBuilt && !_isFalling && _isInClaw)
        {
            transform.position = _claw.transform.position + _clawOffset;

            if (_playerInputReciever1.LeftJoystickButton)
            {
                _isFalling = true;

                _collider.isTrigger = false;
            }
        }

        if (_isFalling)
        {
            _rb.gravityScale = 1;
            _isInClaw = false;
        }
        else
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
        }

        if (!_isFalling && !_isInClaw && !_isSelected)
        {
            transform.position = tetrisntGrid.GetNearestPoint(transform.position);
        }
    }

    public void SetType(int type)
    {
        if (!typeSet)
        {
            typeSet = true;
            _type = type;
            _spriteRenderer.sprite = _squareData.GetSprite(type - 1);
        }
    }

    private void CanMoveAgain()
    {
        _canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_otherSquaresOfShape.Contains(other.gameObject))
        {
            foreach (GameObject square in _otherSquaresOfShape)
            {
                square.GetComponent<BuildSquare>()._isFalling = false;
            }
        }
    }
}