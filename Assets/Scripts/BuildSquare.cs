using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildSquare : MonoBehaviour
{
    private const int PLAYER_INDEX1 = 0;
    private const int PLAYER_INDEX2 = 1;

    [SerializeField][Range(0, 1)] private float _threshold;

    [SerializeField][Range(0, 5)] private float _speed;

    [SerializeField] private Transform[] _points = new Transform[4];
    private Transform dirTransform;

    private int _type;
    private bool typeSet;
    private SpriteRenderer _spriteRenderer;

    public SquareData _squareData;

    private bool _canMove;

    private PlayerInputReciever _playerInputReciever1;
    private PlayerInputReciever _playerInputReciever2;

    private void Awake()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
        _playerInputReciever2 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX2);

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (_playerInputReciever2.Joystick.magnitude > _threshold)
        {

            foreach (Transform point in _points)
            {
                Transform nearestPoint = _points[0];

                if (Vector2.Distance(_playerInputReciever2.Joystick, point.position) < Vector2.Distance(_playerInputReciever2.Joystick, nearestPoint.position))
                {
                    nearestPoint = point;
                }

                dirTransform = nearestPoint;
                //print(nearestPoint);
                //print(Vector2.Distance(_playerInputReciever2.Joystick, point.position));
                print(Vector2.Distance(_points[0].position, _points[1].position));
            }
        }

        if (Physics2D.OverlapPoint(dirTransform.position, 7) && _canMove)
        {
            _canMove = false;

            transform.position = Physics2D.OverlapPoint(dirTransform.position, 7).transform.position;

            Invoke("CanMoveAgain", _speed);
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
