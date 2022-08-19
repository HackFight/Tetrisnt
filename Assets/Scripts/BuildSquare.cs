using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
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

    public bool _isFalling;

    private int _type;
    private bool typeSet;
    public SpriteRenderer _spriteRenderer;

    public SquareData _squareData;

    private bool _canMove;

    private Rigidbody2D _rb;

    private PlayerInputReciever _playerInputReciever1;
    private PlayerInputReciever _playerInputReciever2;

    private void Awake()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
        _playerInputReciever2 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX2);

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _canMove = true;

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

        if (_isFalling)
        {
            _rb.gravityScale = 1;
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
}